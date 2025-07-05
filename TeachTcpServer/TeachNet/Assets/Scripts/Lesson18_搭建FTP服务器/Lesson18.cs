using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson18 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 搭建FTP服务器的几种方式
        //1.使用别人做好的FTP服务器软件 （学习阶段建议使用）
        //2.自己编写FTP服务器应用程序，基于FTP的工作原理，用Socket中TCP通信来进行编程（工作后由后端程序员来做）
        //3.将电脑搭建为FTP文件共享服务器 （工作后由后端程序员来做）

        //第2,3点我们前端程序主要做了解
        //一般在工作中不会由我们来完成这部分工作
        #endregion

        #region 知识点二 使用别人做好的FTP服务器软件来搭建FTP服务器
        //下载Serv-U等FTP服务器软件
        //在想要作为FTP服务器的电脑上运行之
        //1.创建域 直接不停下一步即可
        //2.使用单向加密
        //3.创建用于上传下载的FTP 账号和密码
        #endregion

        #region 总结
        //在实际商业项目开发当中，如果需要用FTP来进行文件传输
        //那么FTP服务器的解决方案都是由后端程序员来完成的
        //不管它使用哪种方式来搭建FTP服务器
        //只要能正常上传下载内容并且保证安全性即可
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
