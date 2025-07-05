using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson105 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 问题修正
        //上节课代码中的 
        //灰度值计算的 蓝色权重 应该是0.0722 我之前写成了 0.722
        #endregion

        #region 知识点一 什么是纯色背景功能
        //我们在边缘描边时，有时只想保留描边的边缘线
        //不想要显示原图的背景颜色
        //比如把整个背景变为白色、黑色、等等自定义颜色
        //而抛弃掉原本图片的颜色信息
        //效果就像是一张描边图片
        #endregion

        #region 知识点二 加入纯色背景功能
        //在上节课的Shader代码中进行修改

        //1.新属性声明 属性映射
        //  添加 背景颜色程度变量 _BackgroundExtent 0表示保留图片原始颜色，1表示完全抛弃图片原始颜色，0~1之间可以自己控制保留程度
        //  添加自定义背景颜色 _BackgroundColor，定义用于替换图片原始颜色的颜色

        //2.修改片元着色器
        //  利用插值运算，记录纯色背景中像素描边颜色
        //  利用插值运算，在 原始图片描边 和 纯色图片描边 之间用程度变量进行控制

        //在上节课的C#代码中进行修改
        //  添加背景颜色程度变量
        //  添加自定义背景光颜色
        //  在UpdateProperty函数中添加属性设置
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
