using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson92 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点回顾
        //1.float4 _Time
        //  4个分量的值分别是(t/20, t, 2t, 3t)
        //  其中t代表该游戏场景从加载开始缩经过的时间

        //2.float4 _SinTime
        //  4个分量的值分别是(t/8, t/4, t/2, t)
        //  其中t代表 游戏运行的时间的正弦值

        //3.float4 _CosTime
        //  4个分量的值分别是(t/8, t/4, t/2, t)
        //  其中t代表 游戏运行的时间的余弦值

        //4.float4 unity_DeltaTime
        //  4个分量的值分别是(dt, 1/dt, smoothDt, 1/smoothDt)
        //  dt代表帧间隔时间（上一帧到当前帧间隔时间）
        //  smoothDt是平滑处理过的时间间隔，对帧间隔时间进行某种平滑算法处理后的结果
        #endregion

        #region 准备工作
        //1.在资料区下载序列帧图集资源
        //2.新建一个测试场景
        #endregion

        #region 知识点一 分析利用纹理坐标制作序列帧动画的原理
        //关键点
        //1.UV坐标范围0~1，原点为图片左下角
        //2.图集序列帧动画播放顺序为从左到右，从上到下

        //分析问题
        //1.如何得到当前应该播放哪一帧动画？
        //2.如何将采样规则从0~1修改为在指定范围内采样？

        //问题解决思路
        //1.用内置时间参数 _Time.y 参与计算得到具体哪一帧
        //  时间是不停增长的数值，用它对总帧数取余，便可以循环获取到当前帧数
        //2.利用时间得到当前应该绘制哪一帧后
        //  我们只需要确认从当前小图片中，采样开始位置，采样范围即可
        //  采样开始位置，可以利用当前帧和行列一起计算
        //  采样范围可以将0~1范围 缩放转换到 小图范围内
        #endregion

        #region 知识点二 用Shader实现序列帧动画
        //1.新建Shader 删除无用代码
        //2.声明属性，进行属性映射
        //  主纹理、图集行列、序列帧切换速度
        //3.透明Shader
        //  设置渲染标签
        //  Tags { "RenderType"="Opaque" "IgnoreProjector"="True" "Queue"="Transparent" }
        //  关闭深度写入，开启混合
        //  ZWrite Off
        //  Blend SrcAlpha OneMinusSrcAlpha
        //4.结构体
        //  只需要顶点坐标和纹理坐标
        //5.顶点着色器
        //  只需要进行坐标转换和纹理坐标赋值
        //6.片元着色器
        //  6-1:利用时间计算帧数
        //  6-2:利用帧数计算当前 uv采样起始位置（得到小图片uv起始位置）
        //  6-3:计算uv缩放比例（将0~1 转换到 0~1/n）
        //  6-4:进行uv偏移计算（在小图片格子中采样）
        //  6-5:采样
        #endregion

        #region 总结
        //Shader实现序列帧动画的关键点是
        //UV坐标原点为左下角，而序列帧图集“原点”为左上角
        //我们需要注意采样开始位置的转换
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
