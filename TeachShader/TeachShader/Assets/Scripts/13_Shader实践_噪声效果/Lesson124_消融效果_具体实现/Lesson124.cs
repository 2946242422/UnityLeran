using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson124 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //消融效果 基本原理
        //通过对比噪声纹理值与消融进度参数，剔除低于阈值的像素，
        //同时在边缘添加渐变颜色实现动态溶解效果。

        //1.如何剔除像素
        //clip(噪声纹理值 – 消融进度参数)
        //2.如何处理边缘
        //利用smoothstep结合消融阈值来决定在渐变纹理中采用的渐变颜色
        //利用lerp来决定在原始颜色和边缘渐变颜色中使用哪个颜色
        //利用自定义参数决定边缘
        #endregion

        #region 知识点一 准备工作
        //1.导入噪声纹理和渐变纹理
        //2.新建Shader Lesson124_Dissolve 
        //2.复制Lesson51中计算切线空间下法线纹理Shader
        #endregion

        #region 知识点二 实现消融效果
        //1.添加属性
        //  _Noise 噪声纹理
        //  _Gradient 渐变纹理
        //  _Dissolve 消融进度
        //  _Range 边缘范围
        //  添加属性映射
        //2.结构体 加入一个噪声纹理uv
        //3.片元着色器
        //  3-1:剔除效果制作
        //      从噪声纹理中采样，利用clip函数进行剔除
        //  3-2:边缘渐变采样，范围控制，颜色插值
        //      利用smoothstep函数计算出采样系数
        //      利用lerp函数决定是用哪个颜色
        #endregion

        #region 知识点三 阴影相关添加

        #endregion

        #region 知识点四 阴影消融效果处理
        //1.复制Lesson64 Shader中自定义投射阴影相关代码
        //2.加入噪声纹理和消融进度属性映射
        //3.结构体中加入uv
        //4.顶点着色器中计算uv缩放偏移
        //5.片元着色器中剔除
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
