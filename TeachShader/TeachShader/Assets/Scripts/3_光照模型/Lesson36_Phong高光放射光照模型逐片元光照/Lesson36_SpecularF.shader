Shader "Unlit/Lesson36_SpecularF"
{
    Properties
    {
        //高光反射颜色  光泽度
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(0, 20)) = 1
    }
    SubShader
    {
        Pass
        {
            //如果有多个Pass渲染通道时 一般情况下会把光照模式的 Tags放到对应的Pass中
            //以免影响其他Pass
            Tags { "LightMode"="ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct v2f
            {
                //裁剪空间下的顶点坐标
                float4 pos:SV_POSITION;
                //世界空间下的 法线信息
                float3 wNormal:NORMAL;
                //世界空间下的 顶点坐标 
                float3 wPos:TEXCOORD0;
            };

            fixed4 _SpecularColor;
            float _SpecularNum;

            v2f vert (appdata_base v)
            {
                v2f data;
                //1.顶点裁剪空间变换
                data.pos = UnityObjectToClipPos(v.vertex);
                //2.进行法线空间变换
                data.wNormal = UnityObjectToWorldNormal(v.normal);
                //3.顶点转到世界空间
                //data.wPos = mul(UNITY_MATRIX_M, v.vertex).xyz;
                //data.wPos = mul(_Object2World, v.vertex).xyz;
                data.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return data;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //1.视角单位向量
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.wPos );

                //2.光的反射单位向量
                //光的方向
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //光反射光线的单位向量
                float3 reflectDir = reflect(-lightDir, i.wNormal);
                //color = 光源颜色 * 材质高光反射颜色 * pow( max(0, dot(视角单位向量, 光的反射单位向量)), 光泽度 )
                fixed3 color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(viewDir, reflectDir)), _SpecularNum );

                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
}
