using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson110_DepthTexture : PostEffectBase
{
    // Start is called before the first frame update
    void Start()
    {
        //������Shader�еõ���Ӧ�����������Ϣ��
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }
}
