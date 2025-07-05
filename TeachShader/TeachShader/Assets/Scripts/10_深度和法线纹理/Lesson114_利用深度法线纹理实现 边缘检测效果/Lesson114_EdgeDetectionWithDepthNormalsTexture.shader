Shader "Unlit/Lesson114_EdgeDetectionWithDepthNormalsTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //用于控制自定义背景颜色程度的 0要显示原始背景色 1只显示边缘 完全显示自定义背景色
        _EdgeOnly("EdgeOnly", Float) = 0
        //边缘的描边颜色
        _EdgeColor("EdgeColor", Color) = (0,0,0,0)
        //自定义背景颜色
        _BackgroundColor("BackgroundColor", Color) = (1,1,1,1)
        //采样偏移程度 主要用来控制描边的粗细 值越大越粗 反之越细
        _SampleDistance("SampleDistance", Float) = 1
        //深度和法线的敏感度 用来进行这个差值判断时 起作用
        _SensitivityDepth("SensitivityDepth", Float) = 1
        _SensitivityNormal("SensitivityNormal", Float) = 1
    }
    SubShader
    {
        //屏幕后处理标配
        ZTest Always
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                //用于存储5个像素的uv坐标
                half2 uv[5] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            //纹素 用于进行uv坐标偏移 取得周围像素的uv坐标的
            half4 _MainTex_TexelSize;
            //深度+法线纹理
            sampler2D _CameraDepthNormalsTexture;\
            //用于控制自定义背景颜色程度的 0要显示原始背景色 1只显示边缘 完全显示自定义背景色
            fixed _EdgeOnly;
            //边缘的描边颜色
            fixed4 _EdgeColor;
            //自定义背景颜色
            fixed4 _BackgroundColor;
            //采样偏移程度 主要用来控制描边的粗细 值越大越粗 反之越细
            float _SampleDistance;
            //深度和法线的敏感度 用来进行这个差值判断时 起作用
            float _SensitivityDepth;
            float _SensitivityNormal;

            //用于比较两个点的深度和法线纹理中采样得到的信息 用来判断是否是边缘
            //返回值的含义 
            //1 - 法线和深度值基本相同 处于同一个平面上
            //0 - 差异大 不在一个平面上
            half CheckSame(half4 depthNormal1, half4 depthNormal2)
            {
                //分别得到两个点信息的深度和法线
                //第一个点的
                //得到深度值
                float depth1 = DecodeFloatRG(depthNormal1.zw);
                //得到法线的xy
                float2 normal1 = depthNormal1.xy;

                //第二个点的
                //得到深度值
                float depth2 = DecodeFloatRG(depthNormal2.zw);
                //得到法线的xy
                float2 normal2 = depthNormal2.xy;

                //法线的差异计算
                //计算两条法线的xy的差值 并且乘以 自定义的敏感度
                float2 normalDiff = abs(normal1 - normal2) * _SensitivityNormal;
                //判断两个法线是否在一个平面
                //如果差异不大 证明基本上在一个平面上 返回 1；否则返回0
                int isSameNormal = (normalDiff.x + normalDiff.y) < 0.1;

                //深度的差异计算
                float depthDiff = abs(depth1 - depth2) * _SensitivityDepth;
                //判断深度是不是很接近 是不是相当于在一个平面上
                //如果满足条件 证明深度值差异非常小 基本趋近于在一个平面上 返回1；否则 返回0
                int isSameDepth = depthDiff < 0.1 * depth1;
                //返回值的含义 
                //1 - 法线和深度值基本相同 处于同一个平面上
                //0 - 差异大 不在一个平面上
                return isSameDepth * isSameNormal ? 1 : 0;
            }

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //uv相关坐标赋值
                half2 uv = v.texcoord;
                //中心点
                o.uv[0] = uv;
                //对角线相关像素uv坐标赋值
                //左上角
                o.uv[1] = uv + _MainTex_TexelSize.xy * half2(-1,1) * _SampleDistance;
                //右下角
                o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1,-1) * _SampleDistance;
                //右上角
                o.uv[3] = uv + _MainTex_TexelSize.xy * half2(1,1) * _SampleDistance;
                //左下角
                o.uv[4] = uv + _MainTex_TexelSize.xy * half2(-1,-1) * _SampleDistance;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //获取四个点的深度和法线信息
                half4 TL = tex2D(_CameraDepthNormalsTexture, i.uv[1]);
                half4 BR = tex2D(_CameraDepthNormalsTexture, i.uv[2]);
                half4 TR = tex2D(_CameraDepthNormalsTexture, i.uv[3]);
                half4 BL = tex2D(_CameraDepthNormalsTexture, i.uv[4]);

                //根据深度+法线信息 去判断 是否是边缘
                half edgeLerpValue = 1;
                //得到判断结果 返回1代表是一个平面 值不变为1；返回0代表不是一个平面，值变为0
                edgeLerpValue *= CheckSame(TL, BR);
                edgeLerpValue *= CheckSame(TR, BL);

                //通过插值进行颜色变化
                fixed4 withEdgeColor = lerp(_EdgeColor, tex2D(_MainTex, i.uv[0]), edgeLerpValue);
                fixed4 onlyEdgeColor = lerp(_EdgeColor, _BackgroundColor, edgeLerpValue);

                return lerp(withEdgeColor, onlyEdgeColor, _EdgeOnly);
            }
            ENDCG
        }
    }
}
