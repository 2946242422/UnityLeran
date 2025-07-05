Shader "Unlit/Lesson118_ObjectCutting"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //用于渲染模型背面像素的纹理
        _BackTex ("BackTex", 2D) = "white" {}
        //切割的方向 0-x 1-y 2-z
        _CuttingDir("CuttingDir", Float) = 0
        //是否切割翻转 0-不翻转 1-翻转
        _Invert("Invert", Float) = 0
        //切割的位置
        _CuttingPos("CuttingPos", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        //因为正反两面都要渲染
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                //世界坐标位置
                float3 worldPos:TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _BackTex;
            fixed _CuttingDir;
            fixed _Invert;
            float4 _CuttingPos;


            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i, fixed face:VFACE) : SV_Target
            {
                //通过用face来进行正反面判断 Unity Shader 因为有了语义 会自动传入对应的参数
                fixed4 col = face > 0 ? tex2D(_MainTex, i.uv) : tex2D(_BackTex, i.uv);

                //丢弃中间值
                fixed cutValue;
                //比较x坐标
                if(_CuttingDir == 0)
                    cutValue = step(_CuttingPos.x, i.worldPos.x);
                //比较y坐标
                else if(_CuttingDir == 1)
                    cutValue = step(_CuttingPos.y, i.worldPos.y);
                //比较z坐标
                else if(_CuttingDir == 2)
                    cutValue = step(_CuttingPos.z, i.worldPos.z);

                //是否进行翻转切割
                cutValue = _Invert ? 1 - cutValue : cutValue;

                //如果丢弃中间值 是0代表要丢弃 是1代表不丢弃 直接返回颜色
                if(cutValue == 0)
                    clip(-1); //传入-1（小于0） 代表这个片元不会渲染 直接丢弃

                return col;
            }
            ENDCG
        }
    }
}
