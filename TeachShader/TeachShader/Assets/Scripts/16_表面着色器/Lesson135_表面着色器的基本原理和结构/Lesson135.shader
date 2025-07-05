Shader "Custom/Lesson135"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("BumpMap", 2D) = "white"{}
        _BumpScale("BumpScale", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        //vertex:FunVertex finalcolor:FunFinalcolor 
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float _BumpScale;//��͹�̶�
        fixed4 _Color;

        struct Input
        {
            //1.uv���
            //����uv��������� �����ڴ��������꣬Ҳ������uv2��Ϊǰ׺
            float2 uv_MainTex;
            float2 uv_BumpMap;
            //2.�ӽǷ��򣨿������ڴ����Ե���յȣ�
            float3 viewDir;
            //3.��Ļ�ռ����꣨�������ڴ��������Ļ��Ч�ȣ�
            float4 screenPos;
            //4.����ռ��µ�λ��
            float3 worldPos;
            //5.����ռ��µķ��䷽��û���޸�o.Normalʱ
            //����޸��˱��淨�� o.Normal
            //�ڱ��溯���У���Ҫʹ�� WorldReflectionVector(IN, o.Normal) ���õ�����ռ��µķ��䷽��
            float3 worldRefl;
            //6.����ռ��µķ��߷���û���޸�o.Normalʱ
            //����޸��˱��淨�� o.Normal
            //�ڱ��溯���У���Ҫʹ�� WorldNormalVector(IN, o.Normal) ���õ�����ռ��µķ��߷���
            float3 worldNormal;
            //7.ʹ��COLOR���嶨���float4��������ʾ��ֵ����𶥵���ɫ
            float4 vertexColor:COLOR;
        };

        //1. void ���溯����(Input IN, inout SurfaceOutput o)
        //2. void ���溯����(Input IN, inout SurfaceOutputStandard o)
        //3. void ���溯����(Input IN, inout SurfaceOutputStandardSpecular o)
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb * _Color;
            o.Alpha = tex.a * _Color.a;
            
            //���԰�͹�̶ȵ�ϵ��
            float3 tangentNormal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            tangentNormal.xy *= _BumpScale;
            tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));
            o.Normal = tangentNormal;
        }

        void FunVertex(inout appdata_full v)
        {
            //�������߼�
            
        }

        void FunFinalcolor(Input IN, SurfaceOutput o, inout fixed4 color)
        {
            //�����մ��������ɫ �ٴν��ж��⴦��
        }

        //�������ӽǵĹ���ģ�ͣ�����������
        //half4 Lighting�Զ������� (SurfaceOutput s, half3 lightDir, half atten)
        //�����ӽǵĹ���ģ�ͣ�����߹ⷴ��
        //half4 Lighting�Զ������� (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        //Ȼ��ֻ��Ҫ�ڹ���ģ�ʹ���д �Զ������� ����
        //�ͻ��Զ����ú����е��߼��������������߼���
        half4 LightingMyLight (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        {

        }

        ENDCG
    }
    FallBack "Diffuse"
}
