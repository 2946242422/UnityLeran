using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson127 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �Զ���������ָ����ʲô
        //����Ŀǰ����ͨ����Shader��������Ե���ʽ
        //��һЩ����ϣ�����ⲿ���õ������ڲ������Inspector��������ʾ

        //�Զ���������ָ�ľ���
        //Unity������ЩĬ�ϵ���ʾ�����⣬�������������Զ���������Inspector������ʾ
        #endregion

        #region ֪ʶ��� ShaderGUI����ʲô
        //����һ�������Զ������ Inspector ���� �Ļ���
        //ͨ���̳� ShaderGUI���������ȫ���Ʋ��ʱ༭����Ĳ��ֺ͹���
        //�������������� Shader �� ����(Properties) ���鶨���Ĭ����Ϊ

        //ShaderGUI���������ǣ�
        //1.�Զ�����ʽ��沼��
        //2.ͨ���ű��߼������Ի���ĳЩ����ֵ��̬���ػ������������
        //3.��Ӹ߼����ܣ�����ͨ����Ӱ�ť�ؼ�������ĳЩ��������ʵʱ���²���Ԥ������ʾ������Ϣ��
        //4.��ǿ���ʱ༭���飬ʹ������Ա������������������ĵײ�Shaderʵ�֣�����ͨ���ѺõĽ�����ٵ�������
        //�ȵ�
        #endregion

        #region ֪ʶ���� ShaderGUI��Ļ���ʹ��
        //1.�Զ���C#�ű��̳�ShaderGUI
        //2.��дOnGUI����
        //3.��OnGUI��������д�Զ��岼���߼�
        //4.��Shader���������� CustomEditor "�Զ���C#�ű���"  

        //�����������ʹ�ø�Shader�Ĳ������ʹ���Զ��岼�ֹ���
        #endregion

        #region ֪ʶ���� �Զ���������
        //ʹ��ShaderGUI�Զ���������ĺ��ķ�������������д��
        //void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties) ����
        //�������������ǳ���Ҫ��
        //1.materialEditor:
        //  �ṩ����ʽ����Ľӿڣ������������ֵ��
        //  �ؼ����ԣ�
        //  target
        //  ������������ȡ��������

        //  �ؼ�������
        //  ShaderProperty(MaterialProperty����, "����")
        //  ���Խ���������Ϊ��Ӧ���֣�������Ĭ��GUI��ʾ

        //2.properties:
        //  ����������Shader�����������ж��������
        //  �ؼ����ԣ�
        //  displayName
        //  ��ȡ������

        //������
        //MaterialProperty ���Զ��� = FindProperty("������", properties)
        //��������FindProperty��������ȡ����Ӧ���Զ���
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
