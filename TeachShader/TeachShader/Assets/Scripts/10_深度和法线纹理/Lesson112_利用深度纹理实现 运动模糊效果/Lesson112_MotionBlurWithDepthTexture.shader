Shader "Unlit/Lesson112_MotionBlurWithDepthTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //���ڿ���ģ���̶ȵ� ģ��ƫ����
        _BlurSize("BlurSize", Float) = 0.5
    }
    SubShader
    {
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
                float2 uv : TEXCOORD0;
                float2 uv_depth : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            fixed _BlurSize;
            sampler2D _CameraDepthTexture;//�������
            float4x4 _ClipToWorldMatrix;//�ü��ռ䵽����ռ�ı任����
            float4x4 _FrontWorldToClipMatrix;//��һ֡ ����ռ䵽�ü��ռ�ı任����

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.uv_depth = v.texcoord;
                //��ƽ̨ʱ��������ж�
                #if UNITY_UV_STARTS_AT_TOP
                    if (_MainTex_TexelSize.y < 0)
                        o.uv_depth.y = 1 - o.uv_depth.y;
                #endif
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //1.�õ��ü��ռ��µ�������
                //��ȡ���ֵ
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv_depth);
                //�ü��ռ��µ�һ��������� ��0~1��Χ�任��-1~1��Χ
                //��һ����
                float4 nowClipPos = float4(i.uv.x * 2 - 1, i.uv.y * 2 - 1, depth * 2 - 1, 1);
                //�òü��ռ䵽����ռ�ı任���� �õ� ����ռ��µĴ���
                float4 worldPos = mul(_ClipToWorldMatrix, nowClipPos);
                //͸�ӳ���
                worldPos /= worldPos.w;
                //������һ֡�ı任���� �õ���һ֡ ��Ӧ�Ĳü��ռ��µĵ�
                //�ڶ�����
                float4 oldClipPos = mul(_FrontWorldToClipMatrix, worldPos);
                //͸�ӳ���
                oldClipPos /= oldClipPos.w;

                //2.�õ��˶�����
                float2 moveDir = (nowClipPos.xy - oldClipPos.xy)/2;

                //3.����ģ������
                float2 uv = i.uv;
                float4 color = float4(0,0,0,0);
                for (int it = 0; it < 3; it++)
                {
                    //��һ�β����ۼӵ��ǵ�ǰ��������λ�õ���ɫ
                    //�ڶ��β����ۼӵ��ǵ�ǰ���ؽ����� moveDir * _BlurSize ƫ�ƺ����ɫ
                    //�ڶ��β����ۼӵ��ǵ�ǰ���ؽ����� 2*moveDir * _BlurSize ƫ�ƺ����ɫ
                    color += tex2D(_MainTex, uv);
                    uv += moveDir * _BlurSize;
                }
                //�������3�κ���ɫ��ƽ��ֵ �൱�ھ����ڽ���ģ��������
                color /= 3;
                //����ģ����������ɫ
                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
    Fallback Off
}
