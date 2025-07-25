Shader "Unlit/Lesson83_Mirror"
{
    Properties
    {
        //主纹理
        _MainTex("MainTex", 2D) = "white"{}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            //映射对应纹理属性的图片颜色相关数据
            sampler2D _MainTex;
            //映射对应纹理属性的 缩放 平(偏)移数据
            float4 _MainTex_ST; //xy代表缩放 zw代表平移

            v2f_img vert (appdata_base v)
            {
               v2f_img data;
               data.pos = UnityObjectToClipPos(v.vertex);
               //v.texcoord.xy //代表uv坐标
               //v.texcoord.zw //代表一些额外信息
               //先缩放 后平移 这个是一个固定的算法 规则如此
               //如果没有进行缩放和平移 那么 这个计算后 值是不会产生变化的
               //因为缩放默认值是1和1 ，平移默认值是0和0
               data.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;

               //由于是镜面效果，所以需要让纹理在x轴方向进行反向
               data.uv.x = 1 - data.uv.x;

               //这是另一种写法
               //TRANSFORM_TEX(v.texcoord.xy, _MainTex);
               return data;
            }

            fixed4 frag (v2f_img i) : SV_Target
            {
                //这传入的uv 是经过插值运算后的 就是每一个片元都有自己的一个uv坐标
                //这样才能精准的在贴图当中取出颜色
                fixed4 color = tex2D(_MainTex, i.uv);

                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
