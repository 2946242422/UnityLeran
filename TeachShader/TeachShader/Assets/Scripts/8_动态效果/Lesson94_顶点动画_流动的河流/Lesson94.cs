using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson94 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //实现2D河流效果的关键公式:
        //某轴位置偏移量 = sin(_Time.y * 波动频率 + 顶点某轴坐标 * 波长的倒数) * 波动幅度
        #endregion

        #region 知识补充
        //渲染标签
        //"DisableBatching" = "True"
        //主要作用：
        //是否对SubShader关闭批处理
        //我们在制作顶点动画时，有时需要关闭该Shader的批处理
        //因为我们在制作顶点动画时，有时需要使用模型空间下的数据
        //而批处理会合并所有相关的模型，这些模型各自的模型空间会丢失，导致我们无法正确使用模型空间下相关数据

        //在实现流程的2D河流效果时，我们就需要让顶点在模型空间下进行偏移
        //因此我们需要使用该标签，为该Shader关闭批处理
        #endregion

        #region 知识点一 导入资源 观察资源
        //1.导出测试用资源
        //2.观察资源模型空间轴向
        //  该模型的模型空间坐标并不符合Unity轴向标准
        //  它的上下是x轴 左右是z轴 前后是y轴
        #endregion

        #region 知识点二 流动的2D河流效果 具体实现
        //1.新建Shader，删除无用代码
        //2.声明属性、映射属性
        //  主纹理(_MainTex)
        //  叠加的颜色(_Color)
        //  波动幅度(_WaveAmplitude)
        //  波动频率(_WaveFrequency)
        //  波长的倒数(_InvWaveLength)
        //3.透明Shader相关
        //  渲染标签相关
        //  Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "DisableBatching"="True"}
        //  深度写入、透明混合相关
        //  ZWrite Off
        //  Blend SrcAlpha OneMinusSrcAlpha
        //4.结构体相关
        //  顶点和uv
        //5.顶点着色器
        //  利用理论中讲解的公式，计算对应轴向偏移位置
        //  注意，在模型空间中偏移
        //6.片元着色器
        //  直接进行颜色采样，颜色叠加
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
