using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson69 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 透明度测试
        //在游戏开发中
        //对象的某些部位完全透明而其他部位完全不透明
        //这种透明需求往往不需要半透明效果
        //相对比较极端，只有看得见和看不见之分
        //比如树叶、草、栅栏等等

        //基本原理：
        //  通过一个阈值来决定哪些像素应该被保留，哪些应该被丢弃
        #endregion

        #region 知识点一 准备工作
        //新建一个Shader
        //将之前书写的透明度测试相关Shader代码(Lesson59_AlphaTest_Both)复制进来

        //在场景中创建一个和之前透明度测试一样的立方体对象
        //用于测试
        #endregion

        #region 知识点二 让透明度测试Shader投射阴影
        //1.同样我们使用FallBack的形式投射阴影
        //  但是需要注意的是
        //  FallBack的内容为：Transparent/Cutout/VertexLit
        //  该默认Shader中会把裁剪后的物体深度信息写入到 阴影映射纹理和摄像机深度图中
        //  注意：
        //  使用该默认Shader计算投射阴影时，需要使用_Cutoff属性 和 _Color属性来进行相关计算
        //  因此我们必须保证我们的Shader当中有名为_Cutoff的阈值属性 和 _Color的漫反射颜色属性
        //  否则无法得到正确阴影结果

        //2.为了得到正确的阴影效果，我们需要将该物体的Cast Shadows（投射阴影）属性设置为Two Sided（双面）
        //  强制让Unity计算阴影隐射纹理时计算所有面的深度信息。
        //  因为如果不设置，默认将物体渲染到阴影隐射纹理和摄像机深度图时只会考虑物体的正面
        //  背对光源的面不会参与计算，设置为双面后即可参与计算，得到正确的结果
        #endregion

        #region 知识点三 让透明度测试Shader接收阴影
        //和我们之前处理接收阴影的方式一样
        //主要分5步骤：
        //1.编译指令
        //  #pragma multi_compile_fwdbase
        //  用于帮助我们编译所有变体 并且保证衰减相关光照变量能够正确赋值到对应的内置变量中
        //2.包含内置文件
        //  #include "AutoLight.cginc"
        //3.结构体中声明阴影坐标宏
        //  SHADOW_COORDS(n)
        //  n为下一个可用的插值寄存器的索引值（结构体前面有几个TEXCOORD就填几）
        //4.坐标转换宏
        //  TRANSFER_SHADOW(o);
        //5.Unity光照衰减计算宏
        //  UNITY_LIGHT_ATTENUATION(atten, v2f结构体, 顶点世界坐标位置);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
