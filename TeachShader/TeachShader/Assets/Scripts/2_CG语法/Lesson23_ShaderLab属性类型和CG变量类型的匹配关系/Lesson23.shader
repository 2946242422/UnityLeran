Shader "Unlit/Lesson23"
{
    Properties
    {
        _MyInt("MyInt", Int) = 1
        _MyFloat("MyFloat", Float) = 1
        _MyRange("MyRang", Range(0,5)) = 1

        _MyColor("MyColor", Color) = (0,0,0,0)
        _MyVector("MyVector", Vector) = (1,2,0,0)

        _My2D("My2D", 2D) = ""{}
        _MyCube("MyCube", Cube) = ""{}

        _My2DArray("My2DArray", 2DArray) = ""{}
        _My3D("My3D", 3D) = ""{}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _MyInt;
            float _MyFloat;
            fixed _MyRange;

            fixed4 _MyColor;
            float4 _MyVector;

            sampler2D _My2D;
            samplerCUBE _MyCube;


            //�ýṹ��
            //�����ڴ�Ӧ�ý׶λ�ȡ��Ӧ�������ݺ�
            //���ݸ�������ɫ���ص�������
            struct a2v
            {
                //��������(����ģ�Ϳռ�)
                float4 vertex:POSITION;
                //���㷨��(����ģ�Ϳռ�)
                float3 normal:NORMAL;
                //��������(uv����)
                float2 uv:TEXCOORD0;
            };


            //�Ӷ�����ɫ�����ݸ�ƬԪ��ɫ���� �ṹ������ 
            //ͬ��������ĳ�ԱҲ��Ҫ������ȥ��������
            struct v2f
            {
                //�ü��ռ��µ�����
                float4 position:SV_POSITION;
                //���㷨��(����ģ�Ϳռ�)
                float3 normal:NORMAL;
                //��������(uv����)
                float2 uv:TEXCOORD0;
            };

            v2f_img vert(appdata_base data)
            {
                //��Ҫ���ݸ�ƬԪ��ɫ��������
                v2f_img v2fData;
                v2fData.pos = UnityObjectToClipPos(data.vertex);
                //һ�����ڽ���������������ľ���任���ռ�任��
                //v2fData.pos = mul(UNITY_MATRIX_MVP,data.vertex);
                //float3 worldPos = mul(_Object2World, data.vertex);
                //v2fData.normal = data.normal;
                v2fData.uv = data.texcoord;

                return v2fData;
            }

          
            fixed4 frag(v2f_img data):SV_Target
            {
                fixed4 color = tex2D(_My2D, data.uv);
                return color;
            }
            ENDCG
        }
    }
}
