Shader "Unlit/Lesson108_Bloom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //���ڴ洢��������ģ����Ľ��
        _Bloom("Bloom", 2D) = ""{}
        //������ֵ ������������ ���������
        _LuminanceThreshold("LuminanceThreshold", Float) = 0.5
        //ģ���뾶
        _BlurSize("BlurSize", Float) = 1
    }
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        sampler2D _Bloom;
        float _LuminanceThreshold;
        float _BlurSize;

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        //������ɫ������ֵ���Ҷ�ֵ��
        fixed luminance(fixed4 color)
        {
            return 0.2125*color.r + 0.7154*color.g + 0.0721*color.b;
        }

        ENDCG

        ZTest Always
        Cull Off
        ZWrite Off
        //��ȡ��Pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            v2f vert(appdata_base v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i):SV_Target{
                //����Դ������ɫ
                fixed4 color = tex2D(_MainTex, i.uv);
                //�õ����ȹ���ֵ
                fixed value = clamp(luminance(color) - _LuminanceThreshold, 0, 1);
                //������ɫ*���ȹ���ֵ
                return color * value;
            }

            ENDCG
        }

        //���ø�˹ģ����2��Pass
        UsePass "Unlit/Lesson106_GaussianBlur/GAUSSIAN_BLUR_HORIZONTAL"
        UsePass "Unlit/Lesson106_GaussianBlur/GAUSSIAN_BLUR_VERTICAL"

        //���ںϳɵ�Pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vertBloom
            #pragma fragment fragBloom

            struct v2fBloom
            {
                float4 pos:SV_POSITION;
                //xy��Ҫ���ڶ���������в���
                //zw��Ҫ���ڶ�����ģ������������
                half4 uv:TEXCOORD0;
            };

            v2fBloom vertBloom(appdata_base v)
            {
                v2fBloom o;

                o.pos = UnityObjectToClipPos(v.vertex);
                //��������������� Ҫ������ͬ�ĵط�������ɫ����
                o.uv.xy = v.texcoord;
                o.uv.zw = v.texcoord;

                //�ú�ȥ�ж�uv�����Ƿ񱻷�ת
                #if UNITY_UV_STARTS_AT_TOP
                //������ص�yС��0 Ϊ���� ��ʾ��Ҫ��Y����е���
                if(_MainTex_TexelSize.y < 0)
                    o.uv.w = 1 - o.uv.w;
                #endif

                return o;
            }

            fixed4 fragBloom(v2fBloom i):SV_TARGET
            {
                return tex2D(_MainTex, i.uv.xy) + tex2D(_Bloom, i.uv.zw);
            }

            ENDCG
        }
    }
    Fallback Off
}
