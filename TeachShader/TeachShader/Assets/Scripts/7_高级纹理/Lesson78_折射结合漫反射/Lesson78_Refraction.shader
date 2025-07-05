Shader "Unlit/Lesson78_Refraction"
{
    Properties
    {
        //漫反射颜色
        _Color("Color", Color) = (1,1,1,1)
        //折射颜色
        _RefractColor("RefractColor", Color) = (1,1,1,1)
        //折射率比值 介质A折射率/介质B折射率
        _RefractRatio("RefractRatio", Range(0.1, 1)) = 0.5
        //立方体纹理贴图
        _Cube("Cubemap", Cube) = ""{}
        //折射程度
        _RefracAmount("RefracAmount", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        //Cull Off
        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            fixed4 _Color;
            fixed4 _RefractColor;
            samplerCUBE _Cube;
            fixed _RefractRatio;
            fixed _RefracAmount;

            struct v2f
            {
                //裁剪空间下顶点坐标
                float4 pos:SV_POSITION;
                 //世界空间下法线
                fixed3 worldNormal:NORMAL;
                //世界空间下的顶点位置
                float3 worldPos:TEXCOORD0;
                //折射向量
                float3 worldRefr:TEXCOORD1;
                //阴影相关
                SHADOW_COORDS(2)
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //顶点坐标转换
                o.pos = UnityObjectToClipPos(v.vertex);
                //法线转世界
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                //顶点转世界
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //视角方向获取 摄像机 - 顶点位置
                fixed3 worldViewDir = UnityWorldSpaceViewDir(o.worldPos);
                //计算折射向量
                //第三个参数一定是 介质A/介质B的结果 我们可以声明一个变量在外部算好传进来 这里我们用两个变量只是为了讲解知识
                o.worldRefr = refract(-normalize(worldViewDir), normalize(o.worldNormal), _RefractRatio);

                //阴影相关处理
                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //漫反射光照相关计算
                //得到光的方向
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                //漫反射颜色
                fixed3 diffuse = _LightColor0.rgb * _Color.rgb * max(0, dot(normalize(i.worldNormal), worldLightDir));

                //立方体纹理采样
                fixed3 cubemapColor = texCUBE(_Cube, i.worldRefr).rgb * _RefractColor.rgb;
                
                //得到光照衰减以及阴影相关的衰减值
                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
                
                //我们利用lerp 在漫反射颜色和反射颜色之间 进行插值 0和1就是极限状态 0 没有折射效果 1只有折射效果 0~1之间就是两者叠加
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.rgb + lerp(diffuse, cubemapColor, _RefracAmount) * atten;

                //结合折射程度进行计算返回
                return fixed4(color, 1.0);
            }

            ENDCG
        }
    }
    FallBack "Reflective/VertexLit"
}
