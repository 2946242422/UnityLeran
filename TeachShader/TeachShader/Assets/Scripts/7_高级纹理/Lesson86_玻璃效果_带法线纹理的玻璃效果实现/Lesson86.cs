using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson86 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ʲô�Ǵ���������Ĳ���Ч��
        #endregion

        #region ֪ʶ��� ����������Ĳ���Ч��ʵ��
        //1.����Shader �����Ͻڿε���ش���

        //2.�� BumpedDiffuse ��׼����������Shader�й��ڷ�����صļ������Ͻ���
        //  ע�⣺������Ҫ_BumpScale�����ư�͹�̶ȣ�����Ĭ�Ϸ��߰�͹�̶����

        //3.�޸ķ��������������
        //  ���ڷ�����Ҫ�ӷ��������л�ȡ
        //  �����Ҫ�����������ļ�����뵽ƬԪ��ɫ����
        #endregion

        #region ֪ʶ���� �������߿ռ䷨������������ƫ��
        //1.����һ����������Ť���̶ȵ�������_Distortion ȡֵ��Χ���Դ�һЩ
        //2.�������߿ռ��·���������ƫ��ֵ
        //  �������йؼ�����
        //  ��һ��
        //  float2 offset = tangentNormal.xy * _Distortion ;
        //  ʹ�����߿ռ��·��ߵ�xy * Ť��ֵ�õ�һ��ƫ���� 
        //  ������߾������߷����Ŷ����ƫ�Ƴ̶ȣ�ȷ����������ķ����ǿ��

        //  �ڶ���
        //  ��Ļ����.xy = offset * ��Ļ����.z + ��Ļ����.xy;
        //  ��ƫ��������Ļ�ռ����ֵ��ˣ�ģ�����ʵ������Ч��
        //  ���ֵԽ�󣨼��������ԽԶ��������Ч��Խ���ԡ�
        //  ��������ʵ�ֽ���ԶС��Ч����ʹ�������ڲ�ͬ����ϵ�����Ч���������졣

        //  ���ּ��㷽ʽ��
        //  ͼ��ѧǰ����ͨ��ʵ���ܽ�����Ľӽ���ʵ��������Ч���ķ���
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
