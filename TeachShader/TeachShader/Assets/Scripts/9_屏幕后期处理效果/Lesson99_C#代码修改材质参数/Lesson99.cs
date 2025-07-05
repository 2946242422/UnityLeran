using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson99 : MonoBehaviour
{
    public Color color;
    [Range(0,1)]
    public float fresnelScale;

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        //��ȡ�������Ⱦ��
        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            //sharedMaterial��material������
            //sharedMaterial:һ���Ǹ�һ������
            //material:һ���Ǹ�һ������Ӱ������ʹ����ͬ������Ķ���
            //�õ���������
            material = renderer.material;//renderer.sharedMaterial;
            //�õ����еĲ�����
            //Material[] materials = renderer.sharedMaterials; //renderer.materials;
            //�޸���ɫ
            //material.color = color;
            //�޸�������
            //material.mainTexture = Resources.Load<Texture2D>("·��");

            //if(material.HasColor("_Color"))
            //{
            //    material.SetColor("_Color", color);
            //    print(material.GetColor("_Color"));
            //}
                

            //if(material.HasFloat("_FresnelScale"))
            //    material.SetFloat("_FresnelScale", fresnelScale);

            //�޸���Ⱦ����
            //material.renderQueue = 2000;

            //�޸Ĳ�����ʹ�õ�shader
            //material.shader = Shader.Find("Unlit/Lesson80_Fresnel");

            //material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));
            //material.SetTextureScale("_MainTex", new Vector2(0.5f, 0.5f));
        }

        #region ֪ʶ�ع�
        //Mesh Renderer(������Ⱦ��)
        //  ������ Mesh(����) ���� MeshFilter(���������������й���)
        //        ������ Geometry Data(��������)
        //  ������ Material(����)
        //        ������ Shader(��ɫ��)
        //        ������ Properties(���ԣ���ɫ������� ���� ��Shader�о�����Щ���Ա�¶�ڲ�����)

        //Skinned Mesh Renderer(��Ƥ������Ⱦ��)
        //  ������ Mesh(����)
        //        ������ Geometry Data(��������)
        //  ������ Bones & Weights(������Ȩ��)
        //  ������ Material(����)
        //        ������ Shader(��ɫ��)
        //        ������ Properties(���ԣ���ɫ������� ���� ��Shader�о�����Щ���Ա�¶�ڲ�����)
        #endregion

        #region ֪ʶ��һ ��εõ�����ʹ�õĲ���
        //1.��ȡ���������Ⱦ��Renderer 
        //  Mesh Renderer��Skinned Mesh Renderer���̳�Renderer
        //  ���ǿ�������ʽ�滻ԭ�����ȡ��װ���������
        //2.ͨ����Ⱦ����ȡ����Ӧ����
        //  ���ǿ���������Ⱦ���е�material����sharedMaterial����ȡ����Ĳ���
        //  ������ڶ�����ʣ�����ʹ��renderer.materials��renderer.sharedMaterials����ȡ

        //material��sharedMaterial������
        //material:
        //material���Ի᷵�ض����ʵ��������, �൱������Ϊ���󴴽�һ���ò��ʵĶ�������
        //����ͨ��material�����޸Ĳ���ʱ����Щ����ֻ��Ӱ������ض����󣬶�����Ӱ��ʹ����ͬ���ʵ���������
        //ʹ��material�������ڴ����ģ���Ϊÿ���������Լ������Ĳ��ʸ��������ǿ��Ե����޸ĵ�������

        //sharedMaterial:
        //sharedMaterial���Ի᷵�ض���Ĺ�����ʣ��൱�������ص�������ʹ��������ʵĶ������ͬһ������ʵ��
        //����ͨ��sharedMaterial�����޸Ĳ���ʱ����Щ���Ļ�Ӱ������ʹ��������ʵĶ���
        //ʹ��sharedMaterial���������ڴ����ģ����ǻ������޸�����ʹ�øò��ʵĶ���
        #endregion

        #region ֪ʶ��� ����޸Ĳ�������
        //1.��ɫ
        //  ���ʶ�������color��Ա������ɫ�޸�
        //2.����
        //  ���ʶ�������mainTexture��Ա�����������޸�
        //3.ͨ���޸ķ�ʽ
        //  �������и���Set�����������޸�����
        //  ͨ���������������Լ���Ӧֵ���и�ֵ
        //  ע�⣺����ֵ��SubShader��������������Ϊ׼������������ϵ���ʾ
        //4.�޸�Shader
        //  ���ò�����shader���Խ����޸�
        //  ����Shader.Find(Shader��)�����õ���ӦShader
        #endregion

        #region ֪ʶ���� �����г��÷���
        //���˸ղ�ѧϰ���޸����Ե���ط���
        //�����л��У�
        //1.�ж�ĳ����ָ�����������Ƿ����
        //2.��ȡĳ������ֵ
        //3.�޸���Ⱦ����
        //4.������������ƫ��
        //�ȵ�
        #endregion

        #region �ܽ�
        //Unity����Ҫͨ��C#�����޸�Shader��ز�����Ϣ
        //����һ�㶼��ͨ������ȥ�����޸ĵ�
        //��Ҫʹ�ò����ṩ�ĸ�����ط��������޸�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (material.HasColor("_Color"))
        {
            material.SetColor("_Color", color);
            print(material.GetColor("_Color"));
        }


        if (material.HasFloat("_FresnelScale"))
            material.SetFloat("_FresnelScale", fresnelScale);
    }
}
