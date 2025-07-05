using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson50 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点 单张纹理结合BlinnPhong光照模型
        //在计算时，有以下的3点注意点
        //1.纹理颜色需要和漫反射颜色 进行乘法叠加, 它们两共同影响最终的颜色
        //2.兰伯特光照模型计算时，漫反射材质颜色使用 1 中的叠加颜色计算
        //3.最终使用的环境光叠加时，环境光变量UNITY_LIGHTMODEL_AMBIENT需要和 1 中颜色进行乘法叠加
        //  为了避免最终的渲染效果偏灰

        //其他的计算步骤同BlinnPhong的逐片元光照实现
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
