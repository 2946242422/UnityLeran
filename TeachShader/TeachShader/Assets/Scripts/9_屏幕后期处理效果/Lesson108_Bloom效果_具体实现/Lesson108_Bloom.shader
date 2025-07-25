Shader "Unlit/Lesson108_Bloom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //用于存储亮度纹理模糊后的结果
        _Bloom("Bloom", 2D) = ""{}
        //亮度阈值 控制亮度纹理 亮度区域的
        _LuminanceThreshold("LuminanceThreshold", Float) = 0.5
        //模糊半径
        _BlurSize("BlurSize", Float) = 1
    }
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        sampler2D _Bloom;
        float _LuminanceThreshold;
        float _BlurSize;

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        //计算颜色的亮度值（灰度值）
        fixed luminance(fixed4 color)
        {
            return 0.2125*color.r + 0.7154*color.g + 0.0721*color.b;
        }

        ENDCG

        ZTest Always
        Cull Off
        ZWrite Off
        //提取的Pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            v2f vert(appdata_base v){
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i):SV_Target{
                //采样源纹理颜色
                fixed4 color = tex2D(_MainTex, i.uv);
                //得到亮度贡献值
                fixed value = clamp(luminance(color) - _LuminanceThreshold, 0, 1);
                //返回颜色*亮度贡献值
                return color * value;
            }

            ENDCG
        }

        //复用高斯模糊的2个Pass
        UsePass "Unlit/Lesson106_GaussianBlur/GAUSSIAN_BLUR_HORIZONTAL"
        UsePass "Unlit/Lesson106_GaussianBlur/GAUSSIAN_BLUR_VERTICAL"

        //用于合成的Pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vertBloom
            #pragma fragment fragBloom

            struct v2fBloom
            {
                float4 pos:SV_POSITION;
                //xy主要用于对主纹理进行采样
                //zw主要用于对亮度模糊后的纹理采样
                half4 uv:TEXCOORD0;
            };

            v2fBloom vertBloom(appdata_base v)
            {
                v2fBloom o;

                o.pos = UnityObjectToClipPos(v.vertex);
                //亮度纹理和主纹理 要采样相同的地方进行颜色叠加
                o.uv.xy = v.texcoord;
                o.uv.zw = v.texcoord;

                //用宏去判断uv坐标是否被翻转
                #if UNITY_UV_STARTS_AT_TOP
                //如果纹素的y小于0 为负数 表示需要对Y轴进行调整
                if(_MainTex_TexelSize.y < 0)
                    o.uv.w = 1 - o.uv.w;
                #endif

                return o;
            }

            fixed4 fragBloom(v2fBloom i):SV_TARGET
            {
                return tex2D(_MainTex, i.uv.xy) + tex2D(_Bloom, i.uv.zw);
            }

            ENDCG
        }
    }
    Fallback Off
}
