Shader "Unlit/Lesson53"
{
    Properties
    {
        _MainColor("MainColor", Color) = (1,1,1,1)
        _RampTex("RampTex", 2D) = ""{}
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(8, 256)) = 18
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            fixed4 _MainColor;
            sampler2D _RampTex;
            float4 _RampTex_ST;
            fixed4 _SpecularColor;
            float _SpecularNum;

            struct v2f
            {
                //裁剪空间下顶点坐标
                float4 pos:SV_POSITION;
                //世界空间下顶点坐标
                float3 worldPos:TEXCOORD0;
                //世界空间下法线
                float3 worldNormal:TEXCOORD1;
            };

            v2f vert (appdata_base v)
            {
                v2f data;
                data.pos = UnityObjectToClipPos(v.vertex);
                data.worldPos = mul(unity_ObjectToWorld, v.vertex);
                data.worldNormal = UnityObjectToWorldNormal(v.normal);
                return data;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //光的方向
                float3 lightDir = normalize(_WorldSpaceLightPos0);
                
                //漫反射颜色（通过渐变纹理得到的颜色来进行叠加）
                fixed halfLambertNum = dot(normalize(i.worldNormal), lightDir) * 0.5 + 0.5;
                //漫反射颜色 = 光的颜色 * 漫反射颜色 * 渐变纹理中取出的颜色
                fixed3 diffuseColor = _LightColor0.rgb * _MainColor.rgb * tex2D(_RampTex, fixed2(halfLambertNum, halfLambertNum));

                //高光反射颜色
                //视角方向
                float3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                float3 halfDir = normalize(lightDir + viewDir);
                fixed3 specularColor = _LightColor0.rgb * _SpecColor.rgb * pow(max(0, dot(i.worldNormal, halfDir)), _SpecularNum);

                //布林方 公式
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.rgb + diffuseColor + specularColor;

                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
}
