Shader "Unlit/Lesson92_SequentialFrameAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //ͼ������
        _Rows("Rows", int) = 8
        _Columns("Columns", int) = 8
        //�л������ٶȱ���
        _Speed("Speed", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Rows;
            float _Columns;
            float _Speed;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //�õ���ǰ֡ ����ʱ���������
                float frameIndex = floor(_Time.y * _Speed) % (_Rows * _Columns);
                //С���ӣ�СͼƬ������ʱ����ʼλ�ü���
                //���Զ�Ӧ���к��� Ŀ���ǽ�����ֵ ת���� 0~1�����귶Χ��
                //1 - (floor(frameIndex / _Columns) + 1)/_Rows
                //  +1 ����Ϊ�Ѹ������Ͻ�ת��Ϊ�������½�
                //  1- ��ΪUV�������ʱ�����½ǽ��в�����
                float2 frameUV = float2(frameIndex % _Columns / _Columns, 1 - (floor(frameIndex / _Columns) + 1)/_Rows);
                //�õ�uv���ű��� �൱�ڴ�0~1��ͼ ���䵽һ�� 0~1/n��һ��Сͼ��
                float2 size = float2(1/_Columns, 1/_Rows);
                //�������յ�uv����������Ϣ
                //*size �൱�ڰ�0~1��Χ ���ŵ��� 0~1/8��Χ
                //+frameUV �൱�ڰ���ʼ�Ĳ���λ�� �ƶ����� ��Ӧ֡С���ӵ���ʼλ��
                float2 uv = i.uv * size + frameUV;
                //���ղ�����ɫ
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
