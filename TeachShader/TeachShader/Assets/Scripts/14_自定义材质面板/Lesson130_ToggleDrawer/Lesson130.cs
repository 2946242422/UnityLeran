using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson130 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �Դ�Shader�������Ի�������ʲô��
        //ͨ��֮ǰ��ѧϰ
        //����֪��ͨ���̳�MaterialPropertyDrawer(�������Ի�����) ʵ�ֵ���
        //�����Զ���Shader������Inspector���ڵ����

        //���Դ�Shader�������Ի�����ָ�ľ���
        //Unity�ڲ�ʵ�ֺõļ̳���MaterialPropertyDrawer(�������Ի�����) �Ļ�����
        //������������Shader����������ֱ��ʹ��
        //��������Inspector�����о߱�������ʽ
        //�������ǽ��в������������
        #endregion

        #region ֪ʶ��� ToggleDrawer��
        //���ã�
        //��float�����Կ��ص���ʽ�ڲ��������������ʾ
        //��ֵֻ������Ϊ0��1, 0Ϊ�ر�, 1Ϊ����

        //ʹ�ó�����
        //1.������ر�ĳ����Ч�������Ƿ����ñ�Ե��
        //2.�л�ĳЩ���ܣ������Ƿ�ʹ��������ɫ��
        //3.����������֧�߼�
        //�ȵ�

        //�÷���
        //��������������������ʱ
        //������ǰ����[Toggle]��[Toggle(�Զ�������)]
        //[Toggle]_������("������", Float) = 0��1

        #endregion

        #region ֪ʶ���� ToggleDrawer�����ؼ���
        //ToggleDrawer���Ժ͹ؼ��ֱ���ָ����ʹ��
        //�ﵽ�ڲ��������ͨ��Toggle�������ùؼ��ֵ�Ч��
        //�Ӷ��л����ܻ�����Ч��
        //�������ÿ������ڲ������Inspector������ͨ���򵥵ĸ�ѡ�����û��߽��ùؼ���

        //ʹ�÷�ʽ��
        //1.ToggleDrawer�͹ؼ��ʰ�
        //  ������ʹ��Toggle����һ��Float����ʱ
        //  Unity���Զ���������ֵ����ȫ�ֹؼ���
        //  ������
        //  [Toggle]_ShowTex("ShowTex", Float) = 0��1
        //  Ĭ�����ɵ�ȫ�ֹؼ���Ϊ _SHOWTEX_ON����ѡʱΪ1ʱ���
        //  ����Ҳ��������Toggle�Զ���ؼ�����[Toggle(�Զ���ؼ�����)]

        //2.�ؼ�������
        //  ����ʹ��
        //  #pragma shader_feature �� #pragma multi_compile �������ؼ���
        //  �������ڲ�������л�Toggleʱ��Unity�����û���ö�Ӧ�Ĺؼ��ʣ�������Ӧ��Shader����

        //ע�⣺����ָ�������ToggleDrawer�Ĺؼ���һ�£�������ȷ�л�����
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
