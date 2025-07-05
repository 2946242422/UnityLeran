using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson79 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 使用的新知识点
        //Schlick 菲涅耳近似等式
        //  R(θ) = R0​ + ( 1− R0​ )( 1 − V·𝑵)^𝟓 
        //      R(θ)：对应角度的菲涅耳反射率
        //      R0：介质反射率
        //      V：视角单位向量
        //      N：法线单位向量

        //使用该等式计算菲涅耳反射率，参与颜色计算
        #endregion

        #region 知识点二 菲涅尔反射的基础实现
        //1.新建Shader 复制Lesson75_ReflectBase中代码
        //  在其基础上进行修改

        //2.属性声明
        //  将反射率变量修改为 _FresnelScale 菲涅耳中介质的反射率

        //3.结构体
        //  由于使用Schlick菲涅耳近似等式
        //  需要用到世界空间下视角方向、和法线向量
        //  因此在结构体中加入这两个变量

        //4.顶点着色器
        //  关键步骤：
        //  结构体中变量赋值

        //5.片元着色器
        //  关键步骤：
        //  利用Schlick菲涅耳近似等式计算出菲涅耳反射率 参与最终颜色计算
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
