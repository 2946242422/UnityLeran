Shader "Unlit/Lesson104_EdgeDetection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //边缘线的颜色
        _EdgeColor("EdgeColor", Color) = (0,0,0,0)
        //背景颜色程度 1为纯色 0为原始颜色
        _BackgroundExtent("BackgroundExtent", Range(0,1)) = 0
        //背景颜色
        _BackgroundColor("BackgroundColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                //用于存储9个像素uv坐标的变量
                half2 uv[9] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            //Unity内置纹素变量
            half4 _MainTex_TexelSize;
            fixed4 _EdgeColor;
            fixed _BackgroundExtent;
            fixed4 _BackgroundColor;


            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //当前顶点的纹理坐标
                half2 uv = v.texcoord;
                //去对9个像素的uv坐标进行计算
                o.uv[0] = uv + _MainTex_TexelSize.xy * half2(-1, -1);
                o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0, -1);
                o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1, -1);
                o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1, 0);
                o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0, 0);
                o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1, 0);
                o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1, 1);
                o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0, 1);
                o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1, 1);

                return o;
            }

            //计算颜色的灰度值
            fixed calcLuminance(fixed4 color)
            {
                return 0.2126*color.r + 0.7152*color.g + 0.0722*color.b;
            }

            //Sobel算子相关的卷积计算
            half Sobel(v2f o)
            {
                //Sobel算子对应的两个卷积核
                half Gx[9] = {-1, -2, -1,
                               0,  0,  0,
                               1,  2,  1};
                half Gy[9] = {-1, 0, 1,
                              -2, 0, 2,
                              -1, 0, 1};
                half L;//灰度值
                half edgeX = 0;//水平方向梯度值
                half edgeY = 0;//数值方向梯度值
                for (int i = 0; i < 9; i++)
                {
                    //采样颜色后 计算灰度值 并记录下来
                    L = calcLuminance(tex2D(_MainTex, o.uv[i]));
                    edgeX += L * Gx[i];
                    edgeY += L * Gy[i];
                }
                //最终的一个该像素的梯度值
                //half G = abs(edgeX) + abs(edgeY);
                return abs(edgeX) + abs(edgeY);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //利用索贝尔算子计算梯度值
                half edge = Sobel(i);
                //利用计算出来的梯度值在原始颜色 和边缘线颜色之间进行插值
                fixed4 withEdgeColor = lerp(tex2D(_MainTex, i.uv[4]), _EdgeColor, edge);
                //纯色上描边
                fixed4 onlyEdgeColor = lerp(_BackgroundColor, _EdgeColor, edge);
                //通过程度变量 去控制 是纯色描边 还是 原始颜色描边 在两者之间 进行过渡
                return lerp(withEdgeColor, onlyEdgeColor, _BackgroundExtent);
            }

            ENDCG
        }
    }

    Fallback Off
}
