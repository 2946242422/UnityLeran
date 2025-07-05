using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson88 : MonoBehaviour
{
    //程序纹理的宽高
    public int textureWidth = 256;
    public int textureHeight = 256;
    //国际象棋棋盘格的行列数
    public int tileCount = 8;
    //棋盘格的两种颜色
    public Color color1 = Color.white;
    public Color color2 = Color.black;


    // Start is called before the first frame update
    void Start()
    {
        #region 本节课所用知识点
        //1.利用Unity中Texture2D类生成纹理对象
        //2.利用Renderer类设置材质球纹理
        //3.利用Unity编辑器拓展知识自定义Inspector窗口
        //4.利用之前课程实现的单张纹理Shader用于测试
        #endregion

        #region 知识点一 生成国际象棋棋盘格纹理对象
        //1.设置程序纹理可编辑参数
        //  纹理宽高、棋盘格行列数、棋盘格两种颜色.
        //2.实现更新纹理方法
        //  2-1：创建Texture2D对象，通过参数设置尺寸
        //  2-2：设置纹理对象每个像素的颜色
        //       规则：国际象棋棋盘的 x和y方向，按格子分，
        //            格子的行列编号同奇同偶则为白色，不同则为黑色
        //            我们只需要判断(x,y)像素所在格子和上面规则的关系即可
        //  2-3：应用像素变化
        #endregion

        #region 知识点二 设置材质球主纹理
        //1.获取脚本依附对象的Renderer渲染器脚本
        //2.通过渲染器脚本设置材质球主纹理
        #endregion

        UpdateTexture();

        #region 知识点三 在Inspector窗口添加更新纹理按钮
        //1.为生成纹理脚本新建自定义Inspector窗口用脚本
        //2.添加自定义编辑器特性
        //3.重写OnInspectorGUI()方法，在其中使用DrawDefaultInspector方法显示默认组件
        //4.新建按钮，用于调用材质更新方法
        #endregion

        #region 总结
        //C#代码动态生成程序纹理相对较简单
        //我们只需要按需求用代码绘制纹理图片
        //在需要的时候更新程序纹理即可
        //更新纹理的时机可以根据需求来定
        //可以是在编辑模式下，可以是在运行时
        #endregion
    }

    /// <summary>
    /// 更新纹理
    /// </summary>
    public void UpdateTexture()
    {
        //更具对应的纹理宽高来new一个2D纹理对象
        Texture2D tex = new Texture2D(textureWidth, textureHeight);
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                //首先需要知道 格子的宽高是多少
                //textureWidth / tileCount = 格子的宽
                //textureHeight / tileCount = 格子的高

                // x / 格子的宽（56）= 当前x所在格子编号
                // y / 格子的高 (56) = 当前y所在格子编号

                //要判断一个数 是偶数还是奇数 直接对2取余 如果是0 则为偶数 如果为1 则为奇数
                //判断 x 和 y 方向 格子索引 是否同奇 或者 同偶
                if( x / (textureWidth / tileCount) % 2 == y / (textureHeight / tileCount) % 2 )
                    tex.SetPixel(x, y, color1);
                else
                    tex.SetPixel(x, y, color2);
            }
        }
        //应用像素的变化
        tex.Apply();

        Renderer renderer = this.GetComponent<Renderer>();
        if(renderer != null)
        {
            //得到渲染器组件中的材质球 并且修改它的主纹理
            renderer.sharedMaterial.mainTexture = tex;
        }
    }
}
