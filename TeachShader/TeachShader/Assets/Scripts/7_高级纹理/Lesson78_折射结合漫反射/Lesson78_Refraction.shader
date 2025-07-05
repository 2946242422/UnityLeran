Shader "Unlit/Lesson78_Refraction"
{
    Properties
    {
        //��������ɫ
        _Color("Color", Color) = (1,1,1,1)
        //������ɫ
        _RefractColor("RefractColor", Color) = (1,1,1,1)
        //�����ʱ�ֵ ����A������/����B������
        _RefractRatio("RefractRatio", Range(0.1, 1)) = 0.5
        //������������ͼ
        _Cube("Cubemap", Cube) = ""{}
        //����̶�
        _RefracAmount("RefracAmount", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        //Cull Off
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
            fixed4 _RefractColor;
            samplerCUBE _Cube;
            fixed _RefractRatio;
            fixed _RefracAmount;

            struct v2f
            {
                //�ü��ռ��¶�������
                float4 pos:SV_POSITION;
                 //����ռ��·���
                fixed3 worldNormal:NORMAL;
                //����ռ��µĶ���λ��
                float3 worldPos:TEXCOORD0;
                //��������
                float3 worldRefr:TEXCOORD1;
                //��Ӱ���
                SHADOW_COORDS(2)
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //��������ת��
                o.pos = UnityObjectToClipPos(v.vertex);
                //����ת����
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                //����ת����
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //�ӽǷ����ȡ ����� - ����λ��
                fixed3 worldViewDir = UnityWorldSpaceViewDir(o.worldPos);
                //������������
                //����������һ���� ����A/����B�Ľ�� ���ǿ�������һ���������ⲿ��ô����� ������������������ֻ��Ϊ�˽���֪ʶ
                o.worldRefr = refract(-normalize(worldViewDir), normalize(o.worldNormal), _RefractRatio);

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

                //�������������
                fixed3 cubemapColor = texCUBE(_Cube, i.worldRefr).rgb * _RefractColor.rgb;
                
                //�õ�����˥���Լ���Ӱ��ص�˥��ֵ
                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
                
                //��������lerp ����������ɫ�ͷ�����ɫ֮�� ���в�ֵ 0��1���Ǽ���״̬ 0 û������Ч�� 1ֻ������Ч�� 0~1֮��������ߵ���
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.rgb + lerp(diffuse, cubemapColor, _RefracAmount) * atten;

                //�������̶Ƚ��м��㷵��
                return fixed4(color, 1.0);
            }

            ENDCG
        }
    }
    FallBack "Reflective/VertexLit"
}
