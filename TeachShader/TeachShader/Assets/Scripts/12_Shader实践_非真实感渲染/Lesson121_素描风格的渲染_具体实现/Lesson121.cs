using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson121 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //基本原理：
        //用漫反射系数决定采样权重，在多张具有不同密度和方向的素描纹理中
        //进行采样，并将采样结果进行叠加得到最终效果

        //关键点：
        //1 多张具有不同密度和方向的素描纹理
        //2 漫反射系数决定采样权重
        //3 采样结果进行叠加
        #endregion

        #region 知识点一 纹理资源导入、相关属性添加
        //1.在资料区下载素描纹理资源
        //2.新建Shader——Lesson121_Sketch，删除无用代码
        //3.添加属性
        //  颜色属性Color —— 用于颜色叠加
        //  平铺系数TileFactor —— 用于平铺纹理，让采样细节更多
        //  素描纹理Sketch1~6 —— 用于采样模拟素描表现效果
        //4.进行属性映射
        #endregion

        #region 知识点二 权重计算(顶点着色器函数逻辑)
        //1.v2f结构体声明
        //  顶点位置、uv、用两个fixed3记录素描纹理权重
        //2.顶点着色器函数实现
        //  2-1 顶点坐标转换
        //  2-2 uv坐标平铺缩放 让纹理*平铺系数
        //  2-3 世界空间光照方向、世界空间法线转换
        //  2-4 兰伯特漫反射光照系数计算
        //  2-5 将光照系数 从 0~1 扩充到 0~7
        //  2-6 根据系数决定素描纹理权重
        #endregion

        #region 知识点三 颜色采样(片元着色器函数逻辑)
        //在片元着色器中实现
        //1.对 1~6 张纹理进行采样 并乘以权重 得到各纹理采样颜色
        //2.根据 1~6张纹理权重计算出白色高光部分颜色
        //3.将1~6张纹理采样颜色 和 白色部分 相加得到最终叠加颜色
        #endregion

        #region 知识点四 外轮廓、阴影相关添加
        //1.外轮廓
        //  直接复用Lesson120_ToonShader中的外轮廓Pass代码
        //2.阴影相关添加
        //  加入 SHADOW_COORDS、TRANSFER_SHADOW、UNITY_LIGHT_ATTENUATION
        //  加入 FallBack "Diffuse"
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
