using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson56 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 准备工作 导入资源

        #endregion

        #region 知识点一 透明测试是用于处理哪种透明需求的？
        //在游戏开发中
        //对象的某些部位完全透明而其他部位完全不透明
        //这种透明需求往往不需要半透明效果
        //相对比较极端，只有看得见和看不见之分
        //比如树叶、草、栅栏等等
        #endregion

        #region 知识点二 透明测试的基本原理
        //基本原理：
        //  通过一个阈值来决定哪些像素应该被保留，哪些应该被丢弃

        //具体实现：
        //  片元携带的颜色信息中的透明度（A值）

        //  不满足条件时（通常是小于某个阈值）
        //  该片元就会被舍弃，被舍弃的片元不会在进行任何处理，不会对颜色缓冲区产生任何影响

        //  满足条件时（通常是大于等于某个阈值）
        //  该片元会按照不透明物体的处理方式来处理

        //阈值判断使用的方法：
        //利用CG中的内置函数：clip(参数)
        //该函数有重载，参数类型可以是 float4 float3 float2 float 等等
        //如果传入的参数任何一个分量是负数就会舍弃当前片元
        //它的内部实现会用到一个 discard 指令，代表剔除该片元 不再参与渲染
        //void clip(float4 x)
        //{
        //   if(any(x < 0))
        //      discard;
        //}
        #endregion

        #region 知识点三 透明测试实现
        //1.我们复制 Lesson50 中颜色纹理结合光照模型的Shader(因为我们的测试资源没有法线贴图等数据)
        //2.在属性中加一个阈值_Cutoff,取值范围为0~1，用来设定用来判断的阈值。并在CG中添加属性的映射成员
        //3.将渲染队列设置为AlphaTest，并配合IgnoreProjector和RenderType一起设置
        //4.在片元着色器中获取了颜色贴图颜色后，就进行阈值判断
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
