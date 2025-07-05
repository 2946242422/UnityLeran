using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Lesson88))]
public class Lesson88_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        //绘制默认参数相关的内容
        DrawDefaultInspector();

        //获取目标脚本
        Lesson88 scriptObj = (Lesson88)target;

        if(GUILayout.Button("更新程序纹理"))
        {
            scriptObj.UpdateTexture();
        }
    }
}
