Shader "Unlit/Lesson29_Lambert"
{
    Properties
    {
        //材质的漫反射光照颜色
        _MainColor("MainColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        //设置我们的光照模式 ForwardBase这种向前渲染模式 主要是用来处理 不透明物体的 光照渲染的
        Tags { "LightMode"="ForwardBase" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //引用对应的内置文件 
            //主要是为了之后 的 比如内置结构体使用，内置变量使用
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            //材质的漫反射颜色
            fixed4 _MainColor;

            //顶点着色器传递给片元着色器的内容
            struct v2f
            {
                //裁剪空间下的顶点坐标信息
                float4 pos:SV_POSITION;
                //对应顶点的漫反射光照颜色
                fixed3 color:COLOR;
            };

            //逐顶点光照 所以相关的漫反射光照颜色的计算 需要写在顶点着色器 回调函数中
            v2f vert (appdata_base v)
            {
                v2f v2fData;
                //把模型空间下的 顶点转换到裁剪空间下
                v2fData.pos = UnityObjectToClipPos(v.vertex);

                //在模型空间下的发现 
                //v.normal
                //获取到 相对于世界坐标系下的 发现信息
                float3 normal = UnityObjectToWorldNormal(v.normal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                //有了相关的参数 就可以用公式来进行计算了
                //光照颜色
                //_LightColor0
                fixed3 color = _LightColor0.rgb * _MainColor.rgb * max(0, dot(normal, lightDir));

                //记录颜色 传递给片元着色器
                //我们加上兰伯特光照模型环境光变量的目的 是希望阴影处不要全黑 不然看起来有一些不自然
                //目的就是为了让表现效果更接近于真实世界 所以需要加上它
                v2fData.color = UNITY_LIGHTMODEL_AMBIENT.rgb + color;

                return v2fData;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //把计算好的兰伯特光照的颜色 传递出去就可以了
                return fixed4(i.color.rgb, 1);
            }
            ENDCG
        }
    }
}
