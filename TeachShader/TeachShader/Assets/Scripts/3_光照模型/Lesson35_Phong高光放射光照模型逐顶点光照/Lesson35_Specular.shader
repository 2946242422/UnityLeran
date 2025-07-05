Shader "Unlit/Lesson35_Specular"
{
     Properties
    {
        _SpecularColor("SpecularColor", Color ) = (1,1,1,1)
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
                data.pos = UnityObjectToClipPos(v.vertex);


                float3 wPos = mul(UNITY_MATRIX_M, v.vertex);
                float3 viewDir = _WorldSpaceCameraPos.xyz - wPos;
                viewDir = normalize(viewDir);

                //2.标准化后的反射方向
                //得到的光位置的方向向量（世界空间下的）
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //法线在世界空间下的向量
                float3 normal = UnityObjectToWorldNormal(v.normal);
                //反射光线向量
                float3 reflectDir = reflect(-lightDir, normal);

                //高光反射光照颜色 = 光源的颜色 * 材质高光反射颜色 * max（0, 标准化后观察方向向量· 标准化后的反射方向）幂
                data.color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(viewDir, reflectDir)), _SpecularNum);

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
