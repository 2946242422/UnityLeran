using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson112 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع�
        //�����������ʵ���˶�ģ��Ч�� �Ļ���ԭ����
        //�õ����ص�ǰ֡����һ֡���ڲü��ռ��µ�λ�ã�
        //��������λ�ü����������˶����򣬴Ӷ�ģ����˶�ģ����Ч��
        #endregion

        #region ����֪ʶ��
        //������Ҫͨ��C#����ΪShader���þ������
        //����ShaderLab�﷨�е������в�û�о������͵ı���
        //�������ֻ��Ҫ��CG����������������Լ���
        //����C#��ͨ�������������ͬ�����Խ�������
        //������
        //CG����������4x4���� ���� float4x4 _ClipToWorldMatrix;
        //C#����������4x4���� ���� Matrix4x4 frontClipToWorldMatrix;
        //ͨ��������ָ�������������ü���
        //material.SetMatrix("_ClipToWorldMatrix", frontClipToWorldMatrix);
        #endregion

        #region ֪ʶ��һ ʵ�� �����������ʵ���˶�ģ����Ļ���ڴ���Ч�� ��Ӧ Shader
        //1.�½�Shader�ļ���ȡ�� MotionBlurWithDepthTexture ��������˶�ģ��Ч��
        //2.�������ԣ���������ӳ��
        //  ������ _MainTex
        //  ģ��ƫ���� _BlurSize

        //  ������� _CameraDepthTexture
        //  ��ǰ֡�ü�������ռ�任���� float4x4 _ClipToWorldMatrix
        //  ��һ֡���絽�ü��ռ�任���� float4x4 _FrontWorldToClipMatrix
        //3.��Ļ�������
        //  ZTest Always 
        //  Cull Off
        //  ZWrite Off
        //4.�ṹ��
        //  �����uv����
        //5.������ɫ��
        //  ����ת�� uv���긳ֵ
        //6.ƬԪ��ɫ��
        //  6-1:�õ��ü��ռ��µ�������
        //      �õ���һ
        //      ���ֵ��ȡ
        //      �����ü��ռ���������� uv �� ���
        //      �õ����
        //      �ü��ռ�����ת����ռ�(ע�������γ�����
        //      ������һ֡�任��������ռ�����ת�ü��ռ䣨ע�������γ�����
        //  6-2:�õ��˶�����
        //      �õ�ǰ֡��-��һ֡�� �õ��˶�����
        //  6-3:����ģ������
        //      ����ģ��ƫ������������3��ƫ�Ʋ�����ɫ�����ƽ��ֵ����
        //7.FallBack Off
        #endregion

        #region ֪ʶ��� ʵ�� �����������ʵ���˶�ģ����Ļ���ڴ���Ч�� ��Ӧ C#
        //1.����C#���룬������Shaderһ��
        //2.�̳���Ļ�������PostEffectBase
        //3.����ģ��ƫ�������������ڼ�¼��һ�α任����ı���
        //4.��дOnRenderImage����
        //  �����н����������ã��任������㣬��Ļ����
        //5.���������ں������������������ʼ����һ֡�任����
        #endregion

        #region ֪ʶ���� ����ע���
        //1.���ǲ�ͬƽ̨���ܴ��ڵĴ�ֱ��ת���⡢
        //  #if UNITY_UV_STARTS_AT_TOP
        //  if (_MainTex_TexelSize.y < 0)
        //    o.uv_depth.y = 1 - o.uv_depth.y;
        //  #endif

        //2.���ƶ�������������2
        //  �Ӷ������˶�ģ��Ч����ǿ�ȣ���Ҫ����ǿ��
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
