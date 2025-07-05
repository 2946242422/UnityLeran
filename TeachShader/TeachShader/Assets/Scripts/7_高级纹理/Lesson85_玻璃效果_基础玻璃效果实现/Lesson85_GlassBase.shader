Shader "Unlit/Lesson85_GlassBase"
{
     Properties
    {
        //主纹理
        _MainTex("MainTex", 2D) = ""{}
        //立方体纹理
        _Cube("Cubemap", Cube) = ""{}
        //折射程度 0~1 0代表完全反射（完全不折射）1代表完全折射（透明效果 相当于光全部进入了内部）
        _RefractAmount("RefractAmount", Range(0,1)) = 1
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
            samplerCUBE _Cube;
            float _RefractAmount;
            //GrabPass默认存储的纹理变量 这个是规则
            sampler2D _GrabTexture;

            struct v2f
            {
                float4 pos:SV_POSITION;//裁剪空间下的顶点坐标
                //用于存储从屏幕图像中采样的坐标（顶点相对于屏幕的位置）
                float4 grabPos:TEXCOORD0;
                //用于在颜色纹理中采样的UV坐标
                float2 uv:TEXCOORD1;
                //世界空间下的反射向量
                //我们将把反射向量的计算放在顶点着色器函数中 节约性能 表现效果也不会太差 肉眼几乎分辨不出来
                float3 worldRefl:TEXCOORD2;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //顶点坐标转换
                o.pos = UnityObjectToClipPos(v.vertex);
                //屏幕坐标转换相关的内容
                o.grabPos = ComputeGrabScreenPos(o.pos);
                //uv坐标计算相关的内容
                o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                //计算反射光向量
                //1.计算世界空间下法线向量
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.世界空间下的顶点坐标
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.计算视角方向 内部是用摄像机位置 - 世界坐标位置 
                fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                //4.计算反射向量
                o.worldRefl = reflect(-worldViewDir, worldNormal);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //反射颜色相关的计算 会叠加主纹理颜色
                //对颜色纹理进行采样
                fixed4 mainTex = tex2D(_MainTex, i.uv);
                //将反射颜色和主纹理颜色进行叠加
                fixed4 reflColor = texCUBE(_Cube, i.worldRefl) * mainTex;
                
                //折射相关的颜色
                //其实就是从我们抓取的 屏幕渲染纹理中进行采样 参与计算
                //抓取纹理中的颜色信息 相当于是这个玻璃对象后面的颜色

                //想要有折射效果 可以在采样之前 进行xy屏幕坐标的偏移
                float2 offset = 1 - _RefractAmount;
                //xy偏移一个位置
                i.grabPos.xy = i.grabPos.xy - offset/10; 

                //利用透视除法 将屏幕坐标转换到 0~1范围内 然后再进行采样
                fixed2 screenUV = i.grabPos.xy / i.grabPos.w;
                //从捕获的渲染纹理中进行采样 获取后面的颜色
                fixed4 grabColor = tex2D(_GrabTexture, screenUV);

                //折射程度 0~1 0代表完全反射（完全不折射）1代表完全折射（透明效果 相当于光全部进入了内部）
                float4 color = reflColor*(1 - _RefractAmount) + grabColor * _RefractAmount;

                return color;
            }

            ENDCG
        }
    }
}
