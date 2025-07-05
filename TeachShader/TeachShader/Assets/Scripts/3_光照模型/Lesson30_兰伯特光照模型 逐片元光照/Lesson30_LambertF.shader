Shader "Unlit/Lesson30_LambertF"
{
    Properties
    {
        _MainColor("MainColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "LightMode"="ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            //材质漫反射颜色
            fixed4 _MainColor;

            //顶点着色器返回出去的内容
            struct v2f
            {
                //裁剪空间下的顶点位置
                float4 pos:SV_POSITION;
                //世界空间下的法线位置
                float3 normal:NORMAL;
            };
            
            //一定注意 顶点着色器回调函数 主要就是用于处理顶点、法线、切线等数据的坐标转换
            v2f vert (appdata_base v)
            {
               v2f v2fData;
               //转换模型空间下的顶点到裁剪空间中
               v2fData.pos = UnityObjectToClipPos(v.vertex);
               //转换模型空间下的法线到世界空间下
               v2fData.normal = UnityObjectToWorldNormal(v.normal);

               return v2fData;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //得到光源单位向量
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //计算除了兰伯特光照的漫反射颜色
                fixed3 color = _LightColor0.rgb * _MainColor.rgb * max(0, dot(i.normal, lightDir));
                //为了让背光的地方不至于是黑色 所以加上自带的漫反射颜色 看起来更加真实
                color = UNITY_LIGHTMODEL_AMBIENT.rgb + color;

                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
}
