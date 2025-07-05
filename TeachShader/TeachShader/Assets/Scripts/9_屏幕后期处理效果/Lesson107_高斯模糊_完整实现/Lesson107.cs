using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson107 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 高斯模糊效果的计算方式优化
        //想要图片变模糊，那么需要扩大高斯滤波核的大小，越大越模糊
        //如果通过扩大高斯滤波核的大小来达到更模糊的目的，付出的代价就是会更加消耗性能
        //因此在Shader中我们不会提供控制高斯滤波核大小的参数，我们的滤波核始终会使用5x5大小的
        //因此，我们就只能使用其他方式来控制模糊程度了，我们一般会使用以下三种方式：
        //1.控制缩放纹理大小
        //2.控制模糊代码执行次数
        //3.控制纹理采样间隔距离
        #endregion

        #region 知识点一 添加控制纹理大小参数
        //在高斯模糊效果的C#代码中加入一个控制缩放的参数
        //它主要是用来降低采样质量的，因此取名叫 downSample（降低采样）

        //如何使用该参数：
        //在OnRenderImage函数中
        //我们使用RenderT.GetTemporary获取渲染纹理缓存区时
        //用源纹理尺寸除以 downSample
        //这样在调用Graphics.Blit进行图像复制处理时
        //相当于就将源纹理缩小了，同时在缩小的过程过程中还会用材质球进行效果处理

        //注意：
        //在进行复制处理之前，我们可以设置渲染纹理缓存对象的 缩放过滤模式
        //buffer.filterMode = FilterMode.Bilinear;

        //FilterMode.Point:点过滤。不进行插值。每个像素都直接从最近的纹理像素获取颜色
        //FilterMode.Bilinear：双线性过滤。它在纹理采样时使用相邻四个纹理像素的加权平均值进行插值，以生成更平滑的图像
        //FilterMode.Trilinear：三线性过滤。它在双线性过滤的基础上增加了在不同 MIP 贴图级别之间的插值。
        #endregion

        #region 知识点二 添加控制模糊代码执行次数参数
        //在高斯模糊效果的C#代码中加入一个控制模糊代码执行次数的参数
        //它主要是用来多次执行材质球中的两个Pass，因此取名叫 iteration(迭代)

        //如何使用该参数：
        //在OnRenderImage函数中
        //我们使用一个for循环，来对原图像进行多次高斯模糊效果处理

        //注意：
        //要保证每次使用完 RenderT.GetTemporary 分配的缓存区
        //都要使用 RenderTexture.ReleaseTemporary 函数将其释放
        #endregion

        #region 知识点三 添加控制纹理采样间隔距离
        //在高斯模糊效果的Shader代码中加入一个控制纹理采样间隔距离的属性
        //它主要是用来控制间隔多少单位偏移uv坐标，因此取名叫 _BlurSpread（模糊半径）

        //如何使用该参数：
        //在顶点着色器进行uv坐标偏移时，乘以该属性，可以通过它控制偏移的多少

        //注意：
        //理论上来说像素是1个单位1个单位偏移的
        //_BlurSpread应该为整数变化
        //但是为了更精细的控制模糊程度，我们可以让其为小数
        //小数变化可以更细微的调整模糊程序
        #endregion

        #region 总结
        //我们并没有通过改变高斯滤波核的大小来控制最终的模糊程度
        //而是通过以上三种方式
        //虽然这样并不符合高斯模糊的理论
        //但是这样更加的高效简单，灵活性也更强，效果也是可以接受的
        //这样更加印证了
        //图像学中，只要最终的效果是好的，那么不必严格遵循数学和物理规则
        //我们应该更多的从效果优先、性能优先、开发效率优先的方向去解决问题
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
