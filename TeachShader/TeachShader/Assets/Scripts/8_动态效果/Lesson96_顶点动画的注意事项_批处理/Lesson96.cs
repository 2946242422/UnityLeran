using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson96 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //我们在之前的顶点动画相关课程中一再强调
        //我们需要在渲染标签中添加
        //"DisableBatching"="True"
        //来让该Shader渲染的对象不进行批处理
        //目的是让基于模型空间的计算能够正确进行
        //不会影响最终的渲染结果
        #endregion

        #region 知识点一 为什么批处理会影响顶点动画
        //Unity中默认有静态批处理和动态批处理
        //批处理的主要作用是
        //合并多个对象，将他们作为一个DrawCall进行处理
        //之所以批处理会对顶点动画带来影响
        //是因为
        //不同的对象会拥有不同的变换矩阵（位置、旋转、缩放）
        //而批处理后
        //他们的变换矩阵会进行统一处理

        //举例：
        //物体A:位于世界空间位置 (0, 0, 0)，无旋转。
        //物体B:位于世界空间位置 (5, 0, 0)，无旋转。
        //他们是两个独立的对象，拥有不同的变换矩阵

        //不进行批处理时：
        //每个对象的变换矩阵会单独传递给Shader，顶点的模型空间位置会根据各自的变换进行正确计算

        //进行批处理时：
        //启用批处理后，Unity会将对象A和对象B合并为一个Draw Call，并使用一个统一的变换矩阵
        //比如在静态批处理中，Unity会将对象A和对象B的顶点合并为一个网格，并使用统一的变换进行渲染
        //批处理后顶点位置是混合的，Shader中无法区分不同对象的模型空间位置
        //可能带来的问题有:
        //顶点动画失效：
        //假设你希望顶点在模型空间的x方向上进行sin波动动画。
        //如果对象A和对象B的模型空间位置被混合，波动动画会变得不可预测

        //变换混淆：
        //对象A和对象B有不同的变换矩阵。
        //如果批处理后使用统一的变换矩阵，Shader无法区分每个顶点属于哪个对象，导致所有顶点的动画效果混淆。

        //总结：
        //批处理会让对象失去独立性
        //相当于将多个对象之间独立的模型空间坐标系合并为一个坐标系
        //从而影响顶点的相对位置和变换矩阵等信息
        //导致顶点动画结果异常
        //因此我们通过渲染标签来关闭批处理
        #endregion

        #region 知识点二 关闭批处理带来的问题
        //关闭批处理带来的最直接问题就是导致
        //DrawCall的提升
        //DrawCall的提升可能会带来性能问题

        //如果DrawCall的增加并没有带来性能问题
        //那我们可以通过关闭批处理来解决顶点动画问题
        //如果带来了性能问题，并且必须优化带有顶点动画的Shader，我们应该如何解决呢？
        #endregion

        #region 知识点三 如何解决问题
        //开启批处理
        //1.顶点颜色
        //  利用顶点颜色来存储每个顶点的位置信息或相对位置信息
        //  我们在C#代码中获取模型网格顶点数据，将数据存储到网格的颜色属性中
        //  在Shader中通过颜色属性获取顶点信息
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            Color[] colors = new Color[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                // 将模型空间位置存储在顶点颜色中
                colors[i] = new Color(vertices[i].x, vertices[i].y, vertices[i].z, 1);
            }

            mesh.colors = colors;
        }
        //  在Shader中直接在appdata_full结构体中点出颜色成员既可以利用它获取到顶点信息

        //2.uv通道
        //  和上面的顶点颜色方案类似，只是把相关信息存储到uv通道中而已，但是一般在存储两个值时使用

        //等等
        #endregion

        #region 总结
        //若顶点动画Shader由于关闭批出来带来了性能问题
        //我们可以去掉渲染标签"DisableBatching"="True"
        //通过
        //1.顶点颜色
        //2.uv通道
        //等方案去避免顶点动画渲染问题
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
