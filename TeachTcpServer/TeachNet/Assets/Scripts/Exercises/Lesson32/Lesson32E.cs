using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lesson32E : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator GetMsg()
    {
        UnityWebRequest req = new UnityWebRequest("web服务器地址", UnityWebRequest.kHttpVerbPOST);
        DownLoadHandlerMsg msgHandler = new DownLoadHandlerMsg();
        req.downloadHandler = msgHandler;

        yield return req.SendWebRequest();

        if( req.result== UnityWebRequest.Result.Success)
        {
            PlayerMsg msg = msgHandler.GetMsg<PlayerMsg>();
            //使用消息对象 来进行逻辑处理
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
