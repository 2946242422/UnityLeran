using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson132 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 PowerSliderDrawer的使用
        //作用：
        //PowerSliderDrawer是Unity自带的继承自MaterialPropertyDrawer(材质属性绘制器) 的类
        //使用它修饰属性，可以让该属性在Inspector窗口中显示为一个指数滑块
        //相比普通的线性滑块，它可以更方便的调整那些变化范围较大、非线性分布的参数
        //比如
        //当属性的值范围较大，但其效果对某些特定范围特别敏感时，可以通过 PowerSlider 进行更直观的调整

        //它和普通滑块属性的区别是：
        //普通滑块属性默认是线性分布，即滑块的位置与参数值成正比
        //PowerSlider 提供了 非线性映射，适合用在参数值对效果影响不均匀的场景
        //我们可以用该类修饰
        //1.光照强度： 调整光的强弱（低强度变化显著，高强度变化较平滑）。
        //2.对比度： 调整图像的对比效果。
        //3.模糊程度： 调整模糊范围，尤其是高斯模糊。
        //等等

        //使用方式：
        //[PowerSlider(指数)] 属性名 ("显示名称", Range(最小值,最大值)) = 默认值

        //对应的函数曲线为 
        //y = x的指数次方
        //x为滑块所在位置
        //y为属性的数值
        #endregion

        #region 知识点二 IntRangeDrawer的使用
        //作用：
        //IntRangeDrawer同样是Unity自带的继承自MaterialPropertyDrawer(材质属性绘制器) 的类
        //使用它修饰属性，可以让该属性在Inspector窗口中显示为一个整数范围滑块

        //使用方式：
        //[IntRange] 属性名 ("显示名称", Range(最小值,最大值)) = 默认值
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
