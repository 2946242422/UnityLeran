using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson103 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع�
        //1.���ȼ������
        //  ��ͼ���ÿ��������ɫ���г˷�����
        //  ������ɫ = ԭʼ��ɫ * ���ȱ���

        //2.���Ͷȼ������
        //  ����ڻҶ���ɫ���в�ֵ
        //  ��һ�����Ҷ�ֵ�����ȣ�L = 0.2126*R + 0.7152*G + 0.0722*B
        //  �ڶ������Ҷ���ɫ = (L, L, L)
        //  ��������������ɫ = lerp( �Ҷ���ɫ, ԭʼ��ɫ, ���Ͷȱ��� )

        //3.�Աȶȼ������
        //  ��������Ի�ɫ���в�ֵ
        //  ��һ�������Ի���ɫ = (0.5,0.5,0.5)
        //  �ڶ�����������ɫ = lerp( ���Ի�ɫ, ԭʼ��ɫ, �Աȶȱ��� )
        #endregion

        #region ֪ʶ��һ ʵ�����ȡ����Ͷȡ��Աȶ���Ļ���ڴ���Ч����Ӧ Shader
        //1.�½�һ��Shader����ΪBrightnessSaturationContrast�����ȱ��ͶȶԱȶȣ�
        //  ɾ���������ô���
        //2.��������������������ӳ��
        //  ������ _MainTex 2D
        //  ���� _Brightness Float
        //  ���Ͷ� _Saturation Float
        //  �Աȶ� _Contrast Float
        //3.������Ȳ��ԡ��޳������д��
        //  ZTest Always  ������Ȳ���
        //  Cull Off    �ر��޳�
        //  Zwrite Off  �ر����д��
        //  ��������������Ļ����ı���

        //  ��Ϊ��Ļ����Ч���൱���ڳ����ϻ�����һ������Ļͬ��ߵ��ı�����Ƭ
        //  ��������Ŀ���Ǳ�����"��ס"�������Ⱦ����
        //  ����������OnRenderImageǰ����[ImageEffectOpaque]����ʱ
        //  ͸����������ڸø���Ļ����Ч����Ⱦ��������ر����д���Ӱ������͸�����Pass
        //4.�ṹ�����
        //  ���㡢��������
        //5.������ɫ��
        //  ����ת�ü��ռ䣬uv����ƫ��
        //6.ƬԪ��ɫ��
        //  ����������в���
        //  �ֱ����ù�ʽ���� ���ȡ����Ͷȡ��Աȶ�
        //  ���ش�������ɫ
        #endregion

        #region ֪ʶ��� ʵ�����ȡ����Ͷȡ��Աȶ���Ļ���ڴ���Ч����Ӧ C#����
        //1.����C#�ű�����ΪBrightnessSaturationContrast�����ȱ��ͶȶԱȶȣ�
        //2.�̳���Ļ�������PostEffectBase
        //3.�������ȱ��ͶȶԱȶȱ��������ڿ���Ч���仯
        //4.��дOnRenderImage���������������ò������Ӧ����
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
