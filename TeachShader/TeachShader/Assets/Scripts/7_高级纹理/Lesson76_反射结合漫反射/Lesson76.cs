using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson76 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 使用的新知识点
        //插值函数
        //  CG中提供了内置函数  lerp 用于进行插值计算
        //  lerp(a, b, t) 
        //  a:插值起点值
        //  b:插值终点值
        //  t:插值因子，0~1之间
        //  内部计算公式：a + t * (b - a)
        //  当 t = 0 时，插值结果为a
        //  当 t = 1 时，插值结果为b
        //  当 t 在 0~1 之间变化时，返回一个a到b之间的一个线性插值结果
        //  我们将利用该函数来决定反射效果的程度
        //  用它在漫反射颜色和反射颜色之间进行插值控制反射程度
        #endregion

        #region 知识点二 实现带漫反射的 反射效果
        //新建一个Shader，复制上节课的代码

        //1.属性声明
        //  我们将加入2个关键属性
        //  1-1:漫反射颜色
        //  1-2:反射颜色

        //2.v2f结构体
        //  因为要在片元着色器中处理光和阴影
        //  需要加入
        //  2-1：世界空间法线:
        //  2-2：世界空间顶点位置
        //  2-3: 阴影宏

        //3.顶点着色器
        //  关键步骤：
        //  3-1:顶点坐标转裁剪坐标
        //  3-2:顶点法线转世界坐标
        //  3-3:顶点坐标转世界坐标
        //  3-4:世界空间下 视角方向计算
        //  3-5:视角反向逆向得到反射方向
        //  3-6:阴影相关计算

        //4.片元着色器
        //  关键步骤：
        //  4-1:得到光的方向
        //  4-2:兰伯特漫反射颜色计算
        //  4-3:立方体纹理采样(利用texCUBE函数)
        //  4-4:衰减计算
        //  4-5:最终颜色计算(利用lerp函数)

        //4.FallBack "Reflective/VertexLit"
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
