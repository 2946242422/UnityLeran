using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson78 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点 实现带漫反射和阴影的 折射效果
        //1.新建一个Shader，复制上节课的代码

        //2.将折射率A和折射率B 合并为一个 折射率比值变量
        //  以后在外部算好了传入，避免内部计算浪费性能

        //3.折射效果结合漫反射和阴影的做法
        //  和之前反射几乎一模一样
        //  我们可以直接复制之前的代码
        //  避免书写重复内容

        //  3-1:属性相关代码复制
        //      漫反射颜色和折射颜色
        //  3-2:渲染路径、编译指令、内置文件复制
        //      属性映射复制修改
        //  3-3:结构体相关内容复制
        //  3-4:顶点着色器相关内容修改
        //  3-5:片元着色器相关内容复制修改
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
