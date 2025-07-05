Shader "Unlit/Lesson111_DepthNormalTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
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
            //深度+法线纹理
            sampler2D _CameraDepthNormalsTexture;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //直接采样 获取到的是裁剪空间下的法线和深度信息
                float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
                fixed depth;
                fixed3 normals;
                DecodeDepthNormal(depthNormal, depth, normals);
                //法线的-1到1之间的区间 变换到 0~1之间
                return fixed4(normals*0.5 + 0.5, 1);
            }
            ENDCG
        }
    }
    Fallback Off
}
