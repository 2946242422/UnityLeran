using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson141 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� ��̬Һ��Ч�� ����ԭ��
        //1.��α�����װ�� ���� ����ģ�ͣ��ⲿ͸�����ڲ���̬Һ��
        //2.���͸����Ⱦ ���� ͸������������
        //3.����޳����� ���� ��ģ�Ϳռ����ĵ���Ϊ�ο��㣬����ת��������ռ���
        //  ����ģ�͵�ǰ����ռ��µĵ�������м�������
        //  ����жϵ��ڲο����Ϸ������Ǳ��������޳�
        //  ���Լ����Զ����������Һ��߶�
        //4.���ģ�Ⲩ��Ч�� ���� ʹ�������ĺ�������ع�ʽ
        //                    ĳ��λ��ƫ���� = sin( _Time.y * ����Ƶ�� + ����ĳ������ * �����ĵ���) * ��������
        #endregion

        #region ֪ʶ��һ ��̬Һ��Ч�� ����ʵ��
        //1.�½�������ɫ�� DynamicLiquid(��̬Һ��)
        //2.ɾ������Ҫ�Ĵ���
        //3.���������Լ�����ӳ��
        //  Һ����ɫ _Color
        //  �߹���ɫ�͹⻬��(rgb����ɫ a���⻬��) _Specular
        //  Һ��߶� _Height
        //  ���Ʊ仯�ٶ� _Speed
        //  �������� _WavaAmplitude
        //  ����Ƶ�� _WavaFrequency
        //  �����ĵ��� _InvWaveLength
        //4.͸������������
        //  RenderType��QueueΪTransparent
        //  Blend DstColor SrcColor
        //  Zwrite Off
        //5.����ָ������
        //  ����ģ������ʹ��StandardSpecular ���Ҳ�Ҫ��Ӱ noshadow
        //6.����ṹ��
        //  ֻ��Ҫ��ǰ���ص���������λ��
        //7.ʵ�ֱ��溯��
        //  7-1:ģ�����ĵ�ת��������
        //  7-2:�������ĵ�����ص�y�������
        //  7-3:�����޳�
        //  7-4:����Ч��ƫ�Ƽ���
        //  7-5:��������ɫ���߹���ɫ���⻬������
        #endregion

        #region ֪ʶ��� ��̬Һ��Ч�� ��ʹ��
        //1.��������������,һ��һС��С����Ϊ����Ӷ���
        //2.��Ľ����壬��Unity�Դ�Shader����Ϊ͸���ģ����Ʋ�������
        //3.С�Ľ����壬�ö�̬Һ��Ч������Ϊ�����е�Һ��
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
