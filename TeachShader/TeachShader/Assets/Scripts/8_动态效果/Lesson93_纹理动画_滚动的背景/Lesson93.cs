using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson93 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识补充
        //内置函数 frac(参数)
        //该函数的内部计算规则为：
        //frac(x) = x - floor(x)
        //一般用于保留数值的小数部分，但是负数时要注意
        //比如：
        //frac(2.5) = 2.5 - 2 = 0.5
        //frac(3.75) = 3.75 - 3 = 0.75
        //frac(-0.25) = -0.25 - (-1) = 0.75
        //frac(-3.75) = -3.75 - (-4) = 0.25

        //它的主要作用是可以帮助我们保证 uv坐标 范围在0~1之间
        //相当于
        //大于1的uv值重新从0开始向1方向取值
        //小于0的uv值重新从1开始向0方向取值
        #endregion

        #region 准备工作
        //导入测试用资源
        #endregion

        #region 知识点一 分析利用纹理坐标制作滚动的背景的原理
        //注意点：
        //滚动的背景使用的美术资源图片，往往是首尾循环相连的

        //基本原理：
        //不停地利用时间变量对uv坐标进行偏移运算
        //超过1的部分从0开始采样
        //小于0的部分从1开始采样
        #endregion

        #region 知识点二 用Shader实现滚动的背景
        //1.新建Shader，删除无用代码
        //2.声明属性，属性映射
        //  主纹理、U轴速度、V轴速度（两个速度的原因是因为图片可能竖直或水平滚动）
        //3.透明Shader
        //  往往这种滚动背景图片都会有透明区域
        //  渲染标签修改、关闭深度写入、进行透明混合
        //4.结构体
        //  顶点和纹理坐标
        //5.顶点着色器 
        //  顶点坐标转换，纹理坐标直接赋值
        //6.片元着色器
        //  利用时间和速度对uv坐标进行偏移计算
        //  利用偏移后的uv坐标进行采样
        #endregion

        #region 总结
        //Shader实现滚动的背景的关键点
        //1.纹理图片必须按规范制作，“首尾相连”
        //2.利用内置时间变量对纹理坐标进行偏移计算
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
