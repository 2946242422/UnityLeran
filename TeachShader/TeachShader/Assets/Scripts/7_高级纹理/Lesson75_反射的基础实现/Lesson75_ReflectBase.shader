Shader "Unlit/Lesson75_ReflectBase"
{
    Properties
    {
        //����������
        _Cube("Cubemap", Cube) = ""{}
        //������
        _Reflectivity("Reflectivity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags{"RenderType"="Opaque" "Queue"="Geometry"}

        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            samplerCUBE _Cube;
            float _Reflectivity;

            struct v2f
            {
                float4 pos:SV_POSITION;//�ü��ռ��µĶ�������
                //����ռ��µķ�������
                //���ǽ��ѷ��������ļ�����ڶ�����ɫ�������� ��Լ���� ����Ч��Ҳ����̫�� ���ۼ����ֱ治����
                float3 worldRefl:TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //��������ת��
                o.pos = UnityObjectToClipPos(v.vertex);
                //���㷴�������
                //1.��������ռ��·�������
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.����ռ��µĶ�������
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.�����ӽǷ��� �ڲ����������λ�� - ��������λ�� 
                fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                //4.���㷴������
                o.worldRefl = reflect(-worldViewDir, worldNormal);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //���������������ö�Ӧ�ķ����������в���
                fixed4 cubemapColor = texCUBE(_Cube, i.worldRefl);
                //�ò�����ɫ * ������ �������յ���ɫЧ��
                return cubemapColor * _Reflectivity;
            }

            ENDCG
        }
    }
}
