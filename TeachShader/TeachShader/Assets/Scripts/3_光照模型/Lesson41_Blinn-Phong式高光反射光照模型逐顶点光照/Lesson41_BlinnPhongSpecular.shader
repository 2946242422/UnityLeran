// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Lesson41_BlinnPhongSpecular"
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
                float4 pos:SV_POSITION;
                fixed3 color:COLOR;
            };

            fixed4 _SpecularColor;
            float _SpecularNum;

            v2f vert (appdata_base v)
            {
                v2f data;
                //顶点转到裁剪空间
                data.pos = UnityObjectToClipPos(v.vertex);

                //高光反射光照颜色 = 光源的颜色 * 材质高光反射颜色 * max（0, 标准化后顶点法线方向向量 · 标准化后半角向量方向向量）幂

                //1.标准化后顶点法线方向向量
                float3 wNormal = UnityObjectToWorldNormal(v.normal);

                //2.标准化后半角向量方向向量
                //视角方向 单位向量
                float3 wPos = mul(unity_ObjectToWorld, v.vertex);
                float3 viewDir = normalize( _WorldSpaceCameraPos.xyz - wPos );
                //光线方向 单位向量
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //半角方向向量
                float3 halfA = normalize(viewDir + lightDir);

                //高光反射光照颜色 = 光源的颜色 * 材质高光反射颜色 * max（0, 标准化后顶点法线方向向量 · 标准化后半角向量方向向量）幂
                data.color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(wNormal, halfA)) , _SpecularNum);


                return data;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color.rgb, 1);
            }
            ENDCG
        }
    }
}
