Shader "Unlit/Lesson68_Attenuation"
{
   Properties
    {
        _MainColor("MainColor", Color) = (1,1,1,1)
        //�߹ⷴ����ɫ  �����
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(0, 20)) = 1
    }
    SubShader
    {
        //Bass Pass ������Ⱦͨ��
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //���ڰ������Ǳ������б��� ���ұ�֤˥����ع��ձ����ܹ���ȷ��ֵ����Ӧ�����ñ�����
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            //���ð����������ļ� �����м�����Ӱ���õ��������꣨��Ӱ�����ͣ�
            #include "AutoLight.cginc"

            //������������ɫ
            fixed4 _MainColor;
            fixed4 _SpecularColor;
            float _SpecularNum;

            //������ɫ�����س�ȥ������
            struct v2f
            {
                //�ü��ռ��µĶ���λ��
                float4 pos:SV_POSITION;
                //����ռ��µķ���λ��
                float3 wNormal:NORMAL;
                //����ռ��µ� �������� 
                float3 wPos:TEXCOORD0;
                //      �ú���v2f�ṹ�壨������ɫ������ֵ����ʹ��
                //      �����Ͼ���������һ�����ڶ���Ӱ������в���������
                //      ���ڲ�ʵ���Ͼ���������һ����Ϊ_ShadowCoord����Ӱ�����������
                //      ��Ҫע����ǣ�
                //      ��ʹ��ʱ SHADOW_COORDS(2) �������2
                //      ��ʾ��Ҫʱ��һ�����õĲ�ֵ�Ĵ���������ֵ   
                //��Ӱ����� ��Ҫ���ڴ洢��Ӱ��������
                SHADOW_COORDS(2)
            };

            //�õ������ع���ģ�ͼ������ɫ ����ƬԪ��
            fixed3 getLambertFColor(in float3 wNormal)
            {
                //�õ���Դ��λ����
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //������������ع��յ���������ɫ
                fixed3 color = _LightColor0.rgb * _MainColor.rgb * max(0, dot(wNormal, lightDir));

                return color;
            }

            //�õ�Blinn Phongʽ�߹ⷴ��ģ�ͼ������ɫ����ƬԪ��
            fixed3 getSpecularColor(in float3 wPos, in float3 wNormal)
            {
                //1.�ӽǵ�λ����
                //float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - wPos );
                float3 viewDir = normalize(UnityWorldSpaceViewDir(wPos));

                //2.��ķ��䵥λ����
                //��ķ���
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                //��Ƿ�������
                float3 halfA = normalize(viewDir + lightDir);
                
                //color = ��Դ��ɫ * ���ʸ߹ⷴ����ɫ * pow( max(0, dot(�ӽǵ�λ����, ��ķ��䵥λ����)), ����� )
                fixed3 color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(wNormal, halfA)), _SpecularNum );

                return color;
            }

            v2f vert (appdata_base v)
            {
                v2f v2fData;
                //ת��ģ�Ϳռ��µĶ��㵽�ü��ռ���
                v2fData.pos = UnityObjectToClipPos(v.vertex);
                //ת��ģ�Ϳռ��µķ��ߵ�����ռ���
                v2fData.wNormal = UnityObjectToWorldNormal(v.normal);
                //����ת������ռ�
                v2fData.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                //      �ú��ڶ�����ɫ�������е��ã������Ӧ��v2f�ṹ�����
                //      �ú�����ڲ��Լ��ж�Ӧ��ʹ��������Ӱӳ�似����SM��SSSM��
                //      ���յ�Ŀ�ľ��ǽ������������ת�����洢��_ShadowCoord��Ӱ�������������
                //      ��Ҫע����ǣ�
                //      1.�ú�����ڲ�ʹ�ö�����ɫ���д���Ľṹ��
                //        �ýṹ���ж��������������vertex
                //      2.�ú�����ڲ�ʹ�ö�����ɫ���ķ��ؽṹ��
                //        ���еĶ���λ������������pos
                //������Ӱӳ���������� �������ڲ�ȥ���м��� Ȼ������� v2f�е�SHADOW_COORDS��
                TRANSFER_SHADOW(v2fData);

                return v2fData;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //���������ع�����ɫ
                fixed3 lambertColor = getLambertFColor(normalize(i.wNormal));
                //����BlinnPhongʽ�߹ⷴ����ɫ
                fixed3 specularColor = getSpecularColor(i.wPos, normalize(i.wNormal));

                //�õ���Ӱ˥��ֵ
                //      �ú���ƬԪ��ɫ���е��ã������Ӧ��v2f�ṹ�����
                //      �ú�����ڲ�����v2f�е� ��Ӱ�����������(ShadowCoord)�����������в���
                //      �������õ������ֵ���бȽϣ��Լ����һ��fixed3����Ӱ˥��ֵ
                //      ����ֻ��Ҫʹ�������صĽ���� (������+�߹ⷴ��) �Ľ����˼���
                //fixed3 shadow = SHADOW_ATTENUATION(i);

                //˥��ֵ
                //fixed atten = 1;

                //���õƹ�˥������Ӱ˥�������ͳһ����˥��ֵ�ļ���
                UNITY_LIGHT_ATTENUATION(atten, i, i.wPos)

                //������������ɫ = ��������ɫ + �����ع���ģ��������ɫ + Phongʽ�߹ⷴ�����ģ��������ɫ
                //˥��ֵ ��� ��������ɫ + �߹ⷴ����ɫ �� �ٽ��г˷�����
                fixed3 blinnPhongColor = UNITY_LIGHTMODEL_AMBIENT.rgb + (lambertColor + specularColor)*atten; 

                return fixed4(blinnPhongColor.rgb, 1);
            }
            ENDCG
        }

        //Additional Pass ������Ⱦͨ��
        Pass
        {
            Tags { "LightMode"="ForwardAdd" }
            //���Լ�����Ч�� ���� ������ɫ���
            Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //���ڰ������Ǳ������б��� ���ұ�֤˥����ع��ձ����ܹ���ȷ��ֵ����Ӧ�����ñ�����
            //#pragma multi_compile_fwdadd
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            //������������ɫ
            fixed4 _MainColor;
            fixed4 _SpecularColor;
            float _SpecularNum;

            //������ɫ�����س�ȥ������
            struct v2f
            {
                //�ü��ռ��µĶ���λ��
                float4 pos:SV_POSITION;
                //����ռ��µķ���λ��
                float3 wNormal:NORMAL;
                //����ռ��µ� �������� 
                float3 wPos:TEXCOORD0;
                //      �ú���v2f�ṹ�壨������ɫ������ֵ����ʹ��
                //      �����Ͼ���������һ�����ڶ���Ӱ������в���������
                //      ���ڲ�ʵ���Ͼ���������һ����Ϊ_ShadowCoord����Ӱ�����������
                //      ��Ҫע����ǣ�
                //      ��ʹ��ʱ SHADOW_COORDS(2) �������2
                //      ��ʾ��Ҫʱ��һ�����õĲ�ֵ�Ĵ���������ֵ   
                //��Ӱ����� ��Ҫ���ڴ洢��Ӱ��������
                SHADOW_COORDS(2)
            };

            v2f vert (appdata_base v)
            {
                v2f v2fData;
                //ת��ģ�Ϳռ��µĶ��㵽�ü��ռ���
                v2fData.pos = UnityObjectToClipPos(v.vertex);
                //ת��ģ�Ϳռ��µķ��ߵ�����ռ���
                v2fData.wNormal = UnityObjectToWorldNormal(v.normal);
                //����ת������ռ�
                v2fData.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                //      �ú��ڶ�����ɫ�������е��ã������Ӧ��v2f�ṹ�����
                //      �ú�����ڲ��Լ��ж�Ӧ��ʹ��������Ӱӳ�似����SM��SSSM��
                //      ���յ�Ŀ�ľ��ǽ������������ת�����洢��_ShadowCoord��Ӱ�������������
                //      ��Ҫע����ǣ�
                //      1.�ú�����ڲ�ʹ�ö�����ɫ���д���Ľṹ��
                //        �ýṹ���ж��������������vertex
                //      2.�ú�����ڲ�ʹ�ö�����ɫ���ķ��ؽṹ��
                //        ���еĶ���λ������������pos
                //������Ӱӳ���������� �������ڲ�ȥ���м��� Ȼ������� v2f�е�SHADOW_COORDS��
                TRANSFER_SHADOW(v2fData);

                return v2fData;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //������������
                fixed3 worldNormal = normalize(i.wNormal);
                //ƽ�й� ��ķ��� ��ʵ��������λ��
                #if defined(_DIRECTIONAL_LIGHT)
                    fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                #else //���Դ�;۹�� ��ķ��� �� ���λ�� - ����λ��
                    fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz - i.wPos);
                #endif
                // ��������ɫ = ����ɫ * ��������ɫ * max(0, dot(��������ϵ�µķ���, ��������ϵ�µĹⷽ��));
                fixed3 diffuse = _LightColor0.rgb * _MainColor.rgb * max(0, dot(worldNormal, worldLightDir));
                
                //BlinnPhong�߹ⷴ��
                //�ӽǷ���
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.wPos.xyz);
                //��Ƿ�������
                fixed3 halfDir = normalize(worldLightDir + viewDir);
                // �߹���ɫ = ����ɫ * �����еĸ߹���ɫ * pow(max(0, dot(��������ϵ����, ��������ϵ�������)), �����);
                fixed3 specular = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0, dot(worldNormal, halfDir)), _SpecularNum);

                //˥��ֵ
                //#if defined(_DIRECTIONAL_LIGHT)
                //    fixed atten = 1;
                //#elif defined(_POINT_LIGHT)
                //    //����������ϵ�¶���ת����Դ�ռ���
                //    float3 lightCoord = mul(unity_WorldToLight, float4(i.wPos, 1)).xyz;
                //    //�����������õ������ƽ�� Ȼ�����ٹ�Դ����������õ�˥��ֵ
                //    fixed atten = tex2D(_LightTexture0, dot(lightCoord,lightCoord).xx).UNITY_ATTEN_CHANNEL;
                //#elif defined(_SPOT_LIGHT)
                //    //����������ϵ�¶���ת����Դ�ռ��� �۹����Ҫ��w�����������
                //    float4 lightCoord = mul(unity_WorldToLight, float4(i.wPos, 1));
                //    fixed atten = (lightCoord.z > 0) * //�ж��ھ۹��ǰ����
                //                  tex2D(_LightTexture0, lightCoord.xy / lightCoord.w + 0.5).w * //ӳ�䵽��ͼ�н��в���
                //                  tex2D(_LightTextureB0, dot(lightCoord,lightCoord).xx).UNITY_ATTEN_CHANNEL; //�����ƽ������
                //#else
                //    fixed atten = 1;
                //#endif

                //���õƹ�˥������Ӱ˥�������ͳһ����˥��ֵ�ļ���
                UNITY_LIGHT_ATTENUATION(atten, i, i.wPos)

                //�ڸ�����Ⱦͨ���в���Ҫ�ڼ��ϻ�������ɫ�� ��Ϊ��ֻ��Ҫ����һ�� �ڻ�����Ⱦͨ�����Ѿ�������
                return fixed4((diffuse + specular)*atten, 1);
            }
            ENDCG
        }
    }

    FallBack "Specular"
}
