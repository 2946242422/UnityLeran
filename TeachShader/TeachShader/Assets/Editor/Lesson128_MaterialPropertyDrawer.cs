using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lesson128_MaterialPropertyDrawer : MaterialPropertyDrawer
{
    private float min; 
    private float max;

    //构造函数一般是用来初始化我们自定义的一些变量的
    public Lesson128_MaterialPropertyDrawer(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        //base.OnGUI(position, prop, label, editor);

        if(prop.type != MaterialProperty.PropType.Float )
        {
            EditorGUILayout.LabelField(label, "请使用float或者数值 不然无法使用该控件");
            return;
        }

        prop.floatValue = EditorGUILayout.Slider(label, prop.floatValue, min, max);

    }
}
