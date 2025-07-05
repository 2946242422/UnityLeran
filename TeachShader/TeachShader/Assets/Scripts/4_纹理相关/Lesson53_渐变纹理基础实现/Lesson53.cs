using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson53 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 导入测试资源
        #endregion

        #region 知识点二 渐变纹理基础Shader实现
        //关键点：
        //1.属性相关
        //  漫反射颜色
        //  渐变纹理
        //  高光反射颜色
        //  光泽度

        //2.结构体相关
        //  顶点着色器中传入：
        //  可以使用 UnityCG.cginc 中的 appdata_base
        //  其中包含了我们需要的顶点、法线相关数据
        //
        //  片元着色器中传入：
        //  自定义一个结构体
        //  其中包含 裁剪空间下坐标、世界空间下顶点坐标、世界空间下法线方向

        //3.顶点着色器回调函数中
        //  3-1 顶点坐标模型转裁剪
        //  3-2 顶点坐标模型转世界
        //  3-3 法线从模型转世界

        //4.片元着色器回调函数中
        //  4-1 计算光的方向
        //  4-2 计算半兰伯特光照后半部分公式值
        //      diffuseColor = 光源的颜色 * 材质的漫反射颜色 *（（标准化后物体表面法线向量· 标准化后光源方向向量）* 0.5 + 0.5）
        //  4-3 利用该值从渐变纹理中取出对应颜色，参与漫反射光照计算 得出漫反射颜色
        //      diffuseColor = 光源的颜色 * 材质的漫反射颜色 * 渐变纹理中取出的颜色
        //  4-4 计算Blinn Phong光照模型，其中的漫反射光照颜色使用上一步计算出来的颜色
        #endregion

        #region 知识点三 修改渐变纹理设置 避免黑点出现
        //避免渐变纹理接缝处有黑点
        //我们需要将 Wrap Mode(循环模式)切换为 Clamp(拉伸模式)

        //出现黑点的原因是：
        //浮点数计算可能存在误差，会出现超过1的值（1.00001）
        //如果使用Repeat(重复模式)，会舍弃整数部分，保留小数0.00001
        //这时对应的颜色会是最左边的值，因此会出现黑色
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
