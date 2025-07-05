using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson71 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是标准漫反射Shader？
        //目前我们已经完成了光源和阴影的主要知识点学习
        //我们学习了多光源、阴影、光照衰减等等知识
        //已经可以在Shader中处理光和阴影相关的效果了
        //那么我们将结合所学的知识实现一个标准的漫反射Shader
        //该Shader其实就是一个
        //带有法线（世界空间中计算-全局效果的表现更准确）的基于Phong光照模型（去掉高光反射）的
        //支持多光源和阴影的Shader

        //说是标准，其实就是一个常用Shader而已
        #endregion

        #region 知识点二 制作常用漫反射Shader
        //1.新建一个Shader，取名叫BumpedDiffuse（凹凸漫反射）
        //2.复制 Lesson52 中Shader代码，粘贴到新建文件中
        //3.加入渲染标签Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        //  渲染类型设置为不透明的、渲染队列设置为几何队列（不透明的几何体通常使用该队列）
        //4.删除高光反射相关代码
        //5.加入阴影、衰减相关代码
        //6.加入附加渲染通道
        //7.加入FallBac "Diffuse"
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
