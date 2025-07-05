Shader "Unlit/Lesson121_Sketch"
{
    Properties
    {
        //整体颜色叠加
        _Color("Color", Color) = (1,1,1,1)
        //平铺纹理的系数
        _TileFactor("TileFactor", Float) = 1
        //6张素描纹理贴图
        _Sketch0("Sketch0", 2D) = ""{}
        _Sketch1("Sketch1", 2D) = ""{}
        _Sketch2("Sketch2", 2D) = ""{}
        _Sketch3("Sketch3", 2D) = ""{}
        _Sketch4("Sketch4", 2D) = ""{}
        _Sketch5("Sketch5", 2D) = ""{}
        //边缘线相关参数
        _OutLineColor("OutLineColor", Color) = (0,0,0,1)
        _OutLineWidth("OutLineWidth", Range(0,1)) = 0.04
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        UsePass "Unlit/Lesson120_ToonShader/OUTLINE"

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            fixed4 _Color;
            fixed _TileFactor;
            sampler2D _Sketch0;
            sampler2D _Sketch1;
            sampler2D _Sketch2;
            sampler2D _Sketch3;
            sampler2D _Sketch4;
            sampler2D _Sketch5;

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                //xyz分别代表第1、2、3张素描纹理的权重
                fixed3 sketchWeights0:TEXCOORD1;
                //xyz分别代表第4、5、6张素描纹理的权重
                fixed3 sketchWeights1:TEXCOORD2;
                float3 worldPos:TEXCOORD3;
                SHADOW_COORDS(4)
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                //顶点坐标转换
                o.vertex = UnityObjectToClipPos(v.vertex);
                //uv坐标平铺缩放 值越大 之后的素描细节越多 越小 细节越粗糙
                o.uv = v.texcoord.xy * _TileFactor;
                //世界空间光照方向
                fixed3 worldLightDir = normalize(WorldSpaceLightDir(v.vertex));
                //世界空间法线方向转换 
                fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //兰伯特漫反射光照强度 0~1 
                fixed diff = max(0, dot(worldLightDir, worldNormal));
                //将其扩充到0~7  7/7 = 1 
                //越大越亮 越小越暗
                diff = diff * 7.0;

                //初始化权重 默认每一张素描纹理对应的权重都是0
                o.sketchWeights0 = fixed3(0,0,0);
                o.sketchWeights1 = fixed3(0,0,0);

                //代表6张图的权重都会是0 那么采样就不会使用6张图的颜色
                if(diff > 6.0){
                    //认为是最亮的部分 我们不需要改变任何权重
                }
                else if(diff > 5.0){//代表 会从第1张图中采样 因此对应权重不会为0
                    o.sketchWeights0.x = diff - 5.0;
                }
                else if(diff > 4.0){//代表 会从第1、2张图中采样 因此对应权重不会为0
                    o.sketchWeights0.x = diff - 5.0;
                    o.sketchWeights0.y = 1 - o.sketchWeights0.x;
                }
                else if(diff > 3.0){//代表 会从第2、3张图中采样 因此对应权重不会为0
                    o.sketchWeights0.y = diff - 3.0;
                    o.sketchWeights0.z = 1 - o.sketchWeights0.y;
                }
                else if(diff > 2.0){//代表 会从第3、4张图中采样 因此对应权重不会为0
                    o.sketchWeights0.z = diff - 2.0;
                    o.sketchWeights1.x = 1 - o.sketchWeights0.z;
                }
                else if(diff > 1.0){//代表 会从第4、5张图中采样 因此对应权重不会为0
                    o.sketchWeights1.x = diff - 1.0;
                    o.sketchWeights1.y = 1 - o.sketchWeights1.x;
                }
                else{//代表 会从第5、6张图中采样 因此对应权重不会为0
                    o.sketchWeights1.y = diff;
                    o.sketchWeights1.z = 1 - diff;
                }

                //顶点坐标转世界坐标
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //如果在顶点着色器中计算的对应素描纹理图片的权重为0 那么这时 对应颜色会为(0,0,0,0)
                fixed4 sketchColor0 = tex2D(_Sketch0, i.uv) * i.sketchWeights0.x;
                fixed4 sketchColor1 = tex2D(_Sketch1, i.uv) * i.sketchWeights0.y;
                fixed4 sketchColor2 = tex2D(_Sketch2, i.uv) * i.sketchWeights0.z;
                fixed4 sketchColor3 = tex2D(_Sketch3, i.uv) * i.sketchWeights1.x;
                fixed4 sketchColor4 = tex2D(_Sketch4, i.uv) * i.sketchWeights1.y;
                fixed4 sketchColor5 = tex2D(_Sketch5, i.uv) * i.sketchWeights1.z;

                //最亮的部分相关的计算 白色叠加
                fixed4 whiteColor = fixed4(1,1,1,1) * (1 - i.sketchWeights0.x - i.sketchWeights0.y -i.sketchWeights0.z -
                                    i.sketchWeights1.x - i.sketchWeights1.y - i.sketchWeights1.z);

                fixed4 sketchClor = sketchColor0 + sketchColor1 + sketchColor2 + sketchColor3 + sketchColor4 +
                                    sketchColor5 + whiteColor;

                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

                return fixed4(sketchClor.rgb * atten * _Color.rgb, 1);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
