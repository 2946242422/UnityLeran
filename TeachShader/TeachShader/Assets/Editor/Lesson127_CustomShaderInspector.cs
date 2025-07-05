using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lesson127_CustomShaderInspector : ShaderGUI
{
    private bool isShow;

    private Lesson128_MaterialPropertyDrawer floatDrawer = new Lesson128_MaterialPropertyDrawer(-2, 2);

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        //base.OnGUI(materialEditor, properties);

        if (GUILayout.Button(isShow ? "����������������" : "��ʾ������������"))
            isShow = !isShow;

        //��ȡ��ǰ������
        Material material = materialEditor.target as Material;

        if (GUILayout.Button("���ò���������"))
        {
            material.SetTexture("_MainTex", null);
            material.SetFloat("_TestFloat", 0);
        }

        material.renderQueue = EditorGUILayout.IntField("��Ⱦ����", material.renderQueue);

        if(isShow)
        {
            //MaterialProperty prop = FindProperty("_TestFloat", properties);
            ////�Զ���һ���϶���ȥ����TestFloat����
            ////���ֵ�����ȡĳһ�����Եķ�ʽ �Ͳ���Ҫʹ���м���� �Լ� ��Ӧ�Ĳ�����������
            //prop.floatValue = EditorGUILayout.Slider("�Զ���float����", prop.floatValue, -1, 1);

            //MaterialProperty prop2 = FindProperty("_MainTex", properties);
            //materialEditor.ShaderProperty(prop2, prop2.displayName);

            foreach (var item in properties)
            {
                if (item.displayName == "TestFloat")
                {
                    //�Զ���һ���϶���ȥ����TestFloat����
                    //item.floatValue = EditorGUILayout.Slider("�Զ���float����", item.floatValue, -1, 1);
                    //material.SetFloat("_TestFloat", value);

                    floatDrawer.OnGUI(EditorGUILayout.GetControlRect(), item, item.displayName, materialEditor);
                }
                else
                    //���û�ȡ����һ�����Ĳ������� ����Unity�Դ���Inspector����UI��ʾ��ʽȥ��ʾ��Щ����
                    materialEditor.ShaderProperty(item, item.displayName);
            }
        }
    }
}
