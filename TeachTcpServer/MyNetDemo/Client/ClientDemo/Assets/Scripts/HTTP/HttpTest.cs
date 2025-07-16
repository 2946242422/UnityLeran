using System;
using System.IO;
using System.Net;
using UnityEngine;

public class HttpTest : MonoBehaviour
{
    private string path = "http://192.168.1.63/HTTP/夏日海边.png";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            HttpWebRequest req = HttpWebRequest.Create(path) as HttpWebRequest;
            req.Method = WebRequestMethods.Http.Get;
            req.Timeout = 2000;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                print("文件存在且可用");
                print(resp.ContentLength);
                print(resp.ContentType);
                resp.Close();
            }
            else
            {
                print("文件不能用" + resp.StatusCode);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        try
        {
            HttpWebRequest request = HttpWebRequest.Create(path) as HttpWebRequest;
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = 2000;
            HttpWebResponse resp = request.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                print("本地路径"+Application.persistentDataPath);
                using (FileStream fileStream =File.Create(Application.persistentDataPath+"/HttpTest.png"))
                {
                    Stream downloadStream = resp.GetResponseStream();
                    byte[] buffer = new byte[resp.ContentLength];
                    int contentRead = downloadStream.Read(buffer, 0, buffer.Length);
                    while (contentRead!=0)
                    {
                        fileStream.Write(buffer, 0, contentRead);
                        contentRead = downloadStream.Read(buffer, 0, buffer.Length);
                    }
                    fileStream.Close();
                    
                    downloadStream.Close();
                    resp.Close();
                }
                print("<下载成功>");
            }
            else
            {
                print("下载失败" + resp.StatusCode);
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}