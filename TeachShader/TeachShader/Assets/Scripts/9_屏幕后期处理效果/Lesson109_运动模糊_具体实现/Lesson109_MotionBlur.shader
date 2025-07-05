Shader "Unlit/Lesson109_MotionBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //ģ���̶ȱ���
        _BlurAmount("BlurAmount", Float) = 0.5
    }
    SubShader
    {
        CGINCLUDE

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        fixed _BlurAmount;

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        v2f vert (appdata_base v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }

        ENDCG

        //��Ļ����Ч������
        ZTest Always
        Cull Off
        ZWrite Off

        //��һ��Pass ���ڻ��RGBͨ��
        Pass
        {
            //��(Դ��ɫ * _BlurAmount) + (Ŀ����ɫ * (1 - _BlurAmount))��
            Blend SrcAlpha OneMinusSrcAlpha
            //��ֻ�ı���ɫ�������е�RGBͨ����
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragRGB
            
            fixed4 fragRGB (v2f i) : SV_Target
            {
                return fixed4(tex2D(_MainTex, i.uv).rgb, _BlurAmount);
            }
            ENDCG
        }

        Pass
        {
            //��������ɫ = (Դ��ɫ * 1) + (Ŀ����ɫ * 0)��
            Blend One Zero
            //��ֻ�ı���ɫ�������е�Aͨ����
            ColorMask A

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragA
            
            fixed4 fragA (v2f i) : SV_Target
            {
                return fixed4(tex2D(_MainTex, i.uv));
            }
            ENDCG
        }
    }
    Fallback Off
}
