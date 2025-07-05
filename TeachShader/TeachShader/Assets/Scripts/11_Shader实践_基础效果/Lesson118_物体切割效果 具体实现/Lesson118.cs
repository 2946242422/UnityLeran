using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson118 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 
        //在片元着色器中判断片元的世界坐标是否满足切割条件（片元世界坐标和切割坐标比较）
        //如果满足则直接抛弃片元不渲染（clip）
        //判断片元在模型中的正反面，决定使用哪种纹理进行渲染（VFACE）
        #endregion

        #region 知识点一 实现物体切割效果的 Shader代码
        //1.新建Shader ObjectCutting
        //  删除无用代码
        //2.属性声明 属性映射
        //  主纹理 _MainTex 2D
        //  背面纹理 _BackTex 2D
        //  切割方向（用来控制比较x、y、z哪个轴）_CuttingDir Float
        //  是否翻转切割 _Invert Float
        //  切割位置（从C#传递过来）_CuttingPos Vector
        //3.关闭剔除 因为要两面渲染
        //4.编译指令 #pragma target 3.0 让VFACE兼容性更好 
        //5.结构体
        //  uv、顶点、世界坐标
        //6.顶点着色器函数
        //  坐标转换、纹理赋值、世界坐标转换
        //7.片元着色器函数
        //  7-1.加入VFACE语义参数
        //  7-2.根据正反面决定采样颜色
        //  7-3.根据切割方向判断是否丢弃（0代表丢弃，1代表不丢弃）
        //      可以使用step(edge, x)函数
        //      x>=edge 返回 1；x<edge 返回 0
        //  7-4.利用是否翻转切割参数决定是否反转丢弃
        //  7-5.利用clip函数丢弃片元
        //  7-6.若不丢弃 直接返回颜色
        #endregion

        #region 知识点二 实现物体切割效果的 C#代码
        //1.新建C#脚本 和Shader名一样
        //2.加入[ExecuteAlways]特性，让编辑模式下也运行，可以看到效果
        //3.声明材质球和切割位置对象
        //4.材质球初始化
        //6.在Update中不停的将切割物体位置传递给Shader
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
