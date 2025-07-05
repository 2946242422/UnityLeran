using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson101 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //屏幕后期处理效果的基本实现原理
        //就是利用 OnRenderImage函数 和 Graphics.Blit函数
        //来获取当前屏幕画面并利用Shader对该纹理进行自定义处理

        //捕获画面的关键 —— void OnRenderImage(RenderTexture source, RenderTexture destination)

        //实现效果的关键 —— Graphics.Blit (Texture source, RenderTexture dest, Material mat, int pass= -1);
        #endregion

        #region 知识点补充
        //1.Shader.isSupported
        //  如何判断Shader在目标平台和硬件上是否能正确运行
        //  我们可以通过获取Shader对象中的isSupported属性判断
        //  如果返回false,不支持
        //  如果返回true,支持

        //2.[ExecuteInEditMode]特性
        //  用于使脚本在编辑器模式下也能执行

        //3.[RequireComponent(typeof(组件名))]特性
        //  指定某个脚本所依赖的组件，它确保当你将脚本附加到游戏对象时，
        //  所需的组件也会自动添加到该游戏对象中
        //  如果这些组件已经存在，它们不会被重复添加
        //  因为后处理脚本一般添加到摄像机上，因此我们用于依赖摄像机

        //4.材质球中的 HideFlags 枚举
        //  从材质球对象中可以点出 HideFlags 枚举
        //  HideFlags.None: 对象是完全可见和可编辑的。这是默认值。
        //  HideFlags.HideInHierarchy: 对象在层级视图中被隐藏，但仍然存在于场景中。
        //  HideFlags.HideInInspector: 对象在检查器中被隐藏，但仍然存在于层级视图中。
        //  HideFlags.DontSaveInEditor: 对象不会被保存到场景中。适用于编辑器模式，不会影响播放模式。
        //  HideFlags.NotEditable: 对象在检查器中是只读的，不能被修改。
        //  HideFlags.DontSaveInBuild: 对象不会被包含在构建中。
        //  HideFlags.DontUnloadUnusedAsset: 对象在资源清理时不会被卸载，即使它没有被引用。
        //  HideFlags.DontSave: 对象不会被保存到场景中，不会在构建中保存，也不会在编辑器中保存。
        //                      这是 DontSaveInEditor | DontSaveInBuild | DontUnloadUnusedAsset 的组合。
        //  如果想要设置枚举满足多个条件 直接多个枚举 进行位或运算即可 |
        #endregion

        #region 知识点一 为什么要实现屏幕后处理基类
        //原因一：
        //为了实现屏幕后期处理效果
        //我们每次都需要做的事情一定是
        //1.实现一个继承子MonoBehaviour的自定义C#脚本
        //2.关联对应的材质球或者Shader
        //3.实现OnRenderImage函数
        //4.在OnRenderImage函数中使用Graphics.Blit函数
        //那么这些共同点我们完全可以抽象到一个基类中去完成
        //以后只需要在子类中实现各自的基本逻辑即可

        //原因二：
        //我们可以在基类中用代码动态创建材质球
        //不需要为每个后处理效果都手动创建材质球
        //只需要在Inspector窗口关联对应使用的Shader即可

        //原因三：
        //在进行屏幕后处理之前，我们往往需要检查一系列条件是否满足
        //比如：
        //当前平台是否支持当前使用的Unity Shader
        //我们可以在基类中进行判断，避免每次书写相同逻辑

        //注意：
        //在一些老版本中，你可能还会在基类中判断目标平台是否支持屏幕后处理和渲染纹理
        //一般通过Unity中的SystemInfo类判断
        //该类可以用于确定底层平台和硬件相关的功能是否被支持
        //官方说明：https://docs.unity.cn/cn/2022.3/ScriptReference/SystemInfo.html

        //但是随着时代发展，目前几乎所有的现代图形硬件都是支持屏幕后处理和渲染纹理了
        //因此我们无需再进行类似的判断的
        //只需要判断Shader是否被支持即可
        #endregion

        #region 知识点二 实现基类功能
        //主要目标
        //1.声明基类，让其依赖Camera，并且让其在编辑模式下可运行，保证我们可以随时看到效果
        //2.基类中声明 公共 Shader，用于在Inspector窗口关联
        //3.基类中声明 私有 Material，用于动态创建
        //4.基类中实现判断Shader是否可用，并且动态创建Material的方法
        //5.基类中实现OnRenderImage的虚方法，完成基本逻辑
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
