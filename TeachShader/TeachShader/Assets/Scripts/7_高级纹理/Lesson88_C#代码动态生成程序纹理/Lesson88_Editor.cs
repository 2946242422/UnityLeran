using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Lesson88))]
public class Lesson88_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        //����Ĭ�ϲ�����ص�����
        DrawDefaultInspector();

        //��ȡĿ��ű�
        Lesson88 scriptObj = (Lesson88)target;

        if(GUILayout.Button("���³�������"))
        {
            scriptObj.UpdateTexture();
        }
    }
}
