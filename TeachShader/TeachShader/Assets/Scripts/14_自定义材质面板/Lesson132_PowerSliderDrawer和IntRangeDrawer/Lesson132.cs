using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson132 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ PowerSliderDrawer��ʹ��
        //���ã�
        //PowerSliderDrawer��Unity�Դ��ļ̳���MaterialPropertyDrawer(�������Ի�����) ����
        //ʹ�����������ԣ������ø�������Inspector��������ʾΪһ��ָ������
        //�����ͨ�����Ի��飬�����Ը�����ĵ�����Щ�仯��Χ�ϴ󡢷����Էֲ��Ĳ���
        //����
        //�����Ե�ֵ��Χ�ϴ󣬵���Ч����ĳЩ�ض���Χ�ر�����ʱ������ͨ�� PowerSlider ���и�ֱ�۵ĵ���

        //������ͨ�������Ե������ǣ�
        //��ͨ��������Ĭ�������Էֲ����������λ�������ֵ������
        //PowerSlider �ṩ�� ������ӳ�䣬�ʺ����ڲ���ֵ��Ч��Ӱ�첻���ȵĳ���
        //���ǿ����ø�������
        //1.����ǿ�ȣ� �������ǿ������ǿ�ȱ仯��������ǿ�ȱ仯��ƽ������
        //2.�Աȶȣ� ����ͼ��ĶԱ�Ч����
        //3.ģ���̶ȣ� ����ģ����Χ�������Ǹ�˹ģ����
        //�ȵ�

        //ʹ�÷�ʽ��
        //[PowerSlider(ָ��)] ������ ("��ʾ����", Range(��Сֵ,���ֵ)) = Ĭ��ֵ

        //��Ӧ�ĺ�������Ϊ 
        //y = x��ָ���η�
        //xΪ��������λ��
        //yΪ���Ե���ֵ
        #endregion

        #region ֪ʶ��� IntRangeDrawer��ʹ��
        //���ã�
        //IntRangeDrawerͬ����Unity�Դ��ļ̳���MaterialPropertyDrawer(�������Ի�����) ����
        //ʹ�����������ԣ������ø�������Inspector��������ʾΪһ��������Χ����

        //ʹ�÷�ʽ��
        //[IntRange] ������ ("��ʾ����", Range(��Сֵ,���ֵ)) = Ĭ��ֵ
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
