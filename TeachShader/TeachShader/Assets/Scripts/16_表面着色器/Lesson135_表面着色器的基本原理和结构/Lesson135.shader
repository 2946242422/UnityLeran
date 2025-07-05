Shader "Custom/Lesson135"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("BumpMap", 2D) = "white"{}
        _BumpScale("BumpScale", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        //vertex:FunVertex finalcolor:FunFinalcolor 
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float _BumpScale;//凹凸程度
        fixed4 _Color;

        struct Input
        {
            //1.uv相关
            //规则：uv纹理变量名 若存在次纹理坐标，也可以用uv2作为前缀
            float2 uv_MainTex;
            float2 uv_BumpMap;
            //2.视角方向（可以用于处理边缘光照等）
            float3 viewDir;
            //3.屏幕空间坐标（可以用于处理反射或屏幕特效等）
            float4 screenPos;
            //4.世界空间下的位置
            float3 worldPos;
            //5.世界空间下的反射方向，没有修改o.Normal时
            //如果修改了表面法线 o.Normal
            //在表面函数中，需要使用 WorldReflectionVector(IN, o.Normal) 来得到世界空间下的反射方向
            float3 worldRefl;
            //6.世界空间下的法线方向，没有修改o.Normal时
            //如果修改了表面法线 o.Normal
            //在表面函数中，需要使用 WorldNormalVector(IN, o.Normal) 来得到世界空间下的法线方向
            float3 worldNormal;
            //7.使用COLOR语义定义的float4变量，表示插值后的逐顶点颜色
            float4 vertexColor:COLOR;
        };

        //1. void 表面函数名(Input IN, inout SurfaceOutput o)
        //2. void 表面函数名(Input IN, inout SurfaceOutputStandard o)
        //3. void 表面函数名(Input IN, inout SurfaceOutputStandardSpecular o)
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb * _Color;
            o.Alpha = tex.a * _Color.a;
            
            //乘以凹凸程度的系数
            float3 tangentNormal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            tangentNormal.xy *= _BumpScale;
            tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
            o.Normal = tangentNormal;
        }

        void FunVertex(inout appdata_full v)
        {
            //处理顶点逻辑
            
        }

        void FunFinalcolor(Input IN, SurfaceOutput o, inout fixed4 color)
        {
            //对最终处理完的颜色 再次进行额外处理
        }

        //不依赖视角的光照模型，比如漫反射
        //half4 Lighting自定义名字 (SurfaceOutput s, half3 lightDir, half atten)
        //依赖视角的光照模型，比如高光反射
        //half4 Lighting自定义名字 (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        //然后只需要在光照模型处填写 自定义名字 即可
        //就会自动调用函数中的逻辑来处理光照相关逻辑了
        half4 LightingMyLight (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        {

        }

        ENDCG
    }
    FallBack "Diffuse"
}
