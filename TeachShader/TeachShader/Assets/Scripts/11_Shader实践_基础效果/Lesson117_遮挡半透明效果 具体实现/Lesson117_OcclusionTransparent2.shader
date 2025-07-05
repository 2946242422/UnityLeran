Shader "Unlit/Lesson117_OcclusionTransparent2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //���������� ��ֱ�Ƕȷ�����
        _FresnelScale("FresnelScale", Range(0,2)) = 1
        //������������ƹ�ʽ�е� N�η�
        _FresnelN("FresnelN",Range(0,5)) = 5
        //�Զ�����ɫ
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
                //��Ҫ���ڽ��з������ʽ����
                //����ռ��� �ӽǷ���
                float3 worldViewDir:TEXCOORD0;
                //����ռ��� ���߷���
                float3 worldNormal:TEXCOORD1;
            };

            //�ڸ�Pass���в���Ҫ���� ���Բ���Ҫӳ���������������
            fixed _FresnelScale;
            fixed _FresnelN;
            fixed4 _Color;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //�ӽǷ��� ����
                o.worldViewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //���������乫ʽ
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
