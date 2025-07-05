using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson80 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点 菲涅尔反射结合漫反射
        //由于菲涅耳反射是基于反射的
        //因此我们在之前实现的反射结合漫反射的基础上修改即可

        //1.新建Shader 复制LLesson76_Reflection的代码

        //2.修改属性
        //  2-1:去掉反射颜色
        //  2-2:修改反射率为菲涅耳相关的R0反射率

        //3.修改v2f结构体
        //  加入视角方向（需要再片元着色器中用菲涅耳近似公式参与计算）

        //4.顶点着色器
        //  修改视角方向临时变量

        //5.片元着色器
        //  5-1:计算菲涅耳反射率
        //  5-2:用菲涅耳反射率参与lerp计算
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
