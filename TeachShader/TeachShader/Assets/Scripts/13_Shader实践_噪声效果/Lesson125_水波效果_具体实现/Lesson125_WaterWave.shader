Shader "Unlit/Lesson125_WaterWave"
{
      Properties
    {
        //������
        _MainTex("MainTex", 2D) = ""{}
        //��������
        _BumpMap("BumpMap", 2D) = ""{}
        //����������
        _Cube("Cubemap", Cube) = ""{}
        
        //��������Ť���̶ȵı���
        _Distortion("Distortion", Range(0,10)) = 0

        //����ˮ��ˮƽ����ֱ�ٶ�ƫ�Ƶ�����
        _WaveXSpeed("WaveXSpeed", Range(-0.1, 0.1)) = 0.01
        _WaveYSpeed("WaveYSpeed", Range(-0.1, 0.1)) = 0.01
        //֮�����ڼ��������Ч����ϵ��
        _FresnelScale("FresnelScale", Range(0,1)) = 1
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
            sampler2D _BumpMap;//��������
            float4 _BumpMap_ST;//������������ź�ƽ��
            samplerCUBE _Cube;
            //GrabPassĬ�ϴ洢��������� ����ǹ���
            sampler2D _GrabTexture;
            float _Distortion;

            fixed _WaveXSpeed;
            fixed _WaveYSpeed;
            float _FresnelScale;

            struct v2f
            {
                float4 pos:SV_POSITION;//�ü��ռ��µĶ�������
                //���ڴ洢����Ļͼ���в��������꣨�����������Ļ��λ�ã�
                float4 grabPos:TEXCOORD0;
                //��������ɫ�����в�����UV����
                float4 uv:TEXCOORD1;
                //����ռ��µķ�������
                //���ǽ��ѷ��������ļ�����ڶ�����ɫ�������� ��Լ���� ����Ч��Ҳ����̫�� ���ۼ����ֱ治����
                //float3 worldRefl:TEXCOORD2;

                //�����������߿ռ䵽����ռ�� �任�����3��
                float4 TtoW0:TEXCOORD3;
                float4 TtoW1:TEXCOORD4;
                float4 TtoW2:TEXCOORD5;
            };

            v2f vert(appdata_full v)
            {
                v2f o;
                //��������ת��
                o.pos = UnityObjectToClipPos(v.vertex);
                //��Ļ����ת����ص�����
                o.grabPos = ComputeGrabScreenPos(o.pos);
                //uv���������ص�����
                o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                //���������uv�������
                o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                //���㷴�������
                //1.��������ռ��·�������
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.����ռ��µĶ�������
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                ////3.�����ӽǷ��� �ڲ����������λ�� - ��������λ�� 
                //fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                ////4.���㷴������
                //o.worldRefl = reflect(-worldViewDir, worldNormal);

                //����ռ�������
                float3 worldTangent = UnityObjectToWorldDir(v.tangent);
                //���㸱���� �����˽���� ��ֱ�����ߺͷ��ߵ����������� ͨ������ ���ߵ��е�w���Ϳ���ȷ������һ��
                float3 worldBinormal = cross(normalize(worldTangent), normalize(worldNormal)) * v.tangent.w;

                o.TtoW0 = float4(worldTangent.x, worldBinormal.x,  worldNormal.x, worldPos.x);
                o.TtoW1 = float4(worldTangent.y, worldBinormal.y,  worldNormal.y, worldPos.y);
                o.TtoW2 = float4(worldTangent.z, worldBinormal.z,  worldNormal.z, worldPos.z);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //����ռ����ӽǷ���
                float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));

                ////ͨ������������� ȡ������������ͼ���е�����
                //float4 packedNormal = tex2D(_BumpMap, i.uv.zw);
                ////������ȡ�����ķ������� ���������㲢�ҿ��ܻ���н�ѹ�������㣬���յõ����߿ռ��µķ�������
                //float3 tangentNormal = UnpackNormal(packedNormal);
                
                //�����һ��ˮ���ƶ��ٶȵĸо�
                //�����ͼ��ѧǰ�����ܽ��һ�׼򵥵��Ŷ��㷨 ������ˮ�������桢��������� �����Ŷ��У���̬�У�
                float2 speed = _Time.y * float2(_WaveXSpeed, _WaveYSpeed);
                fixed3 bump1 = UnpackNormal(tex2D(_BumpMap, i.uv.zw + speed)).rgb;
                fixed3 bump2 = UnpackNormal(tex2D(_BumpMap, i.uv.zw - speed)).rgb;
                fixed3 bump = normalize(bump1 + bump2);

                //�Ѽ�����Ϻ�����߿ռ��µķ���ת��������ռ���
                //float3x3 rotation = float3x3(i.TtoW0.xyz, i.TtoW1.xyz, i.TtoW2.xyz );
                //float3 worldNormal = mul(rotation, bump);
                //���� �����ڽ��о�������
                float3 worldNormal = float3(dot(i.TtoW0.xyz, bump), dot(i.TtoW1.xyz, bump), dot(i.TtoW2.xyz, bump));


                //������ɫ��صļ��� �������������ɫ
                //����ɫ������в���
                fixed4 mainTex = tex2D(_MainTex, i.uv + speed);

                //������Ҫ��������㷴������ ��Ϊ��ʱ�Űѷ��������еķ�����Ϣ �����
                float3 refl = reflect(-viewDir, worldNormal);
                //��������ɫ����������ɫ���е���
                fixed4 reflColor = texCUBE(_Cube, refl) * mainTex;
                
                //������ص���ɫ
                //��ʵ���Ǵ�����ץȡ�� ��Ļ��Ⱦ�����н��в��� �������
                //ץȡ�����е���ɫ��Ϣ �൱���������������������ɫ

                //��Ҫ������Ч�� �����ڲ���֮ǰ ����xy��Ļ�����ƫ��
                float2 offset = bump.xy * _Distortion;
                //xyƫ��һ��λ��
                i.grabPos.xy = offset*i.grabPos.z + i.grabPos.xy; 

                //����͸�ӳ��� ����Ļ����ת���� 0~1��Χ�� Ȼ���ٽ��в���
                fixed2 screenUV = i.grabPos.xy / i.grabPos.w;
                //�Ӳ������Ⱦ�����н��в��� ��ȡ�������ɫ
                fixed4 grabColor = tex2D(_GrabTexture, screenUV);

                //����schlick���������Ƶ�ʽ ���������������
                fixed fresnel = _FresnelScale + (1-_FresnelScale) * pow(1-dot(normalize(viewDir), normalize(worldNormal)), 5);

                float4 color = reflColor*fresnel + grabColor * (1-fresnel);

                return color;
            }

            ENDCG
        }
    }
}
