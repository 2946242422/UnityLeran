using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson88 : MonoBehaviour
{
    //��������Ŀ��
    public int textureWidth = 256;
    public int textureHeight = 256;
    //�����������̸��������
    public int tileCount = 8;
    //���̸��������ɫ
    public Color color1 = Color.white;
    public Color color2 = Color.black;


    // Start is called before the first frame update
    void Start()
    {
        #region ���ڿ�����֪ʶ��
        //1.����Unity��Texture2D�������������
        //2.����Renderer�����ò���������
        //3.����Unity�༭����չ֪ʶ�Զ���Inspector����
        //4.����֮ǰ�γ�ʵ�ֵĵ�������Shader���ڲ���
        #endregion

        #region ֪ʶ��һ ���ɹ����������̸��������
        //1.���ó�������ɱ༭����
        //  �����ߡ����̸������������̸�������ɫ.
        //2.ʵ�ָ���������
        //  2-1������Texture2D����ͨ���������óߴ�
        //  2-2�������������ÿ�����ص���ɫ
        //       ���򣺹����������̵� x��y���򣬰����ӷ֣�
        //            ���ӵ����б��ͬ��ͬż��Ϊ��ɫ����ͬ��Ϊ��ɫ
        //            ����ֻ��Ҫ�ж�(x,y)�������ڸ��Ӻ��������Ĺ�ϵ����
        //  2-3��Ӧ�����ر仯
        #endregion

        #region ֪ʶ��� ���ò�����������
        //1.��ȡ�ű����������Renderer��Ⱦ���ű�
        //2.ͨ����Ⱦ���ű����ò�����������
        #endregion

        UpdateTexture();

        #region ֪ʶ���� ��Inspector������Ӹ�������ť
        //1.Ϊ��������ű��½��Զ���Inspector�����ýű�
        //2.����Զ���༭������
        //3.��дOnInspectorGUI()������������ʹ��DrawDefaultInspector������ʾĬ�����
        //4.�½���ť�����ڵ��ò��ʸ��·���
        #endregion

        #region �ܽ�
        //C#���붯̬���ɳ���������Խϼ�
        //����ֻ��Ҫ�������ô����������ͼƬ
        //����Ҫ��ʱ����³���������
        //���������ʱ�����Ը�����������
        //�������ڱ༭ģʽ�£�������������ʱ
        #endregion
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void UpdateTexture()
    {
        //���߶�Ӧ����������newһ��2D�������
        Texture2D tex = new Texture2D(textureWidth, textureHeight);
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                //������Ҫ֪�� ���ӵĿ���Ƕ���
                //textureWidth / tileCount = ���ӵĿ�
                //textureHeight / tileCount = ���ӵĸ�

                // x / ���ӵĿ�56��= ��ǰx���ڸ��ӱ��
                // y / ���ӵĸ� (56) = ��ǰy���ڸ��ӱ��

                //Ҫ�ж�һ���� ��ż���������� ֱ�Ӷ�2ȡ�� �����0 ��Ϊż�� ���Ϊ1 ��Ϊ����
                //�ж� x �� y ���� �������� �Ƿ�ͬ�� ���� ͬż
                if( x / (textureWidth / tileCount) % 2 == y / (textureHeight / tileCount) % 2 )
                    tex.SetPixel(x, y, color1);
                else
                    tex.SetPixel(x, y, color2);
            }
        }
        //Ӧ�����صı仯
        tex.Apply();

        Renderer renderer = this.GetComponent<Renderer>();
        if(renderer != null)
        {
            //�õ���Ⱦ������еĲ����� �����޸�����������
            renderer.sharedMaterial.mainTexture = tex;
        }
    }
}
