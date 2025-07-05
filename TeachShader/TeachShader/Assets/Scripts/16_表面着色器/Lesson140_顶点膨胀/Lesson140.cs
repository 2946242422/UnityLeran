using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson140 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 表面着色器中如何处理顶点？
        //在编译指令中
        //#pragma surface 表面函数名 光照模型 可选额外参数
        //可选额外参数中添加
        //vertex:自定义函数名
        //函数格式：
        //void 自定义函数名(inout appdata_full v)
        #endregion

        #region 知识点一 实现顶点膨胀效果
        //直接在上节课的代码中进行修改即可
        //1.加入顶点碰撞控制参数
        //2.在编译指令中加入顶点处理函数
        //3.自定义顶点处理函数
        //4.在该函数中对顶点进行偏移
        #endregion

        #region 知识点二 感受最终颜色处理函数
        //可选额外参数中添加
        //finalcolor:自定义函数名
        //函数格式：
        //void 自定义函数名(Input IN, SurfaceOutput... o, inout fixed4 color)
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
