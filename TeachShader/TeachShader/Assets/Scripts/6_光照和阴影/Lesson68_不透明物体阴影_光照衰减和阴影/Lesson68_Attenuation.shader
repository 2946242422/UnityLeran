Shader "Unlit/Lesson68_Attenuation"
{
   Properties
    {
        _MainColor("MainColor", Color) = (1,1,1,1)
        //高光反射颜色  光泽度
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _SpecularNum("SpecularNum", Range(0, 20)) = 1
    }
    SubShader
    {
        //Bass Pass 基础渲染通道
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //用于帮助我们编译所有变体 并且保证衰减相关光照变量能够正确赋值到对应的内置变量中
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            //引用包含该内置文件 其中有计算阴影会用到的三个宏（阴影三剑客）
            #include "AutoLight.cginc"

            //材质漫反射颜色
            fixed4 _MainColor;
            fixed4 _SpecularColor;
            float _SpecularNum;

            //顶点着色器返回出去的内容
            struct v2f
            {
                //裁剪空间下的顶点位置
                float4 pos:SV_POSITION;
                //世界空间下的法线位置
                float3 wNormal:NORMAL;
                //世界空间下的 顶点坐标 
                float3 wPos:TEXCOORD0;
                //      该宏在v2f结构体（顶点着色器返回值）中使用
                //      本质上就是声明了一个用于对阴影纹理进行采样的坐标
                //      在内部实际上就是声明了一个名为_ShadowCoord的阴影纹理坐标变量
                //      需要注意的是：
                //      在使用时 SHADOW_COORDS(2) 传入参数2
                //      表示需要时下一个可用的插值寄存器的索引值   
                //阴影坐标宏 主要用于存储阴影纹理坐标
                SHADOW_COORDS(2)
            };

            //得到兰伯特光照模型计算的颜色 （逐片元）
            fixed3 getLambertFColor(in float3 wNormal)
            {
                //得到光源单位向量
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //计算除了兰伯特光照的漫反射颜色
                fixed3 color = _LightColor0.rgb * _MainColor.rgb * max(0, dot(wNormal, lightDir));

                return color;
            }

            //得到Blinn Phong式高光反射模型计算的颜色（逐片元）
            fixed3 getSpecularColor(in float3 wPos, in float3 wNormal)
            {
                //1.视角单位向量
                //float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - wPos );
                float3 viewDir = normalize(UnityWorldSpaceViewDir(wPos));

                //2.光的反射单位向量
                //光的方向
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                //半角方向向量
                float3 halfA = normalize(viewDir + lightDir);
                
                //color = 光源颜色 * 材质高光反射颜色 * pow( max(0, dot(视角单位向量, 光的反射单位向量)), 光泽度 )
                fixed3 color = _LightColor0.rgb * _SpecularColor.rgb * pow( max(0, dot(wNormal, halfA)), _SpecularNum );

                return color;
            }

            v2f vert (appdata_base v)
            {
                v2f v2fData;
                //转换模型空间下的顶点到裁剪空间中
                v2fData.pos = UnityObjectToClipPos(v.vertex);
                //转换模型空间下的法线到世界空间下
                v2fData.wNormal = UnityObjectToWorldNormal(v.normal);
                //顶点转到世界空间
                v2fData.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                //      该宏在顶点着色器函数中调用，传入对应的v2f结构体对象
                //      该宏会在内部自己判断应该使用哪种阴影映射技术（SM、SSSM）
                //      最终的目的就是将顶点进行坐标转换并存储到_ShadowCoord阴影纹理坐标变量中
                //      需要注意的是：
                //      1.该宏会在内部使用顶点着色器中传入的结构体
                //        该结构体中顶点的命名必须是vertex
                //      2.该宏会在内部使用顶点着色器的返回结构体
                //        其中的顶点位置命名必须是pos
                //计算阴影映射纹理坐标 它会在内部去进行计算 然后将其存入 v2f中的SHADOW_COORDS中
                TRANSFER_SHADOW(v2fData);

                return v2fData;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //计算兰伯特光照颜色
                fixed3 lambertColor = getLambertFColor(normalize(i.wNormal));
                //计算BlinnPhong式高光反射颜色
                fixed3 specularColor = getSpecularColor(i.wPos, normalize(i.wNormal));

                //得到阴影衰减值
                //      该宏在片元着色器中调用，传入对应的v2f结构体对象
                //      该宏会在内部利用v2f中的 阴影纹理坐标变量(ShadowCoord)对相关纹理进行采样
                //      将采样得到的深度值进行比较，以计算出一个fixed3的阴影衰减值
                //      我们只需要使用它返回的结果和 (漫反射+高光反射) 的结果相乘即可
                //fixed3 shadow = SHADOW_ATTENUATION(i);

                //衰减值
                //fixed atten = 1;

                //利用灯光衰减和阴影衰减计算宏统一进行衰减值的计算
                UNITY_LIGHT_ATTENUATION(atten, i, i.wPos)

                //物体表面光照颜色 = 环境光颜色 + 兰伯特光照模型所得颜色 + Phong式高光反射光照模型所得颜色
                //衰减值 会和 漫反射颜色 + 高光反射颜色 后 再进行乘法运算
                fixed3 blinnPhongColor = UNITY_LIGHTMODEL_AMBIENT.rgb + (lambertColor + specularColor)*atten; 

                return fixed4(blinnPhongColor.rgb, 1);
            }
            ENDCG
        }

        //Additional Pass 附加渲染通道
        Pass
        {
            Tags { "LightMode"="ForwardAdd" }
            //线性减淡的效果 进行 光照颜色混合
            Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //用于帮助我们编译所有变体 并且保证衰减相关光照变量能够正确赋值到对应的内置变量中
            //#pragma multi_compile_fwdadd
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            //材质漫反射颜色
            fixed4 _MainColor;
            fixed4 _SpecularColor;
            float _SpecularNum;

            //顶点着色器返回出去的内容
            struct v2f
            {
                //裁剪空间下的顶点位置
                float4 pos:SV_POSITION;
                //世界空间下的法线位置
                float3 wNormal:NORMAL;
                //世界空间下的 顶点坐标 
                float3 wPos:TEXCOORD0;
                //      该宏在v2f结构体（顶点着色器返回值）中使用
                //      本质上就是声明了一个用于对阴影纹理进行采样的坐标
                //      在内部实际上就是声明了一个名为_ShadowCoord的阴影纹理坐标变量
                //      需要注意的是：
                //      在使用时 SHADOW_COORDS(2) 传入参数2
                //      表示需要时下一个可用的插值寄存器的索引值   
                //阴影坐标宏 主要用于存储阴影纹理坐标
                SHADOW_COORDS(2)
            };

            v2f vert (appdata_base v)
            {
                v2f v2fData;
                //转换模型空间下的顶点到裁剪空间中
                v2fData.pos = UnityObjectToClipPos(v.vertex);
                //转换模型空间下的法线到世界空间下
                v2fData.wNormal = UnityObjectToWorldNormal(v.normal);
                //顶点转到世界空间
                v2fData.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                //      该宏在顶点着色器函数中调用，传入对应的v2f结构体对象
                //      该宏会在内部自己判断应该使用哪种阴影映射技术（SM、SSSM）
                //      最终的目的就是将顶点进行坐标转换并存储到_ShadowCoord阴影纹理坐标变量中
                //      需要注意的是：
                //      1.该宏会在内部使用顶点着色器中传入的结构体
                //        该结构体中顶点的命名必须是vertex
                //      2.该宏会在内部使用顶点着色器的返回结构体
                //        其中的顶点位置命名必须是pos
                //计算阴影映射纹理坐标 它会在内部去进行计算 然后将其存入 v2f中的SHADOW_COORDS中
                TRANSFER_SHADOW(v2fData);

                return v2fData;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //兰伯特漫反射
                fixed3 worldNormal = normalize(i.wNormal);
                //平行光 光的方向 其实就是它的位置
                #if defined(_DIRECTIONAL_LIGHT)
                    fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                #else //点光源和聚光灯 光的方向 是 光的位置 - 顶点位置
                    fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz - i.wPos);
                #endif
                // 漫反射颜色 = 光颜色 * 属性中颜色 * max(0, dot(世界坐标系下的法线, 世界坐标系下的光方向));
                fixed3 diffuse = _LightColor0.rgb * _MainColor.rgb * max(0, dot(worldNormal, worldLightDir));
                
                //BlinnPhong高光反射
                //视角方向
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.wPos.xyz);
                //半角方向向量
                fixed3 halfDir = normalize(worldLightDir + viewDir);
                // 高光颜色 = 光颜色 * 属性中的高光颜色 * pow(max(0, dot(世界坐标系法线, 世界坐标系半角向量)), 光泽度);
                fixed3 specular = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0, dot(worldNormal, halfDir)), _SpecularNum);

                //衰减值
                //#if defined(_DIRECTIONAL_LIGHT)
                //    fixed atten = 1;
                //#elif defined(_POINT_LIGHT)
                //    //将世界坐标系下顶点转到光源空间下
                //    float3 lightCoord = mul(unity_WorldToLight, float4(i.wPos, 1)).xyz;
                //    //利用这个坐标得到距离的平方 然后再再光源纹理中隐射得到衰减值
                //    fixed atten = tex2D(_LightTexture0, dot(lightCoord,lightCoord).xx).UNITY_ATTEN_CHANNEL;
                //#elif defined(_SPOT_LIGHT)
                //    //将世界坐标系下顶点转到光源空间下 聚光灯需要用w参与后续计算
                //    float4 lightCoord = mul(unity_WorldToLight, float4(i.wPos, 1));
                //    fixed atten = (lightCoord.z > 0) * //判断在聚光灯前面吗
                //                  tex2D(_LightTexture0, lightCoord.xy / lightCoord.w + 0.5).w * //映射到大图中进行采样
                //                  tex2D(_LightTextureB0, dot(lightCoord,lightCoord).xx).UNITY_ATTEN_CHANNEL; //距离的平方采样
                //#else
                //    fixed atten = 1;
                //#endif

                //利用灯光衰减和阴影衰减计算宏统一进行衰减值的计算
                UNITY_LIGHT_ATTENUATION(atten, i, i.wPos)

                //在附加渲染通道中不需要在加上环境光颜色了 因为它只需要计算一次 在基础渲染通道中已经计算了
                return fixed4((diffuse + specular)*atten, 1);
            }
            ENDCG
        }
    }

    FallBack "Specular"
}
