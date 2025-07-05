using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson87 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 程序纹理是什么
        //程序纹理（Procedural Textures）
        //顾名思义
        //就是通过程序代码生成的纹理

        //我们之前学习的大部分纹理，一般都是美术同学提前制作好的图片
        //比如
        //颜色纹理、法线纹理、渐变纹理、高光遮罩纹理等等
        //就算是在高级纹理中学习的立方体纹理、渲染纹理，他们也是根据摄像机看到的内容生成的

        //而程序纹理是由我们程序员编写代码，动态生成的纹理图片
        //它的可控性和自由度，都远远大于我们之前学习的其他纹理相关内容
        #endregion

        #region 知识点二 程序纹理如何生成
        //一般生成程序纹理由两种方式：
        //1.通过C#脚本生成纹理后传递给Shader
        //2.直接在Shader代码中自定义逻辑生成纹理

        //我们将在之后的课程当中分别讲解用这两种方式
        //我们将以动态生成国际象棋棋盘格举例
        #endregion

        #region 知识点三 程序纹理的好处
        //程序纹理由于是由我们程序员写代码动态生成的
        //因此它具备以下优点：
        //1.由于是动态生成，不需要存储大文件，可以在运行时生成任意分辨率的纹理
        //2.可以根据需求调整自定义参数，实时的更改纹理外观
        //3.通过适当的函数设计，可以生成无缝平铺的纹理

        //总体而言
        //程序纹理的最明显好处就是自由度高，可控性强
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
