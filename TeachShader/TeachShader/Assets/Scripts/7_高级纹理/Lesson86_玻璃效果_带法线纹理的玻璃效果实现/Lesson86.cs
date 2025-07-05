using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson86 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是带法线纹理的玻璃效果
        #endregion

        #region 知识点二 带法线纹理的玻璃效果实现
        //1.创建Shader 复制上节课的相关代码

        //2.将 BumpedDiffuse 标准法线漫反射Shader中关于法线相关的计算整合进来
        //  注意：不再需要_BumpScale来控制凹凸程度，我们默认法线凹凸程度最大化

        //3.修改反射向量计算规则
        //  由于法线需要从法线纹理中获取
        //  因此需要将反射向量的计算放入到片元着色器中
        #endregion

        #region 知识点三 利用切线空间法线来计算折射偏移
        //1.加入一个控制折射扭曲程度的新属性_Distortion 取值范围可以大一些
        //2.利用切线空间下法线来计算偏移值
        //  加入两行关键代码
        //  第一行
        //  float2 offset = tangentNormal.xy * _Distortion ;
        //  使用切线空间下法线的xy * 扭曲值得到一个偏移量 
        //  代表光线经过法线方向扰动后的偏移程度，确定光线折射的方向和强度

        //  第二行
        //  屏幕坐标.xy = offset * 屏幕坐标.z + 屏幕坐标.xy;
        //  用偏移量和屏幕空间深度值相乘，模拟出真实的折射效果
        //  深度值越大（即距离相机越远），折射效果越明显。
        //  这样可以实现近大远小的效果，使得物体在不同深度上的折射效果有所差异。

        //  这种计算方式是
        //  图形学前辈们通过实践总结出来的接近真实世界折射效果的方法
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
