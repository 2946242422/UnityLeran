Shader "Unlit/Lesson77_RefractionBase"
{
    Properties
    {
        //����A������
        _RefractiveIndexA("RefractiveIndexA", Range(1,2)) = 1
        //����B������
        _RefractiveIndexB("RefractiveIndexB", Range(1,2)) = 1.3
        //������������ͼ
        _Cube("Cubemap", Cube) = ""{}
        //����̶�
        _RefracAmount("RefracAmount", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            samplerCUBE _Cube;
            fixed _RefractiveIndexA;
            fixed _RefractiveIndexB;
            fixed _RefracAmount;

            struct v2f
            {
                //�ü��ռ��¶�������
                float4 pos:SV_POSITION;
                //��������
                float3 worldRefr:TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //��������ת��
                o.pos = UnityObjectToClipPos(v.vertex);
                //����ת����
                fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //����ת����
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //�ӽǷ����ȡ ����� - ����λ��
                fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                //������������
                //����������һ���� ����A/����B�Ľ�� ���ǿ�������һ���������ⲿ��ô����� ������������������ֻ��Ϊ�˽���֪ʶ
                o.worldRefr = refract(-normalize(worldViewDir), normalize(worldNormal), _RefractiveIndexA/_RefractiveIndexB);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //�������������
                fixed4 cubemapColor = texCUBE(_Cube, i.worldRefr);
                //�������̶Ƚ��м��㷵��
                return cubemapColor * _RefracAmount;
            }

            ENDCG
        }
    }
}
