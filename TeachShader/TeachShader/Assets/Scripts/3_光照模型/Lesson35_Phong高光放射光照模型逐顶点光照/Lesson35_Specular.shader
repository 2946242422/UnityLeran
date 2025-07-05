Shader "Unlit/Lesson35_Specular"
{
     Properties
    {
        _SpecularColor("SpecularColor", Color ) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(0,20)) = 5
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct v2f
            {
                float4 pos:SV_POSITION;
                fixed3 color:COLOR;
            };

            fixed4 _SpecularColor;
            float _SpecularNum;

            v2f vert (appdata_base v)
            {
                v2f data;
                data.pos = UnityObjectToClipPos(v.vertex);


                float3 wPos = mul(UNITY_MATRIX_M, v.vertex);
                float3 viewDir = _WorldSpaceCameraPos.xyz - wPos;
                viewDir = normalize(viewDir);

                //2.��׼����ķ��䷽��
                //�õ��Ĺ�λ�õķ�������������ռ��µģ�
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //����������ռ��µ�����
                float3 normal = UnityObjectToWorldNormal(v.normal);
                //�����������
                float3 reflectDir = reflect(-lightDir, normal);

                //�߹ⷴ�������ɫ = ��Դ����ɫ * ���ʸ߹ⷴ����ɫ * max��0, ��׼����۲췽�������� ��׼����ķ��䷽����
                data.color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(viewDir, reflectDir)), _SpecularNum);

                return data;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color.rgb, 1);
            }
            ENDCG
        }
    }
}
