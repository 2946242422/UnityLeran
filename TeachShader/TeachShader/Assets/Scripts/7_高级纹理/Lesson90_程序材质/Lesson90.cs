using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson90 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点回顾
        //程序纹理
        //其实就是通过代码（C#或Shader代码）动态生成的图像纹理
        //我们可以在我们的材质中使用该纹理来渲染对象
        #endregion

        #region 知识点一 程序材质是什么
        //程序材质是通过算法和数学函数生成的材质（主要在Shader代码中实现）
        //它通常包括多个纹理属性和各种其他属性，用于计算模拟现实世界中的各种表面特性

        //总的来说
        //程序材质是由多个程序纹理和材质属性组合而成的
        //他们共同定义了材质的外观和物理属性，模拟出复杂的表现效果
        #endregion

        #region 知识点二 制作程序材质的工具
        //常见的制作程序材质的美术工具有：
        //1.Substance Designer（物质设计师）
        //2.Blender
        //3.Houdini
        //等等
        //其中Substance Designer是一个使用非常广泛的的程序材质创建工具
        //在游戏开发、电影制作、建筑可视化、虚拟现实领域都很常用
        //通过它制作的程序纹理不仅可以在各种游戏引擎（Unity、UE等）中使用，还可以在其他领域使用
        //因此我们在此主要讲解如何使用由Substance Designer制作的程序材质
        #endregion

        #region 知识点三 Unity中使用程序材质
        //Substance Designer制作的程序材质后缀为 .sbsar 文件

        //Unity中并不能直接使用.sbsar后缀的程序材质
        //我们需要在Asset Store中搜索Substance
        //找到一个叫：
        //Substance 3D for Unity 的插件并导入到自己的工程中即可
        //这样我们就可以在工程中直接使用.sbsar后缀的程序材质了
        #endregion

        #region 知识点四 获取程序材质
        //我们可以让美术同学使用Substance Designer制作程序材质
        //也可以在一些免费网站中获取程序材质
        //比如:
        //1.Unity资源商店
        //2.Substance Share: https://substance3d.adobe.com/community-assets
        //3.GameTextures：https://gametextures.com/
        //等等
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
