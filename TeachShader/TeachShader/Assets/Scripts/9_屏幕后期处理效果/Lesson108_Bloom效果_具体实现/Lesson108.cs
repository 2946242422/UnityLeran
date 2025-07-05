using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson108 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 Bloom效果基本原理
        //利用4个Pass进行3个处理步骤
        //提取(1个Pass) ：提取原图像中的亮度区域存储到一张新纹理中
        //模糊(2个Pass) ：将提取出来的纹理进行模糊处理（一般采用高斯模糊）
        //合成(1个Pass) ：将模糊处理后的亮度纹理和源纹理进行颜色叠
        #endregion

        #region 知识点 Bloom效果具体实现

        #region 准备工作
        //新建Shader，取名 Bloom ，删除无用代码
        #endregion

        #region 第一步 提取
        //主要目的：提取原图像中的亮度区域存储到一张新纹理中
        //Shader代码
        //1.声明属性
        //  主纹理 _MainTex
        //  亮度区域纹理 _Bloom
        //  亮度阈值 _LuminanceThreshold
        //2.在CGINCLUDE...ENDCG中实现共享CG代码
        //  2-1：属性映射
        //  2-2：结构体（顶点，uv）
        //  2-3: 灰度值（亮度值）计算函数
        //3.屏幕后处理标配
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //4.提取Pass 实现
        //  顶点着色器 
        //      顶点转换、UV赋值
        //  片元着色器
        //      颜色采样、亮度贡献值计算、颜色*亮度贡献值

        //C#代码
        //1.创建C#脚本，名为Bloom
        //2.继承屏幕后处理基类PostEffectBase
        //3.声明亮度阈值成员变量
        //3.重写OnRenderImage函数
        //5.设置材质球的亮度阈值
        //4.在其中利用
        //  Graphics.Blit、RenderTexture.GetTemporary、 RenderTexture.ReleaseTemporary
        //  函数对纹理进行Pass处理 
        #endregion

        #region 第二步 模糊
        //主要目的：将提取出来的纹理进行模糊处理（一般采用高斯模糊）
        //Shader代码
        //1.添加模糊半径属性 _BlurSize，进行属性映射（注意：需要用到纹素）
        //2.修改之前高斯模糊Shader，为其中的两个Pass命名
        //3.在Bloom Shader中，利用UsePass 复用 高斯模糊Shader中两个Pass

        //C#代码
        //1.复制高斯模糊中的3个属性
        //2.复制高斯模糊中C#代码中处理高斯模糊的逻辑
        //3.将模糊处理后的纹理，存储_Bloom纹理属性
        #endregion

        #region 第三步 合成
        //主要目的：将模糊处理后的亮度纹理和源纹理进行颜色叠加
        //Shader代码
        //合并Pass实现
        //1.结构体
        //  顶点坐标，4维的uv（xy存主纹理，wz存亮度提取纹理）
        //2.顶点着色器
        //  顶点坐标转换，纹理坐标赋值
        //
        //  注意：亮度纹理的uv坐标需要判断是否进行Y轴翻转
        //  因为使用RenderTexture写入到Shader的纹理变量时
        //  Unity可能会对其进行Y轴翻转
        //  我们可以利用Unity提供的预处理宏进行判断
        //  #if UNITY_UV_STARTS_AT_TOP
        //  
        //  #endif
        //  如果这个宏被定义，说明当前平台的纹理坐标系的Y轴原点在顶部
        //  还可以在该宏中用纹素进行判断
        //  如果纹素的 y 小于0，为负数，表示需要对Y轴进行调整
        //  配合起来使用就是
        //  #if UNITY_UV_STARTS_AT_TOP
        //  if (_MainTex_TexelSize.y < 0.0)
        //      翻转uv的y轴坐标
        //  #endif
        //  主纹理不需要我们进行额外处理，一般Unity会自动处理
        //  一般只需要在使用RenderTexture时才考虑该问题

        //3.片元着色器
        //  两个纹理颜色采样后相加
        //4.FallBack Off

        //C#代码
        //对源纹理进行合并处理
        #endregion

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
