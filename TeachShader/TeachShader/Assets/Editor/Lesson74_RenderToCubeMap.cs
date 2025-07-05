using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lesson74_RenderToCubeMap : EditorWindow
{
    private GameObject obj;
    private Cubemap cubeMap;

    [MenuItem("立方体纹理动态生成/打开生成窗口")]
    static void OpenWindow()
    {
        Lesson74_RenderToCubeMap window = EditorWindow.GetWindow<Lesson74_RenderToCubeMap>("立方体纹理生成窗口");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("关联对应位置对象");
        //用于关联对象的控件
        obj = EditorGUILayout.ObjectField(obj, typeof(GameObject), true) as GameObject;
        GUILayout.Label("关联对应立方体纹理");
        //用于关联立方体纹理的控件
        cubeMap = EditorGUILayout.ObjectField(cubeMap, typeof(Cubemap), false) as Cubemap;
        //点击按钮后 就去执行生成逻辑
        if(GUILayout.Button("生成立方体纹理"))
        {
            if(obj == null || cubeMap == null)
            {
                EditorUtility.DisplayDialog("提醒", "请先关联对应对象和立方体贴图", "确认");
                return;
            }
            //动态的生成立方体纹理
            GameObject tmpObj = new GameObject("临时对象");
            tmpObj.transform.position = obj.transform.position;
            Camera camera = tmpObj.AddComponent<Camera>();
            //关键方法，可以马上生成6张2D纹理贴图 用于立方体纹理
            camera.RenderToCubemap(cubeMap);
            DestroyImmediate(tmpObj);
        }
    }
}
