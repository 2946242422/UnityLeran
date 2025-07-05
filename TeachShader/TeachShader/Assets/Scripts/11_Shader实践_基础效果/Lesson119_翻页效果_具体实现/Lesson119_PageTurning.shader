Shader "Unlit/Lesson119_PageTurning"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BackTex("BackTex", 2D) = ""{}
        _AngleProgress("_AngleProgress", Range(0,180)) = 0
        _WeightX("WeightX", Range(0,1)) = 0
        _WeightY("WeightY", Range(0,1)) = 0
        _WaveLength("WaveLength", Range(0,3))= 0
        _MoveDis("MoveDis", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            //��������
            sampler2D _MainTex;
            //��������
            sampler2D _BackTex;
            //��ҳ���� 0~180�ȵĽǶ�
            float _AngleProgress;
            //x������Ȩ��
            fixed _WeightX;
            //Y������Ȩ��
            fixed _WeightY;
            //����
            float _WaveLength;
            //ƽ�ƾ���
            float _MoveDis;

            v2f vert (appdata_base v)
            {
                v2f o;
                //�Զ�����б任
                //�ȴ�����ת
                //����sincos���� ����Զ������ ��ҳ�ĽǶȣ����ȣ����õ���Ӧ
                //��sin��cosֵ
                float s;
                float c;
                sincos(radians(_AngleProgress), s, c);
                //����Z����ת����
                float4x4 rotationM = {c, -s, 0, 0,
                                      s, c, 0, 0,
                                      0, 0, 1, 0,
                                      0, 0, 0, 1};
                //���������ǻ���Z����ת �������ǽ��䰴��x�᷽�����ƫ��
                v.vertex += float4(_MoveDis, 0, 0, 0);

                //�����������
                float weight = 1 - abs(90 - _AngleProgress)/90;
                //Y���������
                v.vertex.y += sin(v.vertex.x * _WaveLength) * weight * _WeightY;
                //X������
                v.vertex.x -= v.vertex.x * weight * _WeightX;

                //ƽ�ƺ� ����ת
                float4 postion = mul(rotationM, v.vertex);
                //��ƽ�ƻ���
                postion -= float4(_MoveDis,0,0,0);

                o.vertex = UnityObjectToClipPos(postion);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i, fixed face:VFACE) : SV_Target
            {
                fixed4 col = face > 0 ? tex2D(_MainTex, i.uv) : tex2D(_BackTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
