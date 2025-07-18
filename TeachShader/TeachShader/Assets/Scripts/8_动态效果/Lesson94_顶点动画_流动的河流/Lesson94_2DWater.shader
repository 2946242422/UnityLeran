Shader "Unlit/Lesson94_2DWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        //波动幅度
        _WaveAmplitude("WaveAmplitude", Float) = 1
        //波动频率
        _WaveFrequency("WaveFrequency", Float) = 1
        //波长的倒数
        _InvWaveLength("InvWaveLength", Float) = 1

        //纹理变化速度
        _Speed("Speed", Float) = 1
    }
    SubShader
    {
        //透明Shader相关渲染标签 + 关闭批处理标签
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "DisableBatching"="True" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _WaveAmplitude;
            float _WaveFrequency;
            float _InvWaveLength;
            float _Speed;

            v2f vert (appdata_base v)
            {
                v2f o;
                //模型空间下的偏移位置
                float4 offset;
                //让它在模型空间的x轴方向进行偏移
                offset.x = sin(_Time.y * _WaveFrequency + v.vertex.z * _InvWaveLength) * _WaveAmplitude;
                offset.yzw = float3(0,0,0);
                o.vertex = UnityObjectToClipPos(v.vertex + offset);

                o.uv = v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;

                o.uv += float2(0, _Time.y *_Speed );

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex,i.uv);
                color.rgb *= _Color.rgb;
                return color;
            }
            ENDCG
        }
    }
}
