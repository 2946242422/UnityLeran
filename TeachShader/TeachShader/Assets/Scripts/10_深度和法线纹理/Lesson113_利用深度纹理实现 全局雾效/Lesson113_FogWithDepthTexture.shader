Shader "Unlit/Lesson113_FogWithDepthTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FogColor("FogColor", Color) = (1,1,1,1)
        _FogDensity("FogDensity", Float) = 1
        _FogStart("FogStart", Float) = 0
        _FogEnd("FogEnd", Float) = 10
    } 
    SubShader
    {
        ZTest Always
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //�������UV
                float2 uv_depth : TEXCOORD1;
                //�������� ָ���ĸ��ǵķ������� �����ݵ�ƬԪʱ ���Զ����в�ֵ ���㣩
                float4 ray:TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            //���� �����жϷ�ת��ʹ��
            half4 _MainTex_TexelSize;
            //�������
            sampler2D _CameraDepthTexture;
            //����ص�����
            fixed4 _FogColor;
            fixed _FogDensity;
            float _FogStart;
            float _FogEnd;
            //������� ����洢�� 4����������
            //0-���� 1-���� 2-���� 3-����
            float4x4 _RayMatrix;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.uv_depth = v.texcoord;
                //������ɫ������ ÿһ�����㶼��ִ��һ��
                //������Ļ������˵ �ͻ�ִ��4�� ��Ϊ��4������ ��4���ǣ�
                //ͨ��uv�����ж� ��ǰ�Ķ���λ��
                int index = 0;
                if(v.texcoord.x < 0.5 && v.texcoord.y < 0.5)
                    index = 0;
                else if(v.texcoord.x > 0.5 && v.texcoord.y < 0.5)
                    index = 1;
                else if(v.texcoord.x > 0.5 && v.texcoord.y > 0.5)
                    index = 2;
                else
                    index = 3;
                //�ж� �Ƿ���Ҫ��������ת �����ת�� ��ȵ�uv�Ͷ�Ӧ������Ҫ�仯
                #if UNITY_UV_STARTS_AT_TOP
                if(_MainTex_TexelSize.y < 0)
                {
                    o.uv_depth.y = 1 - o.uv_depth.y;
                    index = 3 - index;
                }
                #endif

                //���ݶ����λ�� ����ʹ����һ����������
                o.ray = _RayMatrix[index];
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //�۲�ռ��� ���������ʵ�ʾ��루Z������
                float linearDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv_depth));
                //��������ռ��� ���ص�����
                float3 worldPos = _WorldSpaceCameraPos + linearDepth * i.ray;
                //����صļ���
                //������� 
                float f = (_FogEnd - worldPos.y)/(_FogEnd - _FogStart);
                //ȡ0~1֮�� ������ȡ��ֵ
                f = saturate(f * _FogDensity);
                //���ò�ֵ ��������ɫ֮������ں�
                fixed3 color = lerp(tex2D(_MainTex, i.uv).rgb, _FogColor.rgb, f);

                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
    Fallback Off
}
