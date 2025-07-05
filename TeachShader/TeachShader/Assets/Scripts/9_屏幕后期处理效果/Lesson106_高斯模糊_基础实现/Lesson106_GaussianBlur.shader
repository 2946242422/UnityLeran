Shader "Unlit/Lesson106_GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSpread("BlurSpread", Float) = 1
    }
    SubShader
    {
        //���ڰ������ô��� ��֮��Ķ��Pass���ж�����ʹ�õĴ���
        CGINCLUDE

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        //���� x=1/��  y=1/��
        half4 _MainTex_TexelSize;
        //����ƫ�Ƽ����λ
        float _BlurSpread;

        struct v2f
        {
            //5�����ص�uv����ƫ��
            half2 uv[5] : TEXCOORD0;
            //�����ڲü��ռ�������
            float4 vertex : SV_POSITION;
        };

        //ƬԪ��ɫ������
        //����Pass����ʹ��ͬһ�� ���ǰ�������߼�д��ͨ�ü���
        fixed4 fragBlur(v2f i):SV_Target
        {
            //�������
            //����� ���е������� ��Ϊֻ���������� û�б�Ҫ����Ϊ5����λ�ľ����
            float weight[3] = {0.4026, 0.2442, 0.0545};
            //�ȼ��㵱ǰ���ص�
            fixed3 sum = tex2D(_MainTex, i.uv[0]).rgb * weight[0];

            //ȥ��������ƫ��1����λ�� �� ����ƫ��������λ�� ��λ��� �ۼ�
            for (int it = 1; it < 3; it++)
            {
                //Ҫ����Ԫ�����
                sum += tex2D(_MainTex, i.uv[it*2 - 1]).rgb * weight[it];
                //����Ԫ�����
                sum += tex2D(_MainTex, i.uv[it*2]).rgb * weight[it];
            }

            return fixed4(sum, 1);
        }

        ENDCG

        Tags { "RenderType"="Opaque" }

        ZTest Always
        Cull Off
        ZWrite Off

        Pass
        {
            Name "GAUSSIAN_BLUR_HORIZONTAL"
            CGPROGRAM
            #pragma vertex vertBlurHorizontal
            #pragma fragment fragBlur

            //ˮƽ����� ������ɫ������
            v2f vertBlurHorizontal(appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                //5�����ص�uvƫ��
                half2 uv = v.texcoord;

                //ȥ����5������ ˮƽλ�õ�ƫ�ƻ�ȡ
                o.uv[0] = uv;
                o.uv[1] = uv + half2(_MainTex_TexelSize.x*1, 0)*_BlurSpread;
                o.uv[2] = uv - half2(_MainTex_TexelSize.x*1, 0)*_BlurSpread;
                o.uv[3] = uv + half2(_MainTex_TexelSize.x*2, 0)*_BlurSpread;
                o.uv[4] = uv - half2(_MainTex_TexelSize.x*2, 0)*_BlurSpread;

                return o;
            }
            ENDCG
        }

        Pass
        {
            Name "GAUSSIAN_BLUR_VERTICAL"
            CGPROGRAM
            #pragma vertex vertBlurVertical
            #pragma fragment fragBlur

            //��ֱ����� ������ɫ������
            v2f vertBlurVertical(appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                //5�����ص�uvƫ��
                half2 uv = v.texcoord;

                //ȥ����5������ ˮƽλ�õ�ƫ�ƻ�ȡ
                o.uv[0] = uv;
                o.uv[1] = uv + half2(0, _MainTex_TexelSize.x*1)*_BlurSpread;
                o.uv[2] = uv - half2(0, _MainTex_TexelSize.x*1)*_BlurSpread;
                o.uv[3] = uv + half2(0, _MainTex_TexelSize.x*2)*_BlurSpread;
                o.uv[4] = uv - half2(0, _MainTex_TexelSize.x*2)*_BlurSpread;

                return o;
            }
            ENDCG
        }
    }

    Fallback Off
}
