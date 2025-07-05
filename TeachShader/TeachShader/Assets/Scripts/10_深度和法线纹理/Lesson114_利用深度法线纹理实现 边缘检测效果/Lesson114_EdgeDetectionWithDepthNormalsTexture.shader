Shader "Unlit/Lesson114_EdgeDetectionWithDepthNormalsTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //���ڿ����Զ��屳����ɫ�̶ȵ� 0Ҫ��ʾԭʼ����ɫ 1ֻ��ʾ��Ե ��ȫ��ʾ�Զ��屳��ɫ
        _EdgeOnly("EdgeOnly", Float) = 0
        //��Ե�������ɫ
        _EdgeColor("EdgeColor", Color) = (0,0,0,0)
        //�Զ��屳����ɫ
        _BackgroundColor("BackgroundColor", Color) = (1,1,1,1)
        //����ƫ�Ƴ̶� ��Ҫ����������ߵĴ�ϸ ֵԽ��Խ�� ��֮Խϸ
        _SampleDistance("SampleDistance", Float) = 1
        //��Ⱥͷ��ߵ����ж� �������������ֵ�ж�ʱ ������
        _SensitivityDepth("SensitivityDepth", Float) = 1
        _SensitivityNormal("SensitivityNormal", Float) = 1
    }
    SubShader
    {
        //��Ļ�������
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
                //���ڴ洢5�����ص�uv����
                half2 uv[5] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            //���� ���ڽ���uv����ƫ�� ȡ����Χ���ص�uv�����
            half4 _MainTex_TexelSize;
            //���+��������
            sampler2D _CameraDepthNormalsTexture;\
            //���ڿ����Զ��屳����ɫ�̶ȵ� 0Ҫ��ʾԭʼ����ɫ 1ֻ��ʾ��Ե ��ȫ��ʾ�Զ��屳��ɫ
            fixed _EdgeOnly;
            //��Ե�������ɫ
            fixed4 _EdgeColor;
            //�Զ��屳����ɫ
            fixed4 _BackgroundColor;
            //����ƫ�Ƴ̶� ��Ҫ����������ߵĴ�ϸ ֵԽ��Խ�� ��֮Խϸ
            float _SampleDistance;
            //��Ⱥͷ��ߵ����ж� �������������ֵ�ж�ʱ ������
            float _SensitivityDepth;
            float _SensitivityNormal;

            //���ڱȽ����������Ⱥͷ��������в����õ�����Ϣ �����ж��Ƿ��Ǳ�Ե
            //����ֵ�ĺ��� 
            //1 - ���ߺ����ֵ������ͬ ����ͬһ��ƽ����
            //0 - ����� ����һ��ƽ����
            half CheckSame(half4 depthNormal1, half4 depthNormal2)
            {
                //�ֱ�õ���������Ϣ����Ⱥͷ���
                //��һ�����
                //�õ����ֵ
                float depth1 = DecodeFloatRG(depthNormal1.zw);
                //�õ����ߵ�xy
                float2 normal1 = depthNormal1.xy;

                //�ڶ������
                //�õ����ֵ
                float depth2 = DecodeFloatRG(depthNormal2.zw);
                //�õ����ߵ�xy
                float2 normal2 = depthNormal2.xy;

                //���ߵĲ������
                //�����������ߵ�xy�Ĳ�ֵ ���ҳ��� �Զ�������ж�
                float2 normalDiff = abs(normal1 - normal2) * _SensitivityNormal;
                //�ж����������Ƿ���һ��ƽ��
                //������첻�� ֤����������һ��ƽ���� ���� 1�����򷵻�0
                int isSameNormal = (normalDiff.x + normalDiff.y) < 0.1;

                //��ȵĲ������
                float depthDiff = abs(depth1 - depth2) * _SensitivityDepth;
                //�ж�����ǲ��Ǻܽӽ� �ǲ����൱����һ��ƽ����
                //����������� ֤�����ֵ����ǳ�С ������������һ��ƽ���� ����1������ ����0
                int isSameDepth = depthDiff < 0.1 * depth1;
                //����ֵ�ĺ��� 
                //1 - ���ߺ����ֵ������ͬ ����ͬһ��ƽ����
                //0 - ����� ����һ��ƽ����
                return isSameDepth * isSameNormal ? 1 : 0;
            }

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //uv������긳ֵ
                half2 uv = v.texcoord;
                //���ĵ�
                o.uv[0] = uv;
                //�Խ����������uv���긳ֵ
                //���Ͻ�
                o.uv[1] = uv + _MainTex_TexelSize.xy * half2(-1,1) * _SampleDistance;
                //���½�
                o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1,-1) * _SampleDistance;
                //���Ͻ�
                o.uv[3] = uv + _MainTex_TexelSize.xy * half2(1,1) * _SampleDistance;
                //���½�
                o.uv[4] = uv + _MainTex_TexelSize.xy * half2(-1,-1) * _SampleDistance;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //��ȡ�ĸ������Ⱥͷ�����Ϣ
                half4 TL = tex2D(_CameraDepthNormalsTexture, i.uv[1]);
                half4 BR = tex2D(_CameraDepthNormalsTexture, i.uv[2]);
                half4 TR = tex2D(_CameraDepthNormalsTexture, i.uv[3]);
                half4 BL = tex2D(_CameraDepthNormalsTexture, i.uv[4]);

                //�������+������Ϣ ȥ�ж� �Ƿ��Ǳ�Ե
                half edgeLerpValue = 1;
                //�õ��жϽ�� ����1������һ��ƽ�� ֵ����Ϊ1������0������һ��ƽ�棬ֵ��Ϊ0
                edgeLerpValue *= CheckSame(TL, BR);
                edgeLerpValue *= CheckSame(TR, BL);

                //ͨ����ֵ������ɫ�仯
                fixed4 withEdgeColor = lerp(_EdgeColor, tex2D(_MainTex, i.uv[0]), edgeLerpValue);
                fixed4 onlyEdgeColor = lerp(_EdgeColor, _BackgroundColor, edgeLerpValue);

                return lerp(withEdgeColor, onlyEdgeColor, _EdgeOnly);
            }
            ENDCG
        }
    }
}
