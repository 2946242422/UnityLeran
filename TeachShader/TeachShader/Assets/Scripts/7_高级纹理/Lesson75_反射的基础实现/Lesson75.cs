using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson75 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 实现反射基础效果使用的新知识点
        //立方体纹理采样函数
        //  CG中提供了内置函数 texCUBE 用于进行立方体纹理采样
        //  texCUBE(立方体纹理,反射方向向量) 便可以得到在立方体纹理中的采样结果
        #endregion

        #region 知识点二 实现反射效果
        //1.属性声明
        //  我们将声明2个关键属性
        //  1-1:立方体纹理
        //  1-2:反射率（0~1）之间

        //2.顶点着色器
        //  关键步骤：
        //  2-1:顶点坐标转裁剪坐标
        //  2-2:顶点法线转世界坐标
        //  2-3:顶点坐标转世界坐标
        //  2-4:世界空间下 视角方向计算
        //  2-5:视角反向逆向得到反射方向

        //3.片元着色器
        //  关键步骤：
        //  3-1:立方体纹理采样(利用texCUBE函数)
        //  3-2:结合反射程度返回最终颜色
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
