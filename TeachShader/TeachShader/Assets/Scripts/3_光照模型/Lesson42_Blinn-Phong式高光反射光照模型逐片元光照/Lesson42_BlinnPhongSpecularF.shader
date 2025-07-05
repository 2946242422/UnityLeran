Shader "Unlit/Lesson42_BlinnPhongSpecularF"
{
    Properties
    {
        //高光反射材质颜色 以及 光泽度
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(0,20)) = 5
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct v2f
            {
                //裁剪空间的位置
                float4 pos:SV_POSITION;
                //基于世界坐标系下的顶点位置
                float3 wPos:TEXCOORD0;
                //基于世界坐标系下的发现
                float3 wNormal:NORMAL;
            };

            fixed4 _SpecularColor;
            float _SpecularNum;

            v2f vert (appdata_base v)
            {
                v2f data;
                //顶点转到裁剪空间
                data.pos = UnityObjectToClipPos(v.vertex);
                //把顶点从模型空间转换到世界空间中 进行矩阵乘法运算
                data.wPos = mul(unity_ObjectToWorld, v.vertex);
                //法线计算
                data.wNormal = UnityObjectToWorldNormal(v.normal);

                return data;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //高光反射光照颜色 = 光源的颜色 * 材质高光反射颜色 * max（0, 标准化后顶点法线方向向量 · 标准化后半角向量方向向量）幂

                float3 viewDir = normalize( _WorldSpaceCameraPos.xyz - i.wPos );
                //光线方向 单位向量
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //半角方向向量
                float3 halfA = normalize(viewDir + lightDir);

                //高光反射光照颜色 = 光源的颜色 * 材质高光反射颜色 * max（0, 标准化后顶点法线方向向量 · 标准化后半角向量方向向量）幂
                fixed3 color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(i.wNormal, halfA)) , _SpecularNum);

                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
}
