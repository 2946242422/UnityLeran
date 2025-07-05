Shader "Unlit/Lesson104_EdgeDetection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //��Ե�ߵ���ɫ
        _EdgeColor("EdgeColor", Color) = (0,0,0,0)
        //������ɫ�̶� 1Ϊ��ɫ 0Ϊԭʼ��ɫ
        _BackgroundExtent("BackgroundExtent", Range(0,1)) = 0
        //������ɫ
        _BackgroundColor("BackgroundColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                //���ڴ洢9������uv����ı���
                half2 uv[9] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            //Unity�������ر���
            half4 _MainTex_TexelSize;
            fixed4 _EdgeColor;
            fixed _BackgroundExtent;
            fixed4 _BackgroundColor;


            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //��ǰ�������������
                half2 uv = v.texcoord;
                //ȥ��9�����ص�uv������м���
                o.uv[0] = uv + _MainTex_TexelSize.xy * half2(-1, -1);
                o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0, -1);
                o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1, -1);
                o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1, 0);
                o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0, 0);
                o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1, 0);
                o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1, 1);
                o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0, 1);
                o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1, 1);

                return o;
            }

            //������ɫ�ĻҶ�ֵ
            fixed calcLuminance(fixed4 color)
            {
                return 0.2126*color.r + 0.7152*color.g + 0.0722*color.b;
            }

            //Sobel������صľ������
            half Sobel(v2f o)
            {
                //Sobel���Ӷ�Ӧ�����������
                half Gx[9] = {-1, -2, -1,
                               0,  0,  0,
                               1,  2,  1};
                half Gy[9] = {-1, 0, 1,
                              -2, 0, 2,
                              -1, 0, 1};
                half L;//�Ҷ�ֵ
                half edgeX = 0;//ˮƽ�����ݶ�ֵ
                half edgeY = 0;//��ֵ�����ݶ�ֵ
                for (int i = 0; i < 9; i++)
                {
                    //������ɫ�� ����Ҷ�ֵ ����¼����
                    L = calcLuminance(tex2D(_MainTex, o.uv[i]));
                    edgeX += L * Gx[i];
                    edgeY += L * Gy[i];
                }
                //���յ�һ�������ص��ݶ�ֵ
                //half G = abs(edgeX) + abs(edgeY);
                return abs(edgeX) + abs(edgeY);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //�������������Ӽ����ݶ�ֵ
                half edge = Sobel(i);
                //���ü���������ݶ�ֵ��ԭʼ��ɫ �ͱ�Ե����ɫ֮����в�ֵ
                fixed4 withEdgeColor = lerp(tex2D(_MainTex, i.uv[4]), _EdgeColor, edge);
                //��ɫ�����
                fixed4 onlyEdgeColor = lerp(_BackgroundColor, _EdgeColor, edge);
                //ͨ���̶ȱ��� ȥ���� �Ǵ�ɫ��� ���� ԭʼ��ɫ��� ������֮�� ���й���
                return lerp(withEdgeColor, onlyEdgeColor, _BackgroundExtent);
            }

            ENDCG
        }
    }

    Fallback Off
}
