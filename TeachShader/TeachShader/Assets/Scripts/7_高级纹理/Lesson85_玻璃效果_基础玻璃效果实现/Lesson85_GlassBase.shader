Shader "Unlit/Lesson85_GlassBase"
{
     Properties
    {
        //������
        _MainTex("MainTex", 2D) = ""{}
        //����������
        _Cube("Cubemap", Cube) = ""{}
        //����̶� 0~1 0������ȫ���䣨��ȫ�����䣩1������ȫ���䣨͸��Ч�� �൱�ڹ�ȫ���������ڲ���
        _RefractAmount("RefractAmount", Range(0,1)) = 1
    }
    SubShader
    {
        //����Ⱦ���и�Ϊ͸���� Ŀ�����ò������� �ͺ���Ⱦ
        //�ܹ�����֮ǰ��ȷ����Ļͼ��
        Tags{"RenderType"="Opaque" "Queue"="Transparent"}
        //ʹ����������ǰ��Ļ���� ���洢��Ĭ�ϵ���Ⱦ���������
        GrabPass{}

        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            samplerCUBE _Cube;
            float _RefractAmount;
            //GrabPassĬ�ϴ洢��������� ����ǹ���
            sampler2D _GrabTexture;

            struct v2f
            {
                float4 pos:SV_POSITION;//�ü��ռ��µĶ�������
                //���ڴ洢����Ļͼ���в��������꣨�����������Ļ��λ�ã�
                float4 grabPos:TEXCOORD0;
                //��������ɫ�����в�����UV����
                float2 uv:TEXCOORD1;
                //����ռ��µķ�������
                //���ǽ��ѷ��������ļ�����ڶ�����ɫ�������� ��Լ���� ����Ч��Ҳ����̫�� ���ۼ����ֱ治����
                float3 worldRefl:TEXCOORD2;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //��������ת��
                o.pos = UnityObjectToClipPos(v.vertex);
                //��Ļ����ת����ص�����
                o.grabPos = ComputeGrabScreenPos(o.pos);
                //uv���������ص�����
                o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                //���㷴�������
                //1.��������ռ��·�������
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.����ռ��µĶ�������
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.�����ӽǷ��� �ڲ����������λ�� - ��������λ�� 
                fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                //4.���㷴������
                o.worldRefl = reflect(-worldViewDir, worldNormal);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //������ɫ��صļ��� �������������ɫ
                //����ɫ������в���
                fixed4 mainTex = tex2D(_MainTex, i.uv);
                //��������ɫ����������ɫ���е���
                fixed4 reflColor = texCUBE(_Cube, i.worldRefl) * mainTex;
                
                //������ص���ɫ
                //��ʵ���Ǵ�����ץȡ�� ��Ļ��Ⱦ�����н��в��� �������
                //ץȡ�����е���ɫ��Ϣ �൱���������������������ɫ

                //��Ҫ������Ч�� �����ڲ���֮ǰ ����xy��Ļ�����ƫ��
                float2 offset = 1 - _RefractAmount;
                //xyƫ��һ��λ��
                i.grabPos.xy = i.grabPos.xy - offset/10; 

                //����͸�ӳ��� ����Ļ����ת���� 0~1��Χ�� Ȼ���ٽ��в���
                fixed2 screenUV = i.grabPos.xy / i.grabPos.w;
                //�Ӳ������Ⱦ�����н��в��� ��ȡ�������ɫ
                fixed4 grabColor = tex2D(_GrabTexture, screenUV);

                //����̶� 0~1 0������ȫ���䣨��ȫ�����䣩1������ȫ���䣨͸��Ч�� �൱�ڹ�ȫ���������ڲ���
                float4 color = reflColor*(1 - _RefractAmount) + grabColor * _RefractAmount;

                return color;
            }

            ENDCG
        }
    }
}
