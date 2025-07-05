using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson131 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 EnumDrawer的使用
        //作用：
        //EnumDrawer是Unity自带的继承自MaterialPropertyDrawer(材质属性绘制器) 的类
        //使用它修饰属性，可以让该属性在Inspector窗口中显示一个下拉列表，供用户选择

        //使用方式：
        //[Enum(显示名1, 值1, 显示名2, 值2, ...)] 属性名("显示名称", Float) = 默认值
        #endregion

        #region 知识点二 KeywordEnumDrawer的使用
        //作用：
        //KeywordEnumDrawer同样是Unity自带的继承自MaterialPropertyDrawer(材质属性绘制器) 的类
        //主要使用它和关键词建立联系

        //使用方式：
        //[KeywordEnum(显示名1, 显示名2 ...)] 属性名("显示名称", Float) = 0
        //当我们使用它是，会自动生成对应关键字
        //_属性名_显示名1、_属性名_显示名2 ....
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
