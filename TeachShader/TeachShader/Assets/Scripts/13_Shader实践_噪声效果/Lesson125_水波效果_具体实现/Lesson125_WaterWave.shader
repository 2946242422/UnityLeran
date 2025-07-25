Shader "Unlit/Lesson125_WaterWave"
{
      Properties
    {
        //主纹理
        _MainTex("MainTex", 2D) = ""{}
        //法线纹理
        _BumpMap("BumpMap", 2D) = ""{}
        //立方体纹理
        _Cube("Cubemap", Cube) = ""{}
        
        //控制折射扭曲程度的变量
        _Distortion("Distortion", Range(0,10)) = 0

        //控制水波水平和竖直速度偏移的属性
        _WaveXSpeed("WaveXSpeed", Range(-0.1, 0.1)) = 0.01
        _WaveYSpeed("WaveYSpeed", Range(-0.1, 0.1)) = 0.01
        //之后用于计算菲涅耳效果的系数
        _FresnelScale("FresnelScale", Range(0,1)) = 1
    }
    SubShader
    {
        //将渲染队列改为透明的 目的是让玻璃对象 滞后渲染
        //能够捕获到之前正确的屏幕图像
        Tags{"RenderType"="Opaque" "Queue"="Transparent"}
        //使用它来捕获当前屏幕内容 并存储到默认的渲染纹理变量中
        GrabPass{}

        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;//法线纹理
            float4 _BumpMap_ST;//法线纹理的缩放和平移
            samplerCUBE _Cube;
            //GrabPass默认存储的纹理变量 这个是规则
            sampler2D _GrabTexture;
            float _Distortion;

            fixed _WaveXSpeed;
            fixed _WaveYSpeed;
            float _FresnelScale;

            struct v2f
            {
                float4 pos:SV_POSITION;//裁剪空间下的顶点坐标
                //用于存储从屏幕图像中采样的坐标（顶点相对于屏幕的位置）
                float4 grabPos:TEXCOORD0;
                //用于在颜色纹理中采样的UV坐标
                float4 uv:TEXCOORD1;
                //世界空间下的反射向量
                //我们将把反射向量的计算放在顶点着色器函数中 节约性能 表现效果也不会太差 肉眼几乎分辨不出来
                //float3 worldRefl:TEXCOORD2;

                //代表我们切线空间到世界空间的 变换矩阵的3行
                float4 TtoW0:TEXCOORD3;
                float4 TtoW1:TEXCOORD4;
                float4 TtoW2:TEXCOORD5;
            };

            v2f vert(appdata_full v)
            {
                v2f o;
                //顶点坐标转换
                o.pos = UnityObjectToClipPos(v.vertex);
                //屏幕坐标转换相关的内容
                o.grabPos = ComputeGrabScreenPos(o.pos);
                //uv坐标计算相关的内容
                o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                //法线纹理的uv坐标计算
                o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                //计算反射光向量
                //1.计算世界空间下法线向量
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.世界空间下的顶点坐标
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                ////3.计算视角方向 内部是用摄像机位置 - 世界坐标位置 
                //fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                ////4.计算反射向量
                //o.worldRefl = reflect(-worldViewDir, worldNormal);

                //世界空间下切线
                float3 worldTangent = UnityObjectToWorldDir(v.tangent);
                //计算副切线 计算叉乘结果后 垂直与切线和法线的向量有两条 通过乘以 切线当中的w，就可以确定是哪一条
                float3 worldBinormal = cross(normalize(worldTangent), normalize(worldNormal)) * v.tangent.w;

                o.TtoW0 = float4(worldTangent.x, worldBinormal.x,  worldNormal.x, worldPos.x);
                o.TtoW1 = float4(worldTangent.y, worldBinormal.y,  worldNormal.y, worldPos.y);
                o.TtoW2 = float4(worldTangent.z, worldBinormal.z,  worldNormal.z, worldPos.z);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //世界空间下视角方向
                float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));

                ////通过纹理采样函数 取出法线纹理贴图当中的数据
                //float4 packedNormal = tex2D(_BumpMap, i.uv.zw);
                ////将我们取出来的法线数据 进行逆运算并且可能会进行解压缩的运算，最终得到切线空间下的法线数据
                //float3 tangentNormal = UnpackNormal(packedNormal);
                
                //加入的一个水波移动速度的感觉
                //这个是图形学前辈们总结的一套简单的扰动算法 可以让水波、火焰、玻璃折射等 出现扰动感（动态感）
                float2 speed = _Time.y * float2(_WaveXSpeed, _WaveYSpeed);
                fixed3 bump1 = UnpackNormal(tex2D(_BumpMap, i.uv.zw + speed)).rgb;
                fixed3 bump2 = UnpackNormal(tex2D(_BumpMap, i.uv.zw - speed)).rgb;
                fixed3 bump = normalize(bump1 + bump2);

                //把计算完毕后的切线空间下的法线转换到世界空间下
                //float3x3 rotation = float3x3(i.TtoW0.xyz, i.TtoW1.xyz, i.TtoW2.xyz );
                //float3 worldNormal = mul(rotation, bump);
                //本质 就是在进行矩阵运算
                float3 worldNormal = float3(dot(i.TtoW0.xyz, bump), dot(i.TtoW1.xyz, bump), dot(i.TtoW2.xyz, bump));


                //反射颜色相关的计算 会叠加主纹理颜色
                //对颜色纹理进行采样
                fixed4 mainTex = tex2D(_MainTex, i.uv + speed);

                //我们需要在这里计算反射向量 因为这时才把法线纹理中的法线信息 算出来
                float3 refl = reflect(-viewDir, worldNormal);
                //将反射颜色和主纹理颜色进行叠加
                fixed4 reflColor = texCUBE(_Cube, refl) * mainTex;
                
                //折射相关的颜色
                //其实就是从我们抓取的 屏幕渲染纹理中进行采样 参与计算
                //抓取纹理中的颜色信息 相当于是这个玻璃对象后面的颜色

                //想要有折射效果 可以在采样之前 进行xy屏幕坐标的偏移
                float2 offset = bump.xy * _Distortion;
                //xy偏移一个位置
                i.grabPos.xy = offset*i.grabPos.z + i.grabPos.xy; 

                //利用透视除法 将屏幕坐标转换到 0~1范围内 然后再进行采样
                fixed2 screenUV = i.grabPos.xy / i.grabPos.w;
                //从捕获的渲染纹理中进行采样 获取后面的颜色
                fixed4 grabColor = tex2D(_GrabTexture, screenUV);

                //利用schlick菲涅耳近似等式 计算菲涅耳反射率
                fixed fresnel = _FresnelScale + (1-_FresnelScale) * pow(1-dot(normalize(viewDir), normalize(worldNormal)), 5);

                float4 color = reflColor*fresnel + grabColor * (1-fresnel);

                return color;
            }

            ENDCG
        }
    }
}
