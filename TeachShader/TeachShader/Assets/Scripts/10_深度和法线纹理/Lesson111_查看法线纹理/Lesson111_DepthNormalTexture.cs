using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson111_DepthNormalTexture : PostEffectBase
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.DepthNormals; 
    }
}
