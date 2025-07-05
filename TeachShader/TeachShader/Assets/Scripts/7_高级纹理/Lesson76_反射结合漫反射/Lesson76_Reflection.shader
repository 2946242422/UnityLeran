Shader "Unlit/Lesson76_Reflection"
{
    Properties
    {
        //��������ɫ
        _Color("Color", Color) = (1,1,1,1)
        //������ɫ
        _ReflectColor("ReflectColor", Color) = (1,1,1,1)
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
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            fixed4 _Color;
            fixed4 _ReflectColor;
            samplerCUBE _Cube;
            float _Reflectivity;

            struct v2f
            {
                float4 pos:SV_POSITION;//�ü��ռ��µĶ�������
                //����ռ��·���
                fixed3 worldNormal:NORMAL;
                //����ռ��µĶ���λ��
                float3 worldPos:TEXCOORD0;
                //����ռ��µķ�������
                //���ǽ��ѷ��������ļ�����ڶ�����ɫ�������� ��Լ���� ����Ч��Ҳ����̫�� ���ۼ����ֱ治����
                float3 worldRefl:TEXCOORD1;
                //��Ӱ���
                SHADOW_COORDS(2)
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
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.�����ӽǷ��� �ڲ����������λ�� - ��������λ�� 
                fixed3 worldViewDir = UnityWorldSpaceViewDir(o.worldPos);
                //4.���㷴������
                o.worldRefl = reflect(-worldViewDir, o.worldNormal);

                //��Ӱ��ش���
                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //�����������ؼ���
                //�õ���ķ���
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                //��������ɫ
                fixed3 diffuse = _LightColor0.rgb * _Color.rgb * max(0, dot(normalize(i.worldNormal), worldLightDir));

                //���������������ö�Ӧ�ķ����������в���
                fixed3 cubemapColor = texCUBE(_Cube, i.worldRefl).rgb * _ReflectColor.rgb;

                //�õ�����˥���Լ���Ӱ��ص�˥��ֵ
                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

                //��������lerp ����������ɫ�ͷ�����ɫ֮�� ���в�ֵ 0��1���Ǽ���״̬ 0 û�з���Ч�� 1ֻ�з���Ч�� 0~1֮��������ߵ���
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.rgb + lerp(diffuse, cubemapColor, _Reflectivity) * atten;

                //�ò�����ɫ * ������ �������յ���ɫЧ��
                return fixed4(color, 1.0);
            }

            ENDCG
        }
    }
    FallBack "Reflective/VertexLit"
}
