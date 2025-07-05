Shader "TeachShader/Lesson4"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //_Name("Display Name", type) = defaultValue[{options}]
        //数值类型
        _MyInt("MyInt", Int) = 1
        _MyFloat("MyFloat", Float) = 0.5
        _MyRange("Range", Range(1, 5)) = 2
        //颜色和向量类型
        _MyColor("MyColor", Color) = (0.5,0.5,0.5,1)
        _MyVector("MyVector", Vector) = (99,95,93,123)
        //贴图纹理类型
        _My2D("My2D", 2D) = "White"{}
        _MyCube("MyCube", Cube) = ""{}
        //不常用 了解即可
        _My2DArray("My2DArray", 2DArray) = ""{}
        _My3D("My3D", 3D) = ""{}

    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Transparent-1"
            //"DisableBatching"="True"
            //"DisableBatching" = "LODFading"
            //"ForceNoShadowCasting" = "True"
            //"IgnoreProjector" = "True"
            //"CanUseSpriteAtlas" = "False"
        }
        LOD 100
        Cull Off
        //ZWrite Off
        //ZTest Greater
        //Blend One One

        //UsePass "TeachShader/Lesson4/MYLESSON8PASS"

        GrabPass
        {
            "_BackgroundTexture"
        }

        Pass
        {
            //1.名字
            Name "MyLesson8Pass"
            //2.渲染标签
            //3.渲染状态
            //4.着色器代码相关
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }

    Fallback "VertexLit"
}
