using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson125 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //1.水波效果 是什么
        //水波效果指在计算机图形学中模拟水面波纹的视觉效果
        //2.水波效果 基本原理
        //水波效果可以基于我们之前实现的带法线纹理的玻璃效果进行修改
        //通过添加噪声法线纹理结合Shader内置时间变量实现水波动态效果
        //加入菲涅耳计算公式实现水面的光学特性
        #endregion

        #region 知识点一 准备工作
        //1.导入噪声纹理和水面颜色纹理，设置噪声纹理配置，将其设置为从灰度中生成的法线纹理
        //2.打开之前的讲解玻璃效果时的测试场景 Lesson84
        //3.新建Shader Lesson125_WaterWave 并删除其中无用代码
        //4.复制 Lesson86_GlassRefraction 带法线纹理的玻璃效果
        //5.在场景中创建一个平面，并创建一个使用新建Shader的材质球并关联对应纹理
        #endregion

        #region 知识点二 实现水波效果
        //1.属性相关
        //  1-1.添加用于控制动态效果的x和y轴方向的速度属性
        //      _WaveXSpeed、_WaveXSpeed
        //  1-2.将玻璃种的折射属性删除，改为之后菲涅耳效果会用到的反射率
        //      删除_RefractAmount，添加_FresnelScale
        //  修改对应的属性映射
        //2.修改片元着色器
        //  2-1.将原来的法线相关采样 修改为扰动算法（对噪声法线纹理进行 + - 偏移采样，采样后叠加单位化）
        //  2-2.修改折射相关计算，将对应法线换为新发现
        //  2-3.修改反射相关计算，世界空间下法线用扰动后发现进行计算，使用它计算反射相关内容
        //  2-4.处理菲涅耳效应，复制Lesson79_FresnelBase中菲涅耳近似公式，用计算出来的系数处理最终的折射和反射相关内容
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
