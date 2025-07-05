using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson72 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是标准高光反射Shader？
        //和上节课一样，所谓的标准高光反射Shader
        //其实就是一个常用的高光反射Shader而已

        //该Shader其实就是一个
        //带有法线（世界空间中计算-全局效果的表现更准确）的基于BlinnPhong光照模型的
        //支持多光源和阴影的Shader
        #endregion

        #region 知识点二 制作常用高光反射Shader
        //1.新建一个Shader，取名叫BumpedSpecular（凹凸镜面反射）
        //2.复制 Lesson52 中Shader代码，粘贴到新建文件中
        //3.加入渲染标签Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        //  渲染类型设置为不透明的、渲染队列设置为几何队列（不透明的几何体通常使用该队列）
        //4.加入阴影、衰减相关代码
        //5.加入附加渲染通道
        //6.加入FallBack "Specular"
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
