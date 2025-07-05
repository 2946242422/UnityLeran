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


            //该结构体
            //是用于从应用阶段获取对应语义数据后
            //传递给顶点着色器回调函数的
            struct a2v
            {
                //顶点坐标(基于模型空间)
                float4 vertex:POSITION;
                //顶点法线(基于模型空间)
                float3 normal:NORMAL;
                //纹理坐标(uv坐标)
                float2 uv:TEXCOORD0;
            };


            //从顶点着色器传递给片元着色器的 结构体数据 
            //同样这里面的成员也需要用语义去进行修饰
            struct v2f
            {
                //裁剪空间下的坐标
                float4 position:SV_POSITION;
                //顶点法线(基于模型空间)
                float3 normal:NORMAL;
                //纹理坐标(uv坐标)
                float2 uv:TEXCOORD0;
            };

            v2f_img vert(appdata_base data)
            {
                //需要传递给片元着色器的数据
                v2f_img v2fData;
                v2fData.pos = UnityObjectToClipPos(data.vertex);
                //一般用于进行坐标或者向量的矩阵变换（空间变换）
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
