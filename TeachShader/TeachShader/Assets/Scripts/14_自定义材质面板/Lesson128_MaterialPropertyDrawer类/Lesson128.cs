using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson128 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 MaterialPropertyDrawer类是用来做什么的
        //MaterialPropertyDrawer(材质属性绘制器) 用于自定义材质 属性 在 材质面板中的显示和交互方式的
        //材质属性通常在Shader中通过 属性语句块 定义。
        //默认情况下，Unity提供了一些基础的控件（如滑块、颜色选择器等）
        //通过继承 MaterialPropertyDrawer(材质属性绘制器)
        //你可以为自定义Shader属性创建更加灵活和直观的控件
        #endregion

        #region 知识点二 它和ShaderGUI的区别
        //ShaderGUI是用来自定义整个材质面板的
        //而MaterialPropertyDrawer(材质属性绘制器) 是用来自定义某一个属性的
        //相当于可以更加精细的来进行属性自定义显示封装
        #endregion

        #region 知识点三 MaterialPropertyDrawer类的声明
        //1.新建一个C#脚本，继承自MaterialPropertyDrawer类
        //2.重写void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor) 方法
        //3.在其中实现UI自定义布局
        #endregion

        #region 知识点四 MaterialPropertyDrawer类的使用
        //1.配合ShaderGUI使用
        //2.独立使用
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
