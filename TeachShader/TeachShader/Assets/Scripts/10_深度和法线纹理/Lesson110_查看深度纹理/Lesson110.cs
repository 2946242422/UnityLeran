using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson110 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� ��������ʹ��
        //��һ������C#��������������������ģʽ
        //�����������
        //Camera.main.depthTextureMode = DepthTextureMode.Depth;

        //�ڶ�������Shader�����а�������������
        //sampler2D _CameraDepthTexture;

        //�������ʹ��
        //ʹ�������������� �õ��Ľ���Ƿ����Ե�
        //  float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
        //�������Ե����ֵ ת�����۲�ռ��� �õ���ֵ�����ص㵽������ľ��루�������ֵ��
        //  float viewDepth = LinearEyeDepth(depth);
        //�������Ե����ֵ ת�����۲�ռ��� �������ص㵽������ľ���ת����[0,1]������ ���������ֵ��
        //  float linearDepth = Linear01Depth(depth);
        #endregion

        #region ֪ʶ��һ ��β鿴���������Ϣ
        //���ǿ�������Ļ���ڴ�����������ѧϰ���Ļ�ȡ�����Ϣ��֪ʶ��
        //�����ֵ��Ϊ��ɫ��RGBֵ��ʾ����Ļ��
        //������������д洢������
        //��������˵��������ֵʹ��0~1��Χ������ֵ
        //Խ�ӽ����ü���Խ������ɫ
        //Խ�ӽ�Զ�ü���Խ������ɫ
        #endregion

        #region ֪ʶ��� ʵ�� �鿴���������Ļ���ڴ���Ч�� ��Ӧ Shader
        //1.�½�Shader��ɾ�����ô���
        //2.��������
        //  ��Shader��ͨ������_CameraDepthTexture��ȡ�������
        //3.������ɫ��
        //  ֻ��Ҫ�޸Ĵ���ṹ������
        //4.ƬԪ��ɫ��
        //  ʹ�������������� �õ��Ľ���Ƿ����Ե�
        //  float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
        //  �������Ե����ֵ ת�����۲�ռ��� �������ص㵽������ľ���ת����[0,1]������ ���������ֵ��
        //  float linearDepth = Linear01Depth(depth);
        //  �����ֵ��Ϊ������ɫ��RGBֵ
        //5.Fallback Off
        #endregion

        #region ֪ʶ���� ʵ�� �鿴���������Ļ���ڴ���Ч�� ��Ӧ C#����
        //1.�½�C#�ű���������Shader��ͬ
        //2.�̳�PostEffectBase
        //3.��Start�����������������ģʽ
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
