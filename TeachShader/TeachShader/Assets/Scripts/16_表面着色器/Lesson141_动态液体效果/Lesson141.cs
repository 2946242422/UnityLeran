using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson141 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 动态液体效果 基本原理
        //1.如何被容器装载 —— 两个模型，外部透明，内部动态液体
        //2.如何透明渲染 —— 透明混合相关设置
        //3.如何剔除像素 —— 将模型空间中心点作为参考点，将其转换到世界空间下
        //  再用模型当前世界空间下的点和它进行减法运算
        //  如果判断点在参考点上方的我们便对其进行剔除
        //  可以加入自定义变量控制液面高度
        //4.如何模拟波纹效果 —— 使用流动的河流的相关公式
        //                    某轴位置偏移量 = sin( _Time.y * 波动频率 + 顶点某轴坐标 * 波长的倒数) * 波动幅度
        #endregion

        #region 知识点一 动态液体效果 具体实现
        //1.新建表面着色器 DynamicLiquid(动态液体)
        //2.删除不必要的代码
        //3.声明属性以及属性映射
        //  液体颜色 _Color
        //  高光颜色和光滑度(rgb做颜色 a做光滑度) _Specular
        //  液体高度 _Height
        //  波纹变化速度 _Speed
        //  波动幅度 _WavaAmplitude
        //  波动频率 _WavaFrequency
        //  波长的倒数 _InvWaveLength
        //4.透明混合相关设置
        //  RenderType和Queue为Transparent
        //  Blend DstColor SrcColor
        //  Zwrite Off
        //5.编译指令设置
        //  光照模型我们使用StandardSpecular 并且不要阴影 noshadow
        //6.输入结构体
        //  只需要当前像素的世界坐标位置
        //7.实现表面函数
        //  7-1:模型中心点转世界坐标
        //  7-2:计算中心点和像素点y轴坐标差
        //  7-3:像素剔除
        //  7-4:波纹效果偏移计算
        //  7-5:漫反射颜色、高光颜色、光滑度设置
        #endregion

        #region 知识点二 动态液体效果 的使用
        //1.创建两个胶囊体,一大一小，小的做为大的子对象
        //2.大的胶囊体，用Unity自带Shader设置为透明的，类似玻璃容器
        //3.小的胶囊体，用动态液体效果制作为容器中的液体
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
