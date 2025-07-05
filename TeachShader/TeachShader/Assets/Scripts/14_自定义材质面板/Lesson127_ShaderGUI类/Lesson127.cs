using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson127 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 自定义材质面板指的是什么
        //我们目前可以通过在Shader中添加属性的形式
        //把一些我们希望从外部设置的内容在材质球的Inspector窗口中显示

        //自定义材质面板指的就是
        //Unity除了这些默认的显示内容外，还可以让我们自定义材质球的Inspector窗口显示
        #endregion

        #region 知识点二 ShaderGUI类是什么
        //它是一个用于自定义材质 Inspector 界面 的基类
        //通过继承 ShaderGUI，你可以完全控制材质编辑界面的布局和功能
        //而不仅仅局限于 Shader 的 属性(Properties) 语句块定义的默认行为

        //ShaderGUI可以让我们：
        //1.自定义材质界面布局
        //2.通过脚本逻辑，可以基于某些属性值动态隐藏或禁用其他属性
        //3.添加高级功能，比如通过添加按钮控件，触发某些操作，如实时更新材质预览，显示调试信息等
        //4.增强材质编辑体验，使美术人员或其他开发者无需关心底层Shader实现，而是通过友好的界面快速调整材质
        //等等
        #endregion

        #region 知识点三 ShaderGUI类的基本使用
        //1.自定义C#脚本继承ShaderGUI
        //2.重写OnGUI方法
        //3.在OnGUI方法中书写自定义布局逻辑
        //4.在Shader语句块最后加入 CustomEditor "自定义C#脚本名"  

        //这样便可以让使用该Shader的材质面板使用自定义布局规则
        #endregion

        #region 知识点四 自定义材质面板
        //使用ShaderGUI自定义材质面板的核心方法就是我们重写的
        //void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) 方法
        //其中两个参数非常重要：
        //1.materialEditor:
        //  提供与材质交互的接口，比如材质属性值等
        //  关键属性：
        //  target
        //  可以利用它获取到材质球

        //  关键方法：
        //  ShaderProperty(MaterialProperty对象, "名字")
        //  可以将属性设置为对应名字，并且用默认GUI显示

        //2.properties:
        //  包含所有在Shader的属性语句块中定义的属性
        //  关键属性：
        //  displayName
        //  获取属性名

        //其他：
        //MaterialProperty 属性对象 = FindProperty("属性名", properties)
        //可以利用FindProperty方法，获取到对应属性对象
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
