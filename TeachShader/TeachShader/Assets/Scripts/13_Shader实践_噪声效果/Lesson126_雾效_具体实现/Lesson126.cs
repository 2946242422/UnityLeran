using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson126 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //噪声雾效可以基于我们之前实现的屏幕后处理效果的全局雾效进行修改
        //通过添加噪声纹理结合Shader内置时间变量实现雾的不均匀以及动态效果

        //关键点：
        //1.不均匀效果的实现
        //从噪声纹理中采样系数，让该系数参与雾的混合因子的计算中
        //2.动态效果的实现
        //自定义x轴和y轴的两个速度变量，利用Shader内置时间参数_Time.y 得到累积变化。
        //用计算结果在噪声纹理中进行偏移采样，从而达到动态效果。
        #endregion

        #region 知识点一 噪声雾效 Shader实现
        //1.打开 Lesson109场景
        //2.新建Shader Lesson126_NoiseFog 删除相关代码
        //3.复制屏幕后处理效果的全局雾效 Lesson113_FogWithDepthTexture
        //4.修改Shader代码
        //  4-1.属性添加
        //      噪声纹理 _Noise
        //      噪声值偏移系数 _NoiseAmount
        //      X、Y轴移动速度 _FogXSpeed, FogYSpeed
        //      属性映射
        //  4-2.速度计算，噪声纹理偏移采样，0~1 转 -0.5~0.5
        //  4-3.参与雾混合因子计算
        #endregion

        #region 知识点二 噪声雾效 C#实现
        //1.新建C#代码 Lesson126_NoiseFog
        //2.复制屏幕后处理效果的全局雾效C#代码 Lesson113_FogWithDepthTexture
        //3.修改C#代码
        //  3-1.添加成员变量
        //      纹理、系数、速度
        //  3-2.设置Shader参数
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
