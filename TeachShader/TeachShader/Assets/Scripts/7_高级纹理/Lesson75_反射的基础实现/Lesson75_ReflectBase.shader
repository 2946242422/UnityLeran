Shader "Unlit/Lesson75_ReflectBase"
{
    Properties
    {
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

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            samplerCUBE _Cube;
            float _Reflectivity;

            struct v2f
            {
                float4 pos:SV_POSITION;//裁剪空间下的顶点坐标
                //世界空间下的反射向量
                //我们将把反射向量的计算放在顶点着色器函数中 节约性能 表现效果也不会太差 肉眼几乎分辨不出来
                float3 worldRefl:TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //顶点坐标转换
                o.pos = UnityObjectToClipPos(v.vertex);
                //计算反射光向量
                //1.计算世界空间下法线向量
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //2.世界空间下的顶点坐标
                fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //3.计算视角方向 内部是用摄像机位置 - 世界坐标位置 
                fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
                //4.计算反射向量
                o.worldRefl = reflect(-worldViewDir, worldNormal);

                return o;
            }

            fixed4 frag(v2f i):SV_TARGET
            {
                //对立方体纹理利用对应的反射向量进行采样
                fixed4 cubemapColor = texCUBE(_Cube, i.worldRefl);
                //用采样颜色 * 反射率 决定最终的颜色效果
                return cubemapColor * _Reflectivity;
            }

            ENDCG
        }
    }
}
