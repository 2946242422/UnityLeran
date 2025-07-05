using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson81 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 渲染目标纹理是什么？
        //渲染目标纹理（Render Target Texture）是一种特殊类型的纹理
        //一般摄像机的渲染结果会输出到颜色缓冲区中
        //最终渲染到设备屏幕上，让玩家通过屏幕看见游戏画面

        //而渲染目标纹理允许我们将渲染结果直接写入到某一张纹理中
        //我们可以利用这个纹理来处理各种特殊效果
        //比如：镜子、玻璃、屏幕后处理、阴影映射等等

        //在Unity中
        //渲染目标纹理（Render Target Texture）通常和渲染纹理（Render Texture）
        //可以互换使用，指的相同的概念。
        //在实际使用中，我们更常听到的是"渲染纹理"这个术语

        //说人话：
        //渲染纹理就是
        //将渲染结果存储到一个纹理对象中，以便在后续的渲染步骤中使用

        //它的作用体现在：
        //我们在进行Shader开发时
        //我们经常会希望某摄像机不要直接将结果渲染到屏幕上
        //而是可以让我们得到它的渲染结果进行二次处理或利用
        #endregion

        #region 知识点二 Unity中的渲染目标纹理
        //常用方法

        //1.渲染纹理（Render Texture）
        //  Unity引擎中提供的一种专门的纹理类型
        //  我们可以在Project窗口中右键Create——>Custom Render Texturn创建
        //  我们只需要把该纹理进行相关的设置，然后关联到摄像机组件的Target Texture中即可
        //  这样对应摄像机渲染的内容就会直接写入到该纹理中
        //  我们便可以使用它来进行相关操作
        //  注意：除了手动创建，我们也可以通过代码创建并关联

        //2.GrabPass
        //  在Unity Shader当中
        //  我们可以在Pass渲染通道中使用 GrabPass 抓取指令
        //  捕获当前屏幕内容并将其保存为纹理，以便在后续的渲染过程中使用

        //3.OnRenderImage
        //  在继承了MonoBehaviour的脚本中
        //  我们可以使用OnRenderImage来获取摄像机渲染完成的图像
        //  该函数一般用于实现自定义的图像后处理效果
        //  相当于将摄像机渲染完成的图像进行二次处理

        //等等
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
