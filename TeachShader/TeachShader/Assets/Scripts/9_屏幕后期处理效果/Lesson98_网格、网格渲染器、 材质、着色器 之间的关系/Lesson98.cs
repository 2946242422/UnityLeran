using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson98 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 网格、网格渲染器、材质、着色器 它们是什么
        #region 网格（Mesh）
        //网格是一个3D对象的几何数据。
        //它由顶点、边和面组成。网格描述了对象的形状和结构，定义了3D模型的轮廓。
        //网格中包含了模型的关键数据
        //比如：
        //顶点、法线、切线、纹理坐标、顶点颜色、骨骼权重、骨骼索引、网格边界等等信息
        //我们在Shader中使用的模型的数据就来自于Mesh

        //Unity中不带骨骼动画的模型网格数据一般在MeshFilter（网格过滤器）组件中进行关联
        //而带骨骼动画的模型网格数据一般在Skinned Mesh Renderer（蒙皮网格渲染器）中进行关联
        #endregion

        #region 网格渲染器（Mesh Renderer）
        //网格渲染器是Unity中的一个组件，用于将网格绘制到屏幕上
        //它主要用来
        //1.引用一个网格对象来获取几何数据，Mesh Renderer组件会自动寻找同一GameObject上
        //  Mesh Filter组件中的网格（Mesh）并将其渲染出来
        //2.引用一个或多个材质，用于定义对象的外观

        //一般不带骨骼动画的模型都使用网格渲染器来进行渲染
        //比如：
        //游戏中的建筑物，箱子，地面等等不需要骨骼动画的模型
        #endregion

        #region 蒙皮网格渲染器（Skinned Mesh Renderer）
        //蒙皮网格渲染器是一种特殊的网格渲染器，用于处理带有骨骼动画的网格。
        //它不仅处理网格的几何数据，还处理骨骼和权重，允许网格根据骨骼动画进行变形。
        //使用蒙皮网格渲染器的对象不需要再使用Mesh Filter组件
        //它可以直接关联对应的网格信息

        //一般带有动画的模型都使用蒙皮网格渲染器来进行渲染
        //比如：
        //游戏中的角色、怪物、机关等等
        #endregion

        #region 材质（Material）
        //材质我们也可以称为材质球
        //它定义了模型网格的外观
        //材质包含对一个着色器的引用，并通过一组属性（例如颜色、纹理等等信息）来配置着色器
        //一个模型可以有多个材质，每个材质应用于模型的不同部分
        #endregion

        #region 着色器（Shader）
        //是一种用于描述如何渲染图形和计算图形外观的程序
        //主要用于控制图形的颜色、光照、纹理和其他视觉效果
        //它是运行在GPU上的程序，用于计算每个像素的颜色

        //我们这套课中学习的知识都是和着色器有关的
        #endregion

        #endregion

        #region 知识点二 它们之间的关系
        //Mesh Renderer(网格渲染器)
        //  └── Mesh(网格) —— MeshFilter(网格过滤器组件进行关联)
        //        └── Geometry Data(几何数据)
        //  └── Material(材质)
        //        └── Shader(着色器)
        //        └── Properties(属性：颜色、纹理等 —— 在Shader中决定哪些属性暴露在材质上)

        //Skinned Mesh Renderer(蒙皮网格渲染器)
        //  └── Mesh(网格)
        //        └── Geometry Data(几何数据)
        //  └── Bones & Weights(骨骼与权重)
        //  └── Material(材质)
        //        └── Shader(着色器)
        //        └── Properties(属性：颜色、纹理等 —— 在Shader中决定哪些属性暴露在材质上)

        //从关系中我们可以得出
        //由于Mesh Renderer和Skinned Mesh Renderer都是Unity中的组件
        //那如果我们想要获取、修改一个对象Mesh(网格)、Material(材质)、材质上属性、材质关联的Shader(着色器)等等信息
        //都可以利用这两个组件去获取
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
