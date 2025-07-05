using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson77 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 使用的新知识点
        //折射向量计算函数
        //  CG中提供了内置函数 refract 用于进行折射向量的计算
        //  refract(入射方向单位向量, 顶点法线单位向量, 入射介质折射率/射入介质折射率)
        //  便可以得到在目标介质中的折射向量

        //  该函数内部就是利用了斯涅耳定律进行的计算
        //  我们无需再自己写逻辑计算了
        #endregion

        #region 知识点二 折射的基础实现
        //1.属性声明
        //  我们将声明4个关键属性
        //  1-1:介质A折射率
        //  1-2:介质B折射率
        //  1-3:立方体纹理贴图
        //  1-4:折射程度

        //2.编译指令、内置文件、属性映射、结构体相关

        //3.顶点着色器
        //  关键步骤：
        //  3-1:顶点坐标转裁剪坐标
        //  3-2:顶点法线转世界坐标
        //  3-3:顶点坐标转世界坐标
        //  3-4:世界空间下 视角方向计算
        //  3-5:利用折射函数计算折射向量

        //4.片元着色器
        //  关键步骤：
        //  3-1:立方体纹理采样(利用texCUBE函数)
        //  3-2:结合折射程度返回最终颜色
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
