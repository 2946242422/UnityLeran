using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson118 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� 
        //��ƬԪ��ɫ�����ж�ƬԪ�����������Ƿ������и�������ƬԪ����������и�����Ƚϣ�
        //���������ֱ������ƬԪ����Ⱦ��clip��
        //�ж�ƬԪ��ģ���е������棬����ʹ���������������Ⱦ��VFACE��
        #endregion

        #region ֪ʶ��һ ʵ�������и�Ч���� Shader����
        //1.�½�Shader ObjectCutting
        //  ɾ�����ô���
        //2.�������� ����ӳ��
        //  ������ _MainTex 2D
        //  �������� _BackTex 2D
        //  �и���������ƱȽ�x��y��z�ĸ��ᣩ_CuttingDir Float
        //  �Ƿ�ת�и� _Invert Float
        //  �и�λ�ã���C#���ݹ�����_CuttingPos Vector
        //3.�ر��޳� ��ΪҪ������Ⱦ
        //4.����ָ�� #pragma target 3.0 ��VFACE�����Ը��� 
        //5.�ṹ��
        //  uv�����㡢��������
        //6.������ɫ������
        //  ����ת��������ֵ����������ת��
        //7.ƬԪ��ɫ������
        //  7-1.����VFACE�������
        //  7-2.�������������������ɫ
        //  7-3.�����и���ж��Ƿ�����0��������1����������
        //      ����ʹ��step(edge, x)����
        //      x>=edge ���� 1��x<edge ���� 0
        //  7-4.�����Ƿ�ת�и���������Ƿ�ת����
        //  7-5.����clip��������ƬԪ
        //  7-6.�������� ֱ�ӷ�����ɫ
        #endregion

        #region ֪ʶ��� ʵ�������и�Ч���� C#����
        //1.�½�C#�ű� ��Shader��һ��
        //2.����[ExecuteAlways]���ԣ��ñ༭ģʽ��Ҳ���У����Կ���Ч��
        //3.������������и�λ�ö���
        //4.�������ʼ��
        //6.��Update�в�ͣ�Ľ��и�����λ�ô��ݸ�Shader
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
