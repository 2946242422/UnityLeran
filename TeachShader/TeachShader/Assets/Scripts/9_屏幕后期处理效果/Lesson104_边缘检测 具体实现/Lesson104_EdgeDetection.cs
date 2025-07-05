using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson104_EdgeDetection : PostEffectBase
{
    public Color EdgeColor;
    public Color BackgroundColor;
    [Range(0,1)]
    public float BackgroundExtent;

    protected override void UpdateProperty() 
    {
        if(material != null)
        {
            material.SetColor("_EdgeColor", EdgeColor);
            material.SetColor("_BackgroundColor", BackgroundColor);
            material.SetFloat("_BackgroundExtent", BackgroundExtent);
        }
    }
}
