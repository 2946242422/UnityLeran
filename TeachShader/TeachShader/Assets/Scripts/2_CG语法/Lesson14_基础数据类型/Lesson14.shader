Shader "Unlit/Lesson14"
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
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
#pragma exclude_renderers gles
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct test
            {
                int i;
                float f;
                bool b;
            };

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

                uint ui = 12;
                int i = -3;

                float f = 1.2f;
                half h = 1.2h;
                fixed fix = 3.2;

                bool b = true;
                string str = "123";

                sampler sam;

                float arrayf[4] = {2,3,4,5};
                //int l = arrayf.length;
                
                float arrayff[3][3] = { {1,2,3},{4,5,6},{7,8,9} };
                //arrayff.length; ÐÐ
                //arrayff[0].length; ÁÐ

                test t;
                t.i = 1;
                t.f = 1.2f;
                t.b = false;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
