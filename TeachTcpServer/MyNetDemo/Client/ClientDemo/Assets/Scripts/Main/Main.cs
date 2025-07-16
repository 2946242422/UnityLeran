using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(NetMgr.Instance == null)
        {
            GameObject obj = new GameObject("Net");
            obj.AddComponent<NetMgr>();
        }

        NetMgr.Instance.Connect("127.0.0.1", 8888);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
