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
            //���+��������
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
                //ֱ�Ӳ��� ��ȡ�����ǲü��ռ��µķ��ߺ������Ϣ
                float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
                fixed depth;
                fixed3 normals;
                DecodeDepthNormal(depthNormal, depth, normals);
                //���ߵ�-1��1֮������� �任�� 0~1֮��
                return fixed4(normals*0.5 + 0.5, 1);
            }
            ENDCG
        }
    }
    Fallback Off
}
