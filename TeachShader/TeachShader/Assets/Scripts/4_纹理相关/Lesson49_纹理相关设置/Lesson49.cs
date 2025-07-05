using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson49 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 关于纹理图片导入相关设置
        //在Unity四部曲Unity核心当中，我们详细的讲解过
        //图片导入相关的设置
        //若不清楚的同学学习选修课程即可
        #endregion

        #region 知识点二 重要纹理相关设置回顾
        //1.Texture Type(纹理图片类型) 和 Texture Shape(纹理图片类型)
        //  决定了我们是否能在Shader当中获取正确数据

        //2.Wrap Mode(循环模式)
        //  决定了缩放偏移的表现效果
        //  Repeat：在区块中重复纹理
        //  Clamp： 拉伸纹理的边缘
        //  Mirror：在每个整数边界上镜像纹理以创建重复图案
        //  Mirror Once：镜像纹理一次，然后将拉伸边缘纹理
        //  Per-axis：单独控制如何在U轴和V轴上包裹纹理

        //3.Filter Mode(过滤模式)
        //  决定了放大缩小纹理时看到的图片质量
        //  Point：纹理在靠近时变为块状
        //  Bilinear：纹理在靠近时变得模糊
        //  Trilinear：与Bilinear类似，但纹理也在不同的Mip级别之间模糊
        //  过滤模式在开启MipMaps根据实际表现选择，可以达到不同的表现效果
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
