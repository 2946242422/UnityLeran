Shader "Unlit/Lesson79_FresnelBase"
{
    Properties
    {
        //����������
        _Cube("Cubemap", Cube) = ""{}
        //������������ ��Ӧ���ʵķ�����
        _FresnelScale("FresnelScale", Range(0,1)) = 1
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
            float _FresnelScale;

            struct v2f
            {
                float4 pos:SV_POSITION;//�ü��ռ��µĶ�������
                //����ռ��µķ���
                float3 worldNormal:NORMAL;
                //����ռ����ӽǵķ���
                float3 worldViewDir:TEXCOORD0;
                //����ռ��µķ�������
                //���ǽ��ѷ��������ļ�����ڶ�����ɫ�������� ��Լ���� ����Ч��Ҳ����̫�� ���ۼ����ֱ治����
                float3 worldRefl:TEXCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //��������ת��
                o.pos = UnityObjectToClipPos(v.vertex);
                //���㷴�������
                //1.��������ռ��·�������
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.����ռ��µĶ�������
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.�����ӽǷ��� �ڲ����������λ�� - ��������λ�� 
                o.worldViewDir = UnityWorldSpaceViewDir(worldPos);
                //4.���㷴������
                o.worldRefl = reflect(-o.worldViewDir, o.worldNormal);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //���������������ö�Ӧ�ķ����������в���
                fixed4 cubemapColor = texCUBE(_Cube, i.worldRefl);

                //����schlick���������Ƶ�ʽ ���������������
                fixed fresnel = _FresnelScale + (1-_FresnelScale) * pow(1-dot(normalize(i.worldViewDir), normalize(i.worldNormal)), 5);

                //�ò�����ɫ * ������ �������յ���ɫЧ��
                return cubemapColor * fresnel;
            }

            ENDCG
        }
    }
}
