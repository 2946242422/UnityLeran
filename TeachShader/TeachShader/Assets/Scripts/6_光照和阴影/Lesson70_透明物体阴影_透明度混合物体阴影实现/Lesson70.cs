using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson70 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 透明度混合
        //透明度测试只能处理极端效果（完全透明和完全不透明）
        //而透明度混合可以帮助我们实现半透明效果
        //它的基本原理：
        //关闭深度写入，开启混合，让片元颜色和颜色缓冲区中颜色进行混合计算
        #endregion

        #region 知识点一 准备工作
        //新建一个Shader
        //将之前书写的透明度混合相关Shader代码(Lesson59_Transparent_Both)复制进来

        //在场景中创建一个和之前透明度测试一样的立方体对象
        //用于测试
        #endregion

        #region 知识点二 透明度混合Shader处理阴影
        //根据上节课的思路
        //我们如果想要物体产生阴影效果无非就两步：
        //1.投射阴影
        //  在FallBack中关联内置的对应Shader（"Transparent/VertexLit"）
        //2.接受阴影
        //  在Shader中书写计算阴影衰减值的相关代码
        //因此对于透明度混合的Shader也会使用同样的思路去制作

        //但是！！！
        //由于透明度混合需要关闭深度写入
        //而阴影相关的处理需要用到深度值参与计算
        //因此Unity中从性能方面考虑（要计算半透明物体的的阴影表现效果是相对复杂的）
        //所有的内置半透明Shader都不会产生阴影效果（比如 Transparent/VertexLit）
        //因此
        //2-1.透明混合Shader想要 投射阴影时
        //    不管你在FallBack中写入哪种自带的半透明混合Shader
        //    都不会有投射阴影的效果，因为深度不会写入

        //2-2.透明混合Shader想要 接受阴影时
        //    Unity内置关于阴影接收计算的相关宏
        //    不会计算处理 透明混合Shader
        //    混合因子 设置为半透明效果(Blend SrcAlpha OneMinusSrcAlpha)的Shader 
        //    因为透明混合物体的深度值和遮挡关系无法直接用传统的深度缓冲和阴影贴图来处理

        //结论：
        //Unity中不会直接为透明度混合Shader处理阴影
        #endregion

        #region 知识点三 强制生成阴影
        //我们可以尝试让透明混合Shader强制投射阴影

        //在FallBack中设置一个非透明Shader，比如VertexLit、Diffuse等
        //用其中的灯光模式设置为阴影投射的渲染通道来参与阴影映射纹理的计算
        //把该物体当成一个实体物体处理

        //但是，这种效果并不真实，不建议使用
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
