Shader "Unlit/Lesson95_Billboarding"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        //���ڿ��ƴ�ֱ����ƺ�ȫ�����Ƶı仯
        _VerticalBillboarding("VerticalBillboarding", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "DisableBatching"="True" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

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
            float4 _MainTex_ST;
            fixed4 _Color;
            float _VerticalBillboarding;

            v2f vert (appdata_full v)
            {
                v2f o;
                //v.color
                //������ϵ�����ĵ㣨Ĭ�����ǻ���ʹ�õ�ģ�Ϳռ�ԭ����
                float3 center = float3(0,0,0);
                //����Z�ᣨnormal��
                float3 cameraInObjectPos = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1));
                //�õ�Z���Ӧ������
                float3 normalDir = cameraInObjectPos - center;
                //�൱�ڰ�y����ѹ�����_VerticalBillboarding��0 �ʹ��������Z��ѹ����xzƽ�� �����1 ��ô�����������ӽǷ���
                normalDir.y *= _VerticalBillboarding;
                //��λ��Z��
                normalDir = normalize(normalDir);
                //ģ�Ϳռ��µ�Y�������� ��Ϊ���� old up
                //Ϊ�˱���z���010�غ� ����Ϊ�غϺ��ټ����� ���ܻ�õ�0����
                float3 upDir = normalDir.y > 0.999 ? float3(0,0,1) : float3(0,1,0);
                //���ò�˼���X�ᣨright��
                float3 rightDir = normalize(cross(upDir, normalDir));
                //ȥ�������ǵ�Y�� Ҳ����newup
                upDir = normalize(cross(normalDir, rightDir));

                //�õ����������������ϵ���ĵ��ƫ��λ��
                float3 centerOffset = v.vertex.xyz - center;
                //����3������������յĶ���λ�õļ���
                float3 newVertexPos = center + rightDir * centerOffset.x + upDir * centerOffset.y + normalDir * centerOffset.z;
                //���¶���ת�����ü��ռ�
                o.vertex = UnityObjectToClipPos(float4(newVertexPos, 1));

                //uv����ƫ������
                o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                color.rgb *= _Color.rgb;
                return color;
            }
            ENDCG
        }
    }
}
