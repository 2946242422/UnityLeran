using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson133 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是属性的特性
        //所谓的属性的特性
        //就是在属性前加上一些类似特性的限制
        //让属性在Inspector窗口中进行 有限制的显示 或者 特殊的布局显示
        #endregion

        #region 知识点二 常用的限制特性
        //[HideInInspector]
        //可以添加到 任意 属性前，是属性在材质面板上隐藏

        //[NoScaleOffset]
        //添加到 2D纹理贴图 属性前，可以让其在材质面板上隐藏纹理Tiling和Offset选项

        //[Normal]
        //添加到 2D纹理贴图 属性前，可以检测关联的纹理贴图是否为法线贴图，如果不是法线，会弹出修复提示

        //[HDR]
        //添加到 2D纹理贴图 或者 颜色 属性前，可以时属性开启高动态范围（HDR）效果，使数值突破1的限制
        //常用与自发光属性
        #endregion

        #region 知识点三 常用的装饰性特性
        //[Space] 或 [Space(数值)]
        //可以在某一个属性前添加一个空白行
        //如果填入数值，可以添加对应行数的空白行

        //[Header(文本标题)]
        //可以在材质属性面板上添加标题文字
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
