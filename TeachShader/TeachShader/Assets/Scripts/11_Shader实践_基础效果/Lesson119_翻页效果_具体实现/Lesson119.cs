using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson119 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 基本原理
        //1.旋转关键点
        //利用旋转矩阵基于某一个轴进行旋转
        //注意：根据模型中心点决定平移方向
        //      先平移 再旋转 再平移回去

        //2.起伏关键点
        //利用三角函数Sin让顶点在Y轴进行位置偏移
        //并且0~90~180度之间变化时，0和180度不需要起伏感，90度时起伏感最大（页面最弯曲）
        #endregion

        #region 知识点 书本翻页效果的具体实现
        //1.新建Shader PageTurning
        //  删除无用代码
        //2.属性声明 属性映射
        //  正面纹理          _MainTex
        //  背面纹理          _BackTex
        //  翻页进度(0~180度) _AngleProgress
        //  y轴弯曲程度(0~1)  _WeightY
        //  x轴收缩程度(0~1)  _WeightY
        //  波长(0~3)         _WaveLength
        //  平移距离          _MoveDis
        //3.由于要双面渲染
        //  Cull Off
        //4.由于要使用VFACE语义，加入编译指令
        //  #pragma target 3.0
        //5.结构体
        //  顶点和UV
        //6.顶点着色器
        //  先实现基本的翻页效果
        //  1.利用sincos函数得到当前翻页精度对应的sin和cos值
        //  2.基于得到的值，构建旋转矩阵
        //  3.旋转前先平移
        //  3.基于旋转矩阵进行顶点旋转
        //  4.旋转结束再平移回去
        //  5.将处理完毕的顶点转换到裁剪空间中
        //  6.UV赋值
        //  实现起伏效果
        //  将起伏相关的计算加入到旋转矩阵之前
        //7.片元着色器
        //  通过VFACE语义判断正反面进行对应的采样即可
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
