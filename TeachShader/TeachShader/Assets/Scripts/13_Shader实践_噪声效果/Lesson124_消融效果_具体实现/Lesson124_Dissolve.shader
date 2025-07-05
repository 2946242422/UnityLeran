Shader "Unlit/Lesson124_Dissolve"
{
     Properties
    {
        _MainColor("MainColor", Color) = (1,1,1,1)
        _MainTex("MainTex", 2D) = ""{}
        _BumpMap("BumpMap", 2D) = ""{}
        _BumpScale("BumpScale", Range(0,1)) = 1
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(8,256)) = 18

        //�������� ���ھ�������Ч����Χ
        _Noise("Noise", 2D) = ""{}
        //�������� ���ھ�����Ե��ɫ
        _Gradient("Gradient", 2D) = ""{}
        //���ڽ���
        _Dissolve("Dissolve", Range(0,1)) = 0
        //���ڱ߽��խ
        _EdgeRange("EdgeRange", Range(0,1)) = 0
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            CGPROGRAM
            #pragma multi_compile_fwdbase
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct v2f
            {
                float4 pos:SV_POSITION;
                //float2 uvTex:TEXCOORD0;
                //float2 uvBump:TEXCOORD1;
                //���ǿ��Ե�������������float2�ĳ�Ա���ڼ�¼ ��ɫ�ͷ��������uv����
                //Ҳ����ֱ������һ��float4�ĳ�Ա xy���ڼ�¼��ɫ�����uv��zw���ڼ�¼���������uv
                float4 uv:TEXCOORD0;
                //��������uv ����֮�����ƫ������
                float2 uvNoise:TEXCOORD1;
                //��ķ��� ��������߿ռ��µ�
                float3 lightDir:TEXCOORD2;
                //�ӽǵķ��� ��������߿ռ��µ�
                float3 viewDir:TEXCOORD3;
                float3 worldPos:TEXCOORD4;
                SHADOW_COORDS(5)
            };

            float4 _MainColor;//��������ɫ
            sampler2D _MainTex;//��ɫ����
            float4 _MainTex_ST;//��ɫ��������ź�ƽ��
            sampler2D _BumpMap;//��������
            float4 _BumpMap_ST;//������������ź�ƽ��
            float _BumpScale;//��͹�̶�
            float4 _SpecularColor;//�߹���ɫ
            fixed _SpecularNum;//�����

            sampler2D _Noise;//��������
            float4 _Noise_ST;//������������ź�ƽ��
            sampler2D _Gradient;//��������
            fixed _Dissolve;//���ڽ���
            fixed _EdgeRange;//���ڱ߽��խ��Χ

            v2f vert (appdata_full v)
            {
                v2f data;
                //��ģ�Ϳռ��µĶ���ת���ü��ռ���
                data.pos = UnityObjectToClipPos(v.vertex);
                //�������������ƫ��
                data.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                data.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                //�����������������ƫ��
                data.uvNoise = v.texcoord.xy * _Noise_ST.xy + _Noise_ST.zw;

                //�ڶ�����ɫ������ �õ� ģ�Ϳռ䵽���߿ռ�� ת������
                //���ߡ������ߡ�����
                //���㸱���� �����˽���� ��ֱ�����ߺͷ��ߵ����������� ͨ������ ���ߵ��е�w���Ϳ���ȷ������һ��
                float3 binormal = cross(normalize(v.tangent), normalize(v.normal)) * v.tangent.w;
                //ת������
                float3x3 rotation = float3x3( v.tangent.xyz,
                                              binormal,
                                              v.normal);
                //ģ�Ϳռ��µĹ�ķ���
                //data.lightDir = ObjSpaceLightDir(v.vertex);
                //����ģ�Ϳռ䵽���߿ռ��ת������ �Ϳ��Եõ����߿ռ��µ� ��ķ�����
                data.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex));

                //ģ�Ϳռ��µ��ӽǵķ���
                //data.viewDir = ObjSpaceViewDir(v.vertex);
                data.viewDir = mul(rotation, ObjSpaceViewDir(v.vertex));

                data.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                TRANSFER_SHADOW(data);

                return data;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //�޳���������
                fixed3 noiseColor = tex2D(_Noise, i.uvNoise.xy).rgb;
                clip(_Dissolve == 1 ? -1 : noiseColor.r - _Dissolve);

                //ͨ������������� ȡ������������ͼ���е�����
                float4 packedNormal = tex2D(_BumpMap, i.uv.zw);
                //������ȡ�����ķ������� ���������㲢�ҿ��ܻ���н�ѹ�������㣬���յõ����߿ռ��µķ�������
                float3 tangentNormal = UnpackNormal(packedNormal);
                //���԰�͹�̶ȵ�ϵ��
                tangentNormal.xy *= _BumpScale;
                tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));

                //�������������� ����ɫ����� ���ַ�����ģ�ͼ���

                //��ɫ�������������ɫ�� ����
                fixed3 albedo = tex2D(_MainTex, i.uv.xy) * _MainColor.rgb;
                //������
                fixed3 lambertColor = _LightColor0.rgb * albedo.rgb * max(0, dot(tangentNormal, normalize(i.lightDir)));
                //�������
                float3 halfA = normalize(normalize(i.viewDir) + normalize(i.lightDir));
                //�߹ⷴ��
                fixed3 specularColor = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0, dot(tangentNormal, halfA)), _SpecularNum);
                
                //ǿ�ȼ���
                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
                
                //���ַ�
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.rgb * albedo + lambertColor*atten + specularColor;

                //������Ҫ��ģ�ͱ�������ɫ �� ���ڱ�Ե����ɫ֮��ȥ����ѡ��
                //������ɫ�Ĳ���
                fixed value = 1 - smoothstep(0, _EdgeRange, noiseColor.r - _Dissolve);
                fixed3 gradientColor = tex2D(_Gradient, fixed2(value, 0.5)).rgb;
                //������ɫ
                //�����ڽ���Ϊ0ʱ һ��Ҫ����ɫʹ�����ǵ�ģ�ͱ�������ɫ
                fixed3 finalColor = lerp(color, gradientColor, value * step(0.000001, _Dissolve));

                return fixed4(finalColor.rgb, 1);
            }
            ENDCG
        }

        //��ע����Ҫ���ڽ�����ӰͶӰ ��Ҫ������������Ӱӳ�������
        Pass{
            Tags{"LightMode" = "ShadowCaster"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //  �ñ���ָ��ʱ����Unity���������ɶ����ɫ������
            //  ����֧�ֲ�ͬ���͵���Ӱ��SM��SSSM�ȵȣ�
            //  ����ȷ����ɫ���ܹ������п��ܵ���ӰͶ��ģʽ����ȷ��Ⱦ
            #pragma multi_compile_shadowcaster
            //  ���а����˹ؼ�����Ӱ������صĺ�
            #include "UnityCG.cginc"

            struct v2f{
                //��������uv ����֮�����ƫ������
                float2 uvNoise:TEXCOORD0;
                //���㵽ƬԪ��ɫ����ӰͶ��ṹ�����ݺ�
                //����궨����һЩ��׼�ĳ�Ա����
                //��Щ������������ӰͶ��·���д��ݶ������ݵ�ƬԪ��ɫ��
                //������Ҫ�ڽṹ����ʹ��
                V2F_SHADOW_CASTER;
            };

            sampler2D _Noise;//��������
            float4 _Noise_ST;//������������ź�ƽ��
            fixed _Dissolve;//���ڽ���

            v2f vert(appdata_base v)
            {
                v2f data;

                //�����������������ƫ��
                data.uvNoise = v.texcoord.xy * _Noise_ST.xy + _Noise_ST.zw;

                //ת����ӰͶ��������ƫ�ƺ�
                //�����ڶ�����ɫ���м���ʹ�����ӰͶ������ı���
                //��Ҫ����
                //2-2-1.������ռ�Ķ���λ��ת��Ϊ�ü��ռ��λ��
                //2-2-2.���Ƿ���ƫ�ƣ��Լ�����Ӱʧ�����⣬�������ڴ�������Ӱʱ
                //2-2-3.���ݶ����ͶӰ�ռ�λ�ã����ں�������Ӱ����
                //������Ҫ�ڶ�����ɫ����ʹ��
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(data);
                return data;
            }

            float4 frag(v2f i):SV_Target
            {
                //�޳���������
                fixed3 noiseColor = tex2D(_Noise, i.uvNoise.xy).rgb;
                clip(_Dissolve == 1 ? -1 : noiseColor.r - _Dissolve);
                //��ӰͶ��ƬԪ��
                //�����ֵд�뵽��Ӱӳ��������
                //������Ҫ��ƬԪ��ɫ����ʹ��
                SHADOW_CASTER_FRAGMENT(i);
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
