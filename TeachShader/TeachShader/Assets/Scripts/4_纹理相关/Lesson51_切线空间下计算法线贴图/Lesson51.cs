using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson51 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 导入测试资源
        //对颜色(单张)纹理和发现纹理进行相关设置
        #endregion

        #region 知识点二 在切线空间下计算 实现法线纹理Shader
        //关键点：
        //1.属性相关
        //  漫反射颜色
        //  单张纹理
        //  法线纹理
        //  凹凸程度
        //  高光反射颜色
        //  光泽度

        //2.结构体相关
        //  顶点着色器中传入：
        //  可以使用 UnityCG.cginc 中的 appdata_full
        //  其中包含了我们需要的顶点、法线、切线、纹理坐标相关数据
        //
        //  片元着色器中传入：
        //  自定义一个结构体
        //  其中包含 裁剪空间下坐标、uv坐标、光的方向、视角的方向

        //3.顶点着色器回调函数中
        //  2-1 顶点坐标模型转裁剪
        //  2-2 单张纹理和法线纹理 UV坐标缩放偏移计算
        //  2-3 副切线计算
        //      用模型空间中的法线和切线进行叉乘 再乘以切线中的w（确定副切线方向）
        //  2-4 构建模型空间到切线空间的变换矩阵
        //      ——  切线  ——
        //      —— 副切线  ——
        //      ——  法线  ——
        //  2-5 将光照方向和视角方向转换到模型空间（利用ObjSpaceLightDir和ObjSpaceViewDir内置函数）
        //  2-6 将光照方向和视角方向转换到切线空间（利用变换矩阵进行乘法运算）

        //4.片元着色器回调函数中
        //  3-1 取出法线贴图中的法线信息（利用纹理采样函数tex2D）
        //  3-2 利用内置的UnpackNormal函数对法线信息进行逆运算以及可能的解压
        //  3-3 用得到的切线空间的法线数据 乘以 BumpScale 来控制凹凸程度
        //  3-4 得到单张纹理颜色和漫反射颜色的叠加颜色
        //  3-5 用切线空间下的 光方向、视角方向、法线方向 进行Blinn Phong光照模型计算
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
