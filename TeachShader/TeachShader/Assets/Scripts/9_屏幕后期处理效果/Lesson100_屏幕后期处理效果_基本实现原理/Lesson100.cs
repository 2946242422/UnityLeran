using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson100 : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是屏幕后期处理效果
        //屏幕后期处理效果（ Screen Post-Processing Effects）是一种在渲染管线的最后阶段应用的视觉效果
        //允许你在场景渲染完成后对最终图像进行各种调整和效果处理，从而增强视觉体验
        //常见的屏幕后期处理效果有：
        //景深、模糊、色彩调整 等等

        //说人话：
        //屏幕后期处理效果就是当游戏画面渲染完毕后
        //通过获取到该画面信息进行额外的效果处理
        #endregion

        #region 知识点二 Unity中 屏幕后期处理效果的 基本实现原理
        //从知识点一中我们可以知道
        //想要完成屏幕后期处理效果
        //最关键的问题在于
        //1.如何获取 游戏画面渲染完毕后的画面信息
        //2.如何为 获取到的画面信息添加自定义效果
        //只要搞清楚这两点，自然就明白了基本实现原理

        //1.如何获取 游戏画面渲染完毕后的画面信息
        //  我们之前在学习渲染纹理时学习过
        //  在Unity中获取渲染纹理的常用方法有三种
        //  RenderTexture、GrabPass、OnRenderImage
        //  我们在处理屏幕后期处理效果时会使用
        //  OnRenderImage函数来获取 游戏画面渲染完毕后的画面信息

        //2.如何为 获取到的画面信息添加自定义效果
        //  主要思路是将获取到的游戏画面作为 自定义Shader的主纹理
        //  通过自定义Shader利用捕获的画面来实现自定义效果
        #endregion

        #region 知识点三 捕获画面的关键——OnRenderImage函数
        //OnRenderImage函数
        //它是在继承了MonoBehaviour的脚本中能够被自动调用的函数（类似生命周期函数）
        //它会在图像的渲染操作完成后调用
        //它的固定写法是：
        //void OnRenderImage(RenderTexture source, RenderTexture destination)
        //第一个参数：源渲染纹理，当前渲染得到的屏幕图像存储在该参数当中
        //第二个参数：目标渲染纹理，将经过处理后的图像写入到目标纹理中用于最终的显示

        //通过该函数我们便可以得到当前渲染的游戏画面
        //并在该函数中对画面对应的渲染纹理进行处理后用于最终显示

        //注意：
        //  该函数得到的源纹理默认是在所有的不透明和透明的Pass执行完毕后调用的
        //  基于该源纹理进行修改会对游戏场景中所有游戏对象产生影响
        //  如果你想要在不透明的Pass执行完毕后就调用该函数，只需要在该函数前加上特性
        //  [ImageEffectOpaque]
        //  这样就不会对透明物体产生影响
        #endregion

        #region 知识点四 实现效果的关键——Graphics.Blit函数
        //Graphics.Blit函数
        //用于将一个图像从一个纹理复制到另一个纹理
        //同时可以在这个过程中用着色器对图像进行处理
        //它有很多重载，我们主要讲解几个常用的：

        //1.将源纹理直接复制到目标纹理
        //  Graphics.Blit (Texture source, RenderTexture dest)

        //2.将源纹理复制到目标纹理并应用一个材质
        //  Graphics.Blit (Texture source, RenderTexture dest, Material mat, int pass= -1);
        //  source源纹理会被传递给mat材质中Shader中名为_MainTex的纹理属性用于进行处理
        //  pass参数默认值为-1，表示会依次调用Shader内的所有Pass进行处理，否则，只会调用给定索引的Pass
        #endregion

        #region 总结
        //屏幕后期处理效果的基本实现原理
        //就是利用 OnRenderImage函数 和 Graphics.Blit函数
        //来获取当前屏幕画面并利用Shader对该纹理进行自定义处理
        #endregion
    }
    //加入该特性 就不会对透明物体产生影响
    //[ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        ////1.将源纹理直接复制到目标纹理
        //Graphics.Blit(source, destination);
        //把源纹理 通过 材质球当中的Shader进行效果处理 然后写入到目标纹理中 最终呈现在屏幕上
        Graphics.Blit(source, destination, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
