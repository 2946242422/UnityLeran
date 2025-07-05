Shader "Unlit/Lesson129"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            //�ñ���ָ�������Ĺؼ��֣�ֻ�йؼ�������ʱ�Ż����ɶ�ӦShader����
            //#pragma shader_feature _TEST_KEYWORD1 _TEST_KEYWORD2

            //�ñ���ָ�������Ĺؼ��֣������Ƿ����ö������ɶ�ӦShader����
            #pragma multi_compile _TEST_KEYWORD1 _TEST_KEYWORD2

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4(1,1,1,1);

                //�������
                #if defined(_TEST_KEYWORD1)
                col = tex2D(_MainTex, i.uv);
                #endif

                //��������ش���
                #if defined(_TEST_KEYWORD2)
                UNITY_APPLY_FOG(i.fogCoord, col);
                #endif

                return col;
            }
            ENDCG
        }
    }
}
