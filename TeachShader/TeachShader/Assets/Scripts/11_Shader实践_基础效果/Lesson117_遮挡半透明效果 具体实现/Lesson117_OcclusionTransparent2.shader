Shader "Unlit/Lesson117_OcclusionTransparent2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //菲涅尔反射 垂直角度反射率
        _FresnelScale("FresnelScale", Range(0,2)) = 1
        //菲涅尔反射近似公式中的 N次方
        _FresnelN("FresnelN",Range(0,5)) = 5
        //自定义颜色
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            ZTest Greater
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                //主要用于进行菲尼尔公式计算
                //世界空间下 视角方向
                float3 worldViewDir:TEXCOORD0;
                //世界空间下 法线方向
                float3 worldNormal:TEXCOORD1;
            };

            //在该Pass当中不需要采样 所以不需要映射主纹理相关内容
            fixed _FresnelScale;
            fixed _FresnelN;
            fixed4 _Color;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //视角方向 法线
                o.worldViewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //菲涅尔反射公式
                fixed alpha = _FresnelScale + 
                             (1-_FresnelScale) * pow(1 - dot(normalize(i.worldViewDir), normalize(i.worldNormal)), _FresnelN);
                
                return fixed4(_Color.rgb, alpha);
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _Alpha;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
