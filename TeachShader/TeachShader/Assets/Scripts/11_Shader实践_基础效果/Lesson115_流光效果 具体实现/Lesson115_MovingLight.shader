Shader "Unlit/Lesson115_MovingLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //光叠加的颜色 默认图片时白色 我们叠加一个自定义颜色 可以改变它
        _Color("Color", Color) = (1,1,1,1)
        //流光移动的速度
        _Speed("Speed", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend One One
        Cull Off

        Pass
        {
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
            float _Speed;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //根据时间 进行UV坐标偏移 这样x（u）方向就可以看到移动的效果了
                i.uv = float2(i.uv.x + _Time.x * _Speed, i.uv.y);
                return tex2D(_MainTex, i.uv) * _Color;
            }
            ENDCG
        }
    }
}
