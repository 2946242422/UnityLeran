using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson106 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //利用两个一维高斯滤波核来计算高斯模糊时
        //对于5x5的滤波核，它其中的元素，不管是水平还是竖直方向，都是
        //(0.0545, 0.2442, 0.4026, 0.2442, 0.0545)
        //而其中主要的数值就三个
        //0.4026, 0.2442, 0.0545
        //我们将利用该高斯核来进行卷积计算
        #endregion

        #region 知识补充
        //在实现高斯模糊效果时，
        //我们将在Shader中利用两个Pass来分别计算水平卷积和竖直卷积
        //而两个Pass会存在相同的代码
        //我们将使用一个新的预处理指令
        //CGINCLUDE
        //.... 
        //中间包裹CG代码
        //....
        //ENDCG
        //它写在SubShader语句块中，Pass外

        //它的作用是用于封装共享代码
        //可以在其中定义常量、函数、结构体、宏等等内容
        //这些封装起来的代码可以在同一个Shader文件中的多个Pass中使用
        //也可以在其他Shader文件中引用
        //使用它可以避免我们重复编写一些相同的代码
        //从而提高代码复用性和可维护性
        #endregion

        #region 知识点一 实现高斯模糊基础效果的制作思路
        //在Shader中写两个Pass
        //一个Pass用来计算 水平方向卷积
        //一个Pass用来计算 竖直方向卷积
        //两个Pass的区别：
        //  顶点着色器中计算的uv偏移位置不同，一个水平偏移，一个竖直偏移

        //两个Pass的共同点：
        //  1.使用的内置文件相同
        //  2.使用的属性相同
        //  3.片元着色器的计算规则可以相同
        //      我们可以用uv数组存储5个像素的UV坐标偏移
        //      数组中存储的像素UV偏移分别为
        //      index       0  1  2  3  4
        //      x或y偏移    0  1 -1  2 -2
        //      其中
        //      第0个元素 对应的高斯核元素为 0.4026
        //      第1,2个元素 对应的高斯核元素为 0.2442
        //      第3,4个元素 对应的高斯核元素为 0.0545
        //      那么不管竖直还是水平可以统一一套计算规则进行计算
        #endregion

        #region 知识点二 实现 高斯模糊基础屏幕后期处理效果 对应 Shader
        //1.新建Shader,取名为高斯模糊GaussianBlur,删除无用代码
        //2.声明变量
        //  主纹理 _MainTex
        //3.利用补充知识 CGINCLUDE ... ENDCG
        //  实现两个Pass共享的代码
        //  2-1.内置文件引用
        //  2-2.属性映射,注意映射纹素，需要用于uv偏移计算
        //  2-3.结构体
        //      顶点和uv数组（用于存储5个像素的uv偏移）
        //  2-4.两个顶点着色器函数
        //      一个水平偏移采样
        //      一个竖直偏移采样
        //  2-5.片元着色器
        //      共同的卷积计算方式，对位相乘后相加
        //4.屏幕后处理效果标配
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //5.实现两个Pass
        //  主要用编译指令指明顶点和片元着色器调用的函数即可
        #endregion

        #region 知识点三 实现 高斯模糊基础屏幕后期处理效果 对应 C#
        //补充知识
        //由于我们需要用两个Pass对图像进行处理
        //相当于先让捕获的图像进行水平卷积计算得到一个结果
        //再用这个结果进行竖直卷积计算得到最终结果
        //因此我们需要利用Graphics.Blit进行两次Pass代码的执行
        //所以我们需要一个中间纹理缓存区，用于记录中间的处理结果
        //我们需要用到
        //RenderTexture.GetTemporary方法
        //它的作用是获取一个临时的RenderTexture对象，我们可以利用它来存储中间结果
        //我们使用它传入3个参数的重载
        //RenderTexture.GetTemporary(纹理宽、纹理高、深度缓冲-一般填0即可）
        //需要注意的是
        //使用该方法返回的 RenderTexture对象
        //需要配合使用 RenderTexture.ReleaseTemporary(对象) 方法来释放该对象缓存

        //1.创建C#脚本，名为高斯模糊GaussianBlur
        //2.继承屏幕后处理基类PostEffectBase
        //3.重写OnRenderImage函数
        //4.在其中利用Graphics.Blit、RenderTexture.GetTemporary、 RenderTexture.ReleaseTemporary
        //  函数对纹理进行两次Pass处理
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
