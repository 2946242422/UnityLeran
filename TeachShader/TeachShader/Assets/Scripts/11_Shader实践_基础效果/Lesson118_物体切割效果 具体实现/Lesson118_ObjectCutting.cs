using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class Lesson118_ObjectCutting : MonoBehaviour
{
    //����
    private Material material;
    //�и���� ���ھ����и�λ��
    public GameObject cutObj;

    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<Renderer>().sharedMaterial; 
    }

    // Update is called once per frame
    void Update()
    {
        if(material != null && cutObj != null)
        {
            material.SetVector("_CuttingPos", cutObj.transform.position);
        }
    }
}
