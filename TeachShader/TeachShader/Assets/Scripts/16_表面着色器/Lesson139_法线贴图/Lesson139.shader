Shader "Custom/Lesson139"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("BumpMap", 2D) = ""{}

        //_Emission("Emission", Color) = (1,1,1,1)
        _Metallic("Metallic", Range(0,1)) = 0
        _Smoothness("Smoothness", Range(0,1)) = 0

        _Expansion("Expansion", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vertexFunc finalcolor:colorFunc

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _BumpMap;
        //fixed4 _Emission;
        fixed _Metallic;
        fixed _Smoothness;
        //顶点往外扩张多少
        float _Expansion;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            //漫反射颜色
            o.Albedo = texColor.rgb;
            //透明通道相关
            o.Alpha = texColor.a * _Color.a;
            //得到切线空间下的法线
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

            //o.Emission = _Emission.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }


        void vertexFunc(inout appdata_full v)
        {
            //修改顶点坐标 往外扩充
            v.vertex.xyz += v.normal * _Expansion;
        }

        void colorFunc(Input IN, SurfaceOutputStandard o, inout fixed4 color)
        {
            color *= _Color;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
