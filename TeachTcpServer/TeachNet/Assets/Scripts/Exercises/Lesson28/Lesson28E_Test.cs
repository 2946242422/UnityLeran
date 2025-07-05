using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Lesson28E_Test : MonoBehaviour
{
    public RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        //只要保证一运行时 进行该判断 进行动态创建
        if(NetWWWMgr.Instance == null)
        {
            GameObject obj = new GameObject("WWW");
            obj.AddComponent<NetWWWMgr>();
        }

        //在任何地方使用NetWWWMgr都没有问题

        //NetWWWMgr.Instance.LoadRes<Texture>("http://192.168.50.109:8000/Http_Server/实战就业路线.jpg", (obj) =>
        //{
        //    //使用加载结束的资源
        //    image.texture = obj;
        //});

        //NetWWWMgr.Instance.LoadRes<byte[]>("http://192.168.50.109:8000/Http_Server/实战就业路线.jpg", (obj) =>
        //{
        //    //使用加载结束的资源
        //    //把得到的字节数组存储到本地
        //    print(Application.persistentDataPath);
        //    File.WriteAllBytes(Application.persistentDataPath + "/www图片.jpg", obj);
        //});

        //NetWWWMgr.Instance.LoadRes<string>("http://192.168.50.109:8000/Http_Server/test.txt", (str) =>
        //{
        //    print(str);
        //});

        //NetWWWMgr.Instance.UploadFile("练习题上传.jpg", Application.streamingAssetsPath + "/test.png", (code) =>
        //{
        //    if (code == UnityWebRequest.Result.Success)
        //    {
        //        print("上传成功");
        //    }
        //    else
        //        print("上传失败" + code);
        //});

        //NetWWWMgr.Instance.UnityWebRequestLoad<Texture>("http://192.168.50.109:8000/Http_Server/实战就业路线.jpg",
        //                                                (tex) =>
        //                                                {
        //                                                    image.texture = tex;
        //                                                });

        NetWWWMgr.Instance.UnityWebRequestLoad<byte[]>("http://192.168.50.109:8000/Http_Server/实战就业路线.jpg",
                                                        (bytes) =>
                                                        {
                                                            print("图片字节数" + bytes.Length);
                                                        });

        print(Application.persistentDataPath);
        NetWWWMgr.Instance.UnityWebRequestLoad<object>("http://192.168.50.109:8000/Http_Server/实战就业路线.jpg",
                                                        (obj) =>
                                                        {
                                                            print("保存本地成功");
                                                        }, Application.persistentDataPath + "/Lesson28E.jpg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
