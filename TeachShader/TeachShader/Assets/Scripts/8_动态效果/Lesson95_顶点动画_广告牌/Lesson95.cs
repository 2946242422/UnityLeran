using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson95 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //广告牌效果实现关键点
        //1.新坐标系
        //  原点确定（一般0,0,0）
        //  坐标轴计算（x，y，z）
        //2.顶点计算
        //  偏移位置 = 顶点坐标– Center
        //  新顶点位置 = Center + X轴 * 偏移位置.x + Y轴 * 偏移位置.y + Z轴 * 偏移位置.z
        //3.全向广告牌和垂直广告牌区别
        //  计算normal轴时，y为0则为垂直广告牌
        #endregion

        #region 导入资源

        #endregion

        #region 知识点 广告牌效果 具体实现
        //1.新建Shader，删除无用代码
        //2.声明属性，属性映射
        //  主纹理、颜色叠加、垂直广告牌程度（0为垂直广告牌，1为全向广告牌）
        //3.透明Shader相关
        //  注意：关闭批处理，并让其两面渲染
        //4.结构体相关
        //  顶点和纹理坐标
        //5.顶点着色器
        //  5-1:确定新坐标中心点
        //  5-2:计算Z轴（normal），将摄像机坐标转到模型空间
        //  5-3:用垂直广告牌程度改变Z轴y值后，单位化
        //  5-4:声明Y轴（old up）
        //  5-5:利用Z轴（normal）和Y轴（old up）叉乘计算出X轴（right）
        //  5-6:利用Z轴（normal）和X轴（right）叉乘计算出Y轴（up）
        //  5-7:得到顶点相对于新坐标系中心点的偏移位置
        //  5-8:利用新中心点和3轴计算出顶点新位置
        //  5-9:新顶点转到裁剪空间
        //  5-10:UV缩放偏移
        //6:片元着色器
        //  直接采样 叠加颜色即可
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
