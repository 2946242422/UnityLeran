Shader "Unlit/Lesson121_Sketch"
{
    Properties
    {
        //������ɫ����
        _Color("Color", Color) = (1,1,1,1)
        //ƽ�������ϵ��
        _TileFactor("TileFactor", Float) = 1
        //6������������ͼ
        _Sketch0("Sketch0", 2D) = ""{}
        _Sketch1("Sketch1", 2D) = ""{}
        _Sketch2("Sketch2", 2D) = ""{}
        _Sketch3("Sketch3", 2D) = ""{}
        _Sketch4("Sketch4", 2D) = ""{}
        _Sketch5("Sketch5", 2D) = ""{}
        //��Ե����ز���
        _OutLineColor("OutLineColor", Color) = (0,0,0,1)
        _OutLineWidth("OutLineWidth", Range(0,1)) = 0.04
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        UsePass "Unlit/Lesson120_ToonShader/OUTLINE"

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            fixed4 _Color;
            fixed _TileFactor;
            sampler2D _Sketch0;
            sampler2D _Sketch1;
            sampler2D _Sketch2;
            sampler2D _Sketch3;
            sampler2D _Sketch4;
            sampler2D _Sketch5;

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                //xyz�ֱ�����1��2��3�����������Ȩ��
                fixed3 sketchWeights0:TEXCOORD1;
                //xyz�ֱ�����4��5��6�����������Ȩ��
                fixed3 sketchWeights1:TEXCOORD2;
                float3 worldPos:TEXCOORD3;
                SHADOW_COORDS(4)
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                //��������ת��
                o.vertex = UnityObjectToClipPos(v.vertex);
                //uv����ƽ������ ֵԽ�� ֮�������ϸ��Խ�� ԽС ϸ��Խ�ֲ�
                o.uv = v.texcoord.xy * _TileFactor;
                //����ռ���շ���
                fixed3 worldLightDir = normalize(WorldSpaceLightDir(v.vertex));
                //����ռ䷨�߷���ת�� 
                fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //���������������ǿ�� 0~1 
                fixed diff = max(0, dot(worldLightDir, worldNormal));
                //�������䵽0~7  7/7 = 1 
                //Խ��Խ�� ԽСԽ��
                diff = diff * 7.0;

                //��ʼ��Ȩ�� Ĭ��ÿһ�����������Ӧ��Ȩ�ض���0
                o.sketchWeights0 = fixed3(0,0,0);
                o.sketchWeights1 = fixed3(0,0,0);

                //����6��ͼ��Ȩ�ض�����0 ��ô�����Ͳ���ʹ��6��ͼ����ɫ
                if(diff > 6.0){
                    //��Ϊ�������Ĳ��� ���ǲ���Ҫ�ı��κ�Ȩ��
                }
                else if(diff > 5.0){//���� ��ӵ�1��ͼ�в��� ��˶�ӦȨ�ز���Ϊ0
                    o.sketchWeights0.x = diff - 5.0;
                }
                else if(diff > 4.0){//���� ��ӵ�1��2��ͼ�в��� ��˶�ӦȨ�ز���Ϊ0
                    o.sketchWeights0.x = diff - 5.0;
                    o.sketchWeights0.y = 1 - o.sketchWeights0.x;
                }
                else if(diff > 3.0){//���� ��ӵ�2��3��ͼ�в��� ��˶�ӦȨ�ز���Ϊ0
                    o.sketchWeights0.y = diff - 3.0;
                    o.sketchWeights0.z = 1 - o.sketchWeights0.y;
                }
                else if(diff > 2.0){//���� ��ӵ�3��4��ͼ�в��� ��˶�ӦȨ�ز���Ϊ0
                    o.sketchWeights0.z = diff - 2.0;
                    o.sketchWeights1.x = 1 - o.sketchWeights0.z;
                }
                else if(diff > 1.0){//���� ��ӵ�4��5��ͼ�в��� ��˶�ӦȨ�ز���Ϊ0
                    o.sketchWeights1.x = diff - 1.0;
                    o.sketchWeights1.y = 1 - o.sketchWeights1.x;
                }
                else{//���� ��ӵ�5��6��ͼ�в��� ��˶�ӦȨ�ز���Ϊ0
                    o.sketchWeights1.y = diff;
                    o.sketchWeights1.z = 1 - diff;
                }

                //��������ת��������
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //����ڶ�����ɫ���м���Ķ�Ӧ��������ͼƬ��Ȩ��Ϊ0 ��ô��ʱ ��Ӧ��ɫ��Ϊ(0,0,0,0)
                fixed4 sketchColor0 = tex2D(_Sketch0, i.uv) * i.sketchWeights0.x;
                fixed4 sketchColor1 = tex2D(_Sketch1, i.uv) * i.sketchWeights0.y;
                fixed4 sketchColor2 = tex2D(_Sketch2, i.uv) * i.sketchWeights0.z;
                fixed4 sketchColor3 = tex2D(_Sketch3, i.uv) * i.sketchWeights1.x;
                fixed4 sketchColor4 = tex2D(_Sketch4, i.uv) * i.sketchWeights1.y;
                fixed4 sketchColor5 = tex2D(_Sketch5, i.uv) * i.sketchWeights1.z;

                //�����Ĳ�����صļ��� ��ɫ����
                fixed4 whiteColor = fixed4(1,1,1,1) * (1 - i.sketchWeights0.x - i.sketchWeights0.y -i.sketchWeights0.z -
                                    i.sketchWeights1.x - i.sketchWeights1.y - i.sketchWeights1.z);

                fixed4 sketchClor = sketchColor0 + sketchColor1 + sketchColor2 + sketchColor3 + sketchColor4 +
                                    sketchColor5 + whiteColor;

                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

                return fixed4(sketchClor.rgb * atten * _Color.rgb, 1);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
