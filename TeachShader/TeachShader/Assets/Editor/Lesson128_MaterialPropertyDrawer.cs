using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lesson128_MaterialPropertyDrawer : MaterialPropertyDrawer
{
    private float min; 
    private float max;

    //���캯��һ����������ʼ�������Զ����һЩ������
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
            EditorGUILayout.LabelField(label, "��ʹ��float������ֵ ��Ȼ�޷�ʹ�øÿؼ�");
            return;
        }

        prop.floatValue = EditorGUILayout.Slider(label, prop.floatValue, min, max);

    }
}
