Shader "Custom/DynamicLiquid"
{
    Properties
    {
        //Һ����ɫ
        _Color("Color", Color) = (1,1,1,1)
        //�߹���ɫ�͹⻬�Ȳ���
        _Specular("Specular", Color) = (0,0,0,0)
        //Һ��ĸ߶�
        _Height("Height", Float) = 0

        //���Ʊ仯�ٶ�
        _Speed("Speed", Float) = 1
        //��������
        _WaveAmplitude("WaveAmplitude", Float) = 1
        //����Ƶ��
        _WaveFrequency("WaveFrequency", Float) = 1
        //�����ĵ���
        _InvWaveLength("InvWaveLength", Float) = 1
    }
    SubShader
    {
        //͸��������
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend DstColor SrcColor
        ZWrite Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf StandardSpecular noshadow 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        fixed4 _Color;
        fixed4 _Specular;
        float _Height;
        float _Speed;
        float _WaveAmplitude;
        float _WaveFrequency;
        float _InvWaveLength;

        struct Input
        {
            //����ռ��µ����ص��λ��
            float3 worldPos;
        };

       
        void surf (Input IN, inout SurfaceOutputStandardSpecular o)
        {
            //��ģ�Ϳռ������ĵ�ת��������ռ���
            float3 centerPoint = mul(unity_ObjectToWorld, float4(0,0,0,1));
            //��ǰ���ص�����ĵ�ĸ߶Ȳ�
            float liquidHeight = centerPoint.y - IN.worldPos.y + _Height* 0.01;

            //����ƫ�Ƽ���
            float waveOffset = sin(_Time.y * _WaveFrequency + IN.worldPos.x * _InvWaveLength) * _WaveAmplitude;
            liquidHeight += waveOffset;

            //���liquidHeight >= 0 �򷵻�1 ���С��0 �򷵻�0
            //�����0 ��ϣ�����޳� �����޳�
            liquidHeight = step(0, liquidHeight);
            //���liquidHeight��0 - 0.001 �϶�С��0 �ͻᱻ�޳� ���ᱻ��Ⱦ
            clip(liquidHeight - 0.001);


            //��������ɫ
            o.Albedo = _Color.rgb;
            //�߹���ɫ
            o.Specular = _Specular.rgb;
            //�⻬������
            o.Smoothness = _Specular.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
