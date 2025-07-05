Shader "Unlit/Lesson76_Reflection"
{
    Properties
    {
        //漫反射颜色
        _Color("Color", Color) = (1,1,1,1)
        //反射颜色
        _ReflectColor("ReflectColor", Color) = (1,1,1,1)
        //立方体纹理
        _Cube("Cubemap", Cube) = ""{}
        //反射率
        _Reflectivity("Reflectivity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags{"RenderType"="Opaque" "Queue"="Geometry"}

        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            fixed4 _Color;
            fixed4 _ReflectColor;
            samplerCUBE _Cube;
            float _Reflectivity;

            struct v2f
            {
                float4 pos:SV_POSITION;//裁剪空间下的顶点坐标
                //世界空间下法线
                fixed3 worldNormal:NORMAL;
                //世界空间下的顶点位置
                float3 worldPos:TEXCOORD0;
                //世界空间下的反射向量
                //我们将把反射向量的计算放在顶点着色器函数中 节约性能 表现效果也不会太差 肉眼几乎分辨不出来
                float3 worldRefl:TEXCOORD1;
                //阴影相关
                SHADOW_COORDS(2)
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //顶点坐标转换
                o.pos = UnityObjectToClipPos(v.vertex);
                //计算反射光向量
                //1.计算世界空间下法线向量
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.世界空间下的顶点坐标
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.计算视角方向 内部是用摄像机位置 - 世界坐标位置 
                fixed3 worldViewDir = UnityWorldSpaceViewDir(o.worldPos);
                //4.计算反射向量
                o.worldRefl = reflect(-worldViewDir, o.worldNormal);

                //阴影相关处理
                TRANSFER_SHADOW(o);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //漫反射光照相关计算
                //得到光的方向
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                //漫反射颜色
                fixed3 diffuse = _LightColor0.rgb * _Color.rgb * max(0, dot(normalize(i.worldNormal), worldLightDir));

                //对立方体纹理利用对应的反射向量进行采样
                fixed3 cubemapColor = texCUBE(_Cube, i.worldRefl).rgb * _ReflectColor.rgb;

                //得到光照衰减以及阴影相关的衰减值
                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

                //我们利用lerp 在漫反射颜色和反射颜色之间 进行插值 0和1就是极限状态 0 没有反射效果 1只有反射效果 0~1之间就是两者叠加
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.rgb + lerp(diffuse, cubemapColor, _Reflectivity) * atten;

                //用采样颜色 * 反射率 决定最终的颜色效果
                return fixed4(color, 1.0);
            }

            ENDCG
        }
    }
    FallBack "Reflective/VertexLit"
}
