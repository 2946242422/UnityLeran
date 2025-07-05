using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson123 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Mathf.PerlinNoise的作用
        //float Mathf.PerlinNoise(float x, float y)
        //该函数是 Unity 提供的一个用于生成 Perlin Noise 的函数，
        //主要用于生成平滑、连续的伪随机值。
        //它广泛应用于纹理生成、地形生成、动画和特效等需要平滑过渡的场景

        //传入的两个参数是二维坐标点，通常用于表示柏林噪声空间中的位置
        //返回值为一个浮点数，范围为0~1之间
        //返回的噪声值具有连续性，即相邻输入值对应的输出值是平滑过渡的
        #endregion

        #region 知识点二 利用该函数生成柏林噪声纹理图片

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
