using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson115 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 Unity Shader中的内置时间变量
        //float4 _Time
        //  4个分量的值分别是(t/20, t, 2t, 3t)
        //  其中t代表该游戏场景从加载开始所经过的时间
        #endregion

        #region 知识点 流光效果的具体实现
        //1.新建Shader MovingLight
        //  删除无用代码
        //2.属性声明 属性映射
        //  主纹理
        //  叠加颜色
        //  移动速度
        //3.透明相关设置
        //  渲染标签设置——渲染类型，渲染队列
        //  混合模式 用 Blend One One 直接叠加颜色 让其效果更亮 更有流光的感觉
        //  关闭剔除 两面都渲染
        //4.顶点着色器
        //  坐标转换，纹理缩放偏移
        //5.片元着色器
        //  用_Time进行u轴方向的偏移采样
        //  返回采样颜色 * 叠加颜色
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
