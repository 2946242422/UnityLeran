using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson55 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 复用法线纹理贴图Shader代码（切线空间下的）

        #endregion

        #region 知识点二 将高光遮罩纹理实现融入其中
        //1.从纹理中取出对应的遮罩掩码值（颜色的RGB值都可以使用）
        //2.用该掩码值和遮罩系数(我们自己定义的)相乘得到遮罩值
        //3.用该遮罩值和高光反射计算出来的颜色相乘
        #endregion

        #region 知识点三 遮罩纹理中的RGBA值
        //对于高光遮罩纹理中的RGBA值，是非常浪费的
        //因为我们只使用其中一个值就可以得到我们想要的数据
        
        //因此对于遮罩纹理来说
        //我们可以合理的利用其中的每一个值来存储我们想要的数据

        //随着以后的学习
        //我们可以在遮罩纹理当中存储更多信息
        //比如：
        //R值代表高光遮罩数据
        //G值代表透明遮罩数据
        //B值代表特效遮罩数据
        //等等
        //甚至可以用 n 张遮挡纹理存储 4xn 个会参与 每个片元渲染计算的值
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
