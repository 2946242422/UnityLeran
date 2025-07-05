using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson116 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 模型描边效果 基本原理
        //两个Pass渲染对象，一个Pass用于渲染沿法线方向放大的模型，一个Pass用于渲染正常模型。
        //第一个Pass需要关闭深度写入
        //Shader的渲染队列应该设置为Transparent透明队列
        #endregion

        #region 知识点 模型描边效果 具体实现
        //1.新建Shader 取名 OutLine
        //  删除无用代码
        //2.属性声明
        //  边缘线颜色 _OutLineColor
        //  边缘线粗细 _OutLineWidth
        //3.复制一个Pass
        //4.编写第一个Pass
        //  4-1.关闭深度写入
        //  4-2.属性映射
        //  4-3.结构体相关
        //      不需要纹理坐标,因为颜色定死 不需要采样
        //  4-4.顶点着色器
        //      顶点朝法线方向进行偏移后再转换到裁剪空间
        //  4-5.片元着色器
        //      直接返回边缘线颜色
        //5.编写第二个Pass
        //  编写传统的纹理采样Pass进行测试即可（如果有光照相关的需求 自定添加即可）
        //6.FallBack "Diffuse"
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
