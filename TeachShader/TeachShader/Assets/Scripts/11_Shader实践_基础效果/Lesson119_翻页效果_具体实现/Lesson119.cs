using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson119 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� ����ԭ��
        //1.��ת�ؼ���
        //������ת�������ĳһ���������ת
        //ע�⣺����ģ�����ĵ����ƽ�Ʒ���
        //      ��ƽ�� ����ת ��ƽ�ƻ�ȥ

        //2.����ؼ���
        //�������Ǻ���Sin�ö�����Y�����λ��ƫ��
        //����0~90~180��֮��仯ʱ��0��180�Ȳ���Ҫ����У�90��ʱ��������ҳ����������
        #endregion

        #region ֪ʶ�� �鱾��ҳЧ���ľ���ʵ��
        //1.�½�Shader PageTurning
        //  ɾ�����ô���
        //2.�������� ����ӳ��
        //  ��������          _MainTex
        //  ��������          _BackTex
        //  ��ҳ����(0~180��) _AngleProgress
        //  y�������̶�(0~1)  _WeightY
        //  x�������̶�(0~1)  _WeightY
        //  ����(0~3)         _WaveLength
        //  ƽ�ƾ���          _MoveDis
        //3.����Ҫ˫����Ⱦ
        //  Cull Off
        //4.����Ҫʹ��VFACE���壬�������ָ��
        //  #pragma target 3.0
        //5.�ṹ��
        //  �����UV
        //6.������ɫ��
        //  ��ʵ�ֻ����ķ�ҳЧ��
        //  1.����sincos�����õ���ǰ��ҳ���ȶ�Ӧ��sin��cosֵ
        //  2.���ڵõ���ֵ��������ת����
        //  3.��תǰ��ƽ��
        //  3.������ת������ж�����ת
        //  4.��ת������ƽ�ƻ�ȥ
        //  5.��������ϵĶ���ת�����ü��ռ���
        //  6.UV��ֵ
        //  ʵ�����Ч��
        //  �������صļ�����뵽��ת����֮ǰ
        //7.ƬԪ��ɫ��
        //  ͨ��VFACE�����ж���������ж�Ӧ�Ĳ�������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
