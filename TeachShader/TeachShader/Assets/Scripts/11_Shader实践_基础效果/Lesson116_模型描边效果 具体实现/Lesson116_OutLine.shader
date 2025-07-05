Shader "Unlit/Lesson116_OutLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutLineColor("OutLineColor", Color) = (1,1,1,1)
        _OutLineWidth("OutLineWidth", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }

        Pass
        {
            //�ر����д�� Ŀ���� �ڶ���Pass�ܹ������غϵĵط�
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            //��Ե����ɫ
            fixed4 _OutLineColor;
            //��Ե�ߴ�ϸ
            fixed _OutLineWidth;

            v2f vert (appdata_base v)
            {
                v2f o;
                //ƫ�ƶ���λ�� �����߷���ƫ��
                //�����ǵĶ��㳯���߷��� ƫ�� �Զ������λ ����Զ������ ���Ǿ���ģ�����Ͷ��ٵ� �Ϳ��Ծ�����Ե�ߵĴ�ϸ
                float3 newVertex = v.vertex + normalize(v.normal) * _OutLineWidth;
                //�����͹���Ķ���ת���ü��ռ�
                o.vertex = UnityObjectToClipPos(float4(newVertex, 1));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutLineColor;
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
            half4 _MainTex_ST;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
