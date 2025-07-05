using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson89 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点回顾
        //1.国际象棋棋盘格规则
        //  格子的行列编号同奇同偶则为白色，不同则为黑色

        //2.数学知识回顾
        //  2-1:两个奇数相加结果为偶数
        //  2-2:两个偶数相加结果为偶数
        //  2-2:一个奇数和一个偶数相加的结果是奇数
        #endregion

        #region 补充新知识点
        //  Shader中的内置函数floor（属于UnityCG.cginc）
        //  该函数 传入一个数值 floor(数值)
        //  会对传入数值向下取整
        //  比如：
        //  floor(2.6)  返回  2
        //  floor(0.4)  返回  0
        //  floor(-2.3) 返回 -3
        #endregion

        #region 知识点 Shader代码动态计算国际象棋棋盘格纹理
        //1.新建Shader，删除无用代码
        //2.声明属性
        //  平铺数量(行列数) _TileCount
        //  格子颜色1 _Color1
        //  格子颜色2 _Color2
        //3.v2f结构体
        //  顶点和uv
        //4.顶点着色器
        //  顶点坐标转换
        //  uv直接赋值
        //5.片元着色器
        //  uv * 行列数 将0~1范围 变为 0~_TileCount范围
        //  利用floor得到行列格子编号
        //  利用奇偶相加规律得到 0、1 值，0代表同奇或同偶，1代表不同
        //  利用该值决定该像素使用哪种颜色
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
