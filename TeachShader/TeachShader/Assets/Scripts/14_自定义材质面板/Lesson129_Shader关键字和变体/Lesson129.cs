using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson129 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Shader中的变体是什么？
        //Shader Variant（变体）指的是一个Shader的特定配置
        //通过不同的关键字（Keywords）和 设置组合 来实现不同的效果
        //每一种关键字组合或设置都会生成一个独立的Shader变体
        //最终以二进制形式存储在构建文件中供运行时使用

        //说人话：
        //Shader变体就是基于一个Shader文件当中的代码，编译生成多个版本的Shader
        //它基于关键字来生成各种不同的版本

        //举例说明
        //一个Shader中有两个关键字
        //每个关键字有两种状态（启用或禁用）
        //最终生成的变体数量为2²=4
        //变体 1：没有启用任何关键字（默认）。
        //变体 2：启用了 关键字1。
        //变体 3：启用了 关键字2。
        //变体 4：同时启用了 关键字1 和 关键字2
        //运行时，Unity会根据具体设置选择与之匹配的变体来使用

        //Unity的一些内置功能会隐式的生成变体
        //比如
        //光照模式
        //雾效
        //渲染管线
        //等等
        #endregion

        #region 知识点二 局部Shader关键字
        //1.声明：
        //声明局部关键字有两种方式
        //  #pragma shader_feature 或 #pragma multi_compile 来声明关键词
        //  1-1.#pragma shader_feature 关键词1 关键词2 关键词3....
        //      作用：
        //      该编译指令声明的关键字，只有关键词启用时才会生成对应Shader变体

        //  1-2.#pragma multi_compile 关键词1 关键词2 关键词3.... 
        //      作用：
        //      该编译指令声明的关键字，不管是否启用都会生成对应Shader变体

        //关键词命名规则
        //通常使用大写字母和下划线分隔(_大写单词_大写单词...)
        //比如：_Test_Keyword

        //两种声明方式的区别：
        //主要区别在于 生成Shader变体的数量
        //shader_feature:
        //编译更快，Shader文件更小，适用开关功能较少的场景
        //multi_compile:
        //编译更长，Shader文件更大，适用需要完整覆盖的功能

        //2.使用
        //关键字使用规则：
        //  利用
        //  #if defined(关键词名)
        //      ....
        //  #elif defined(关键词名)
        //      ....
        //  #else
        //      ....
        //  #endif
        //  配合使用，判断关键词启用时处理什么逻辑
        #endregion

        #region 知识点三 Shader中利用C#代码启用和禁用关键字
        //Unity Shader中的关键字是一种用于控制Shader变体的机制
        //允许我们通过启用或者禁用特定功能来动态调整Shader的行为

        //Shader中的关键字可以分为2种
        //1.全局关键字
        //Unity自带的关键字，所有Shader中共享
        //可以在C#代码中通过
        //Shader.EnableKeyword("全局关键字名")
        //Shader.DisableKeyword("全局关键字名")
        //来控制关键字的启用和禁用

        //2.局部关键字
        //只在特定的Shader中有效，不会影响其他Shader
        //Unity更推荐使用局部关键字以减少关键字冲突问题
        //可以在C#代码中通过
        //材质球.EnableKeyword("局部关键字名")
        //材质球.DisableKeyword("局部关键字名")
        //来控制关键字的启用和禁用
        #endregion

        #region 知识点四 变体和条件分支的区别
        //条件分支语句会带来逻辑上执行的开销
        //虽然运行时直接判断但是会带来对性能的影响

        //变体虽然会增加编译时间并占用更多的内容
        //但是它的性能高，灵活性强

        //在静态功能选择时变体更优
        //在动态功能选择时条件分支语句更适合
        #endregion

        #region 总结
        //Shader 变体就是在编译时基于关键字生成多个版本的 Shader 代码，
        //在运行时根据关键字状态选择相应的版本来渲染物体。
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
