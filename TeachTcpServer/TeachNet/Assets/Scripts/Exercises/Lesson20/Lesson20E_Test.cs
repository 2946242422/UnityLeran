using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson20E_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FtpMgr.Instance.UpLoadFile("MrTangPic.png", Application.streamingAssetsPath + "/test.png", () =>
        {
            print("上传结束 调用委托函数");
        });

        print(Application.persistentDataPath);
        FtpMgr.Instance.DownLoadFile("实战就业路线.jpg", Application.persistentDataPath + "/实战就业路线.jpg", () =>
        {
            print("下载结束 调用委托函数");
        });

        print("测试测试");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
