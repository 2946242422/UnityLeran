using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson106_GaussianBlur : PostEffectBase
{
    [Range(1,8)]
    public int downSample = 1;
    [Range(1,16)]
    public int iterations = 1;
    [Range(0,3)]
    public float blurSpread = 0.6f;

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //base.OnRenderImage(source, destination);
        if (material != null)
        {
            int rtW = source.width / downSample;
            int rtH = source.height / downSample;

            //准备一个缓存区
            RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
            //采用双线性过滤模式来缩放 可以让缩放效果更平滑
            buffer.filterMode = FilterMode.Bilinear;
            //直接缩放写入到缓存纹理中
            Graphics.Blit(source, buffer);
            //在使用材质球之前设置 模糊半径属性
            //material.SetFloat("_BlurSpread", blurSpread);

            //多次去执行 高斯模糊逻辑
            for (int i = 0; i < iterations; i++)
            {
                //如果想要模糊半径影响模糊想过更强烈 更平滑
                //一般可以在我们的迭代中进行设置 相当于每次迭代处理高斯模糊时 都在增加我们的间隔距离
                material.SetFloat("_BlurSpread", 1 + i * blurSpread);

                //又声明一个新的缓冲区
                RenderTexture buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);

                //因为我们需要用两个Pass 处理图像两次 
                //进行第一次 水平卷积计算
                Graphics.Blit(buffer, buffer1, material, 0); //Color1
                //这时 关键内容都在buffer1中 buffer没用了 释放掉
                RenderTexture.ReleaseTemporary(buffer);

                buffer = buffer1;
                buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                //进行第二次 垂直卷积计算
                Graphics.Blit(buffer, buffer1, material, 1);//在Color1的基础上乘上Color2 得到最终的高斯模糊计算结果
                                                                //释放缓存区
                RenderTexture.ReleaseTemporary(buffer);
                //buffer和buffer1指向的都是这一次高斯模糊处理的结果
                buffer = buffer1;
            }
            //在for循环中得到最终的模糊结果 然后写入到目标纹理中
            Graphics.Blit(buffer, destination);
            //释放掉缓存区
            RenderTexture.ReleaseTemporary(buffer);
        }
        else
            Graphics.Blit(source, destination);
    }
}
