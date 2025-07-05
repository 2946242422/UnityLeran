using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson57 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 透明度混合是用来处理哪种需求的？
        //上节课我们学习的透明度测试，并不能用于实现半透明效果
        //它只存在看得见和完全看不见两种状态，一般用来处理镂空效果
        //而这节课要学习的透明度混合，主要就是用来实现半透明效果的
        #endregion

        #region 知识点二 透明度混合的基本原理
        //基本原理：
        //  关闭深度写入，开启混合，让片元颜色和颜色缓冲区中颜色进行混合计算

        //具体实现：
        //  1. 采用半透明的混合因子进行混合 Blend SrcAlpha OneMinusSrcAlpha
        //     因此
        //     目标颜色 = SrcAlpha * 源颜色 + (1-SrcAlpha)*目标颜色
        //             = 源颜色透明度 * 源颜色 + (1-源颜色透明度) * 目标颜色
        //  2. 声明一个0~1区间的_AlphaScale用于控制对象整体透明度
        #endregion

        #region 知识点三 透明度混合实现
        //1.我们复制 Lesson50 中颜色纹理结合光照模型的Shader(因为我们的测试资源没有法线贴图等数据)
        //2.在属性中加一个阈值_AlphaScale,取值范围为0~1，用来设定对象整体透明度。并在CG中添加属性的映射成员
        //3.将渲染队列设置为Transparent，并配合IgnoreProjector和RenderType一起设置
        //4.关闭深度写入Zwrite off，设置混合因子Blend SrcAlpha OneMinusSrcAlpha
        //5.在片元着色器中获取了颜色贴图颜色后，修改最后返回颜色的A值为 纹理.a * _AlphaScale
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
