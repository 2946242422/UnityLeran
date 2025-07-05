using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lesson74_RenderToCubeMap : EditorWindow
{
    private GameObject obj;
    private Cubemap cubeMap;

    [MenuItem("����������̬����/�����ɴ���")]
    static void OpenWindow()
    {
        Lesson74_RenderToCubeMap window = EditorWindow.GetWindow<Lesson74_RenderToCubeMap>("�������������ɴ���");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("������Ӧλ�ö���");
        //���ڹ�������Ŀؼ�
        obj = EditorGUILayout.ObjectField(obj, typeof(GameObject), true) as GameObject;
        GUILayout.Label("������Ӧ����������");
        //���ڹ�������������Ŀؼ�
        cubeMap = EditorGUILayout.ObjectField(cubeMap, typeof(Cubemap), false) as Cubemap;
        //�����ť�� ��ȥִ�������߼�
        if(GUILayout.Button("��������������"))
        {
            if(obj == null || cubeMap == null)
            {
                EditorUtility.DisplayDialog("����", "���ȹ�����Ӧ�������������ͼ", "ȷ��");
                return;
            }
            //��̬����������������
            GameObject tmpObj = new GameObject("��ʱ����");
            tmpObj.transform.position = obj.transform.position;
            Camera camera = tmpObj.AddComponent<Camera>();
            //�ؼ�������������������6��2D������ͼ ��������������
            camera.RenderToCubemap(cubeMap);
            DestroyImmediate(tmpObj);
        }
    }
}
