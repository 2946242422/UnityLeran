using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson130 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 自带Shader材质属性绘制类是什么？
        //通过之前的学习
        //我们知道通过继承MaterialPropertyDrawer(材质属性绘制器) 实现的类
        //可以自定义Shader属性在Inspector窗口的外观

        //而自带Shader材质属性绘制类指的就是
        //Unity内部实现好的继承自MaterialPropertyDrawer(材质属性绘制器) 的绘制类
        //可以让我们在Shader属性语句块中直接使用
        //让属性在Inspector窗口中具备特殊样式
        //方便我们进行材质球参数设置
        #endregion

        #region 知识点二 ToggleDrawer类
        //作用：
        //将float数据以开关的形式在材质属性面板上显示
        //数值只能设置为0或1, 0为关闭, 1为开启

        //使用场景：
        //1.开启或关闭某个特效（比如是否启用边缘）
        //2.切换某些功能（比如是否使用纹理颜色）
        //3.控制条件分支逻辑
        //等等

        //用法：
        //在属性语句块中声明属性时
        //在属性前加上[Toggle]或[Toggle(自定义名称)]
        //[Toggle]_属性名("属性名", Float) = 0或1

        #endregion

        #region 知识点三 ToggleDrawer关联关键字
        //ToggleDrawer可以和关键字编译指令结合使用
        //达到在材质面板中通过Toggle禁用启用关键字的效果
        //从而切换功能或者特效等
        //它可以让开发者在材质球的Inspector窗口中通过简单的复选框启用或者禁用关键字

        //使用方式：
        //1.ToggleDrawer和关键词绑定
        //  当我们使用Toggle定义一个Float属性时
        //  Unity会自动根据属性值设置全局关键词
        //  举例：
        //  [Toggle]_ShowTex("ShowTex", Float) = 0或1
        //  默认生成的全局关键词为 _SHOWTEX_ON（勾选时为1时激活）
        //  我们也可以利用Toggle自定义关键词名[Toggle(自定义关键词名)]

        //2.关键词连接
        //  可以使用
        //  #pragma shader_feature 或 #pragma multi_compile 来关联关键词
        //  当我们在材质面板切换Toggle时，Unity会启用或禁用对应的关键词，触发对应的Shader变体

        //注意：编译指令必须与ToggleDrawer的关键词一致，才能正确切换功能
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
