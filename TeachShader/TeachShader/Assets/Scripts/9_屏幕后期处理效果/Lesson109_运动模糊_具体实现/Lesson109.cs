using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson109 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� �˶�ģ������ԭ��
        //����֮ǰ����Ⱦ��������ϰѵ�ǰ����Ⱦͼ����ӵ�֮ǰ����Ⱦͼ����
        //ͨ��RenderTexture�����б��棬��2��Pass�����л�ϵ���

        //һ��Pass���RGBͨ����������ͼƬ����ģ���̶Ⱦ������ջ��Ч��
        //Blend SrcAlpha OneMinusSrcAlpha��(Դ��ɫ * SrcAlpha) + (Ŀ����ɫ * (1 - SrcAlpha))��
        //ColorMask RGB ��ֻ�ı���ɫ�������е�RGBͨ����

        //һ��Pass���Aͨ�����ɵ�ǰ��Ļͼ���Aͨ��������
        //Blend One Zero��������ɫ = (Դ��ɫ * 1) + (Ŀ����ɫ * 0)��
        //ColorMask A��ֻ�ı���ɫ�������е�Aͨ����
        #endregion

        #region ׼������ ����Գ���

        #endregion

        #region ֪ʶ��һ ʵ�� �˶�ģ����Ļ���ڴ���Ч�� ��Ӧ Shader
        //1.�½�Shader ��Ϊ�˶�ģ��(MotionBlur) ɾ���������ô���
        //2.��������
        //  ������ _MainTex
        //  ģ���̶� _BlurAmount
        //3.����CG���� CGINCLUDE...ENDCG
        //  �����ļ�UnityCG.cginc����
        //  ����ӳ��
        //  �ṹ�壨�����UV)
        //  ������ɫ�����ü��ռ�ת�� uv���긳ֵ��
        //4.��Ļ����Ч������
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //5.��һ��Pass�����ڻ��RGBͨ����
        //  ������� �� ��ɫ�ɰ�����
        //      Blend SrcAlpha OneMinusSrcAlpha��(Դ��ɫ * SrcAlpha) + (Ŀ����ɫ * (1 - SrcAlpha))��
        //      ColorMask RGB ��ֻ�ı���ɫ�������е�RGBͨ����
        //  ƬԪ��ɫ��
        //      �����������������ģ���̶���ΪAͨ������ɫ��������ɫ���л��
        //6.�ڶ���Pass���û����Aͨ����
        //  ������� �� ��ɫ�ɰ�����
        //      Blend One Zero��������ɫ = (Դ��ɫ * 1) + (Ŀ����ɫ * 0)��
        //      ColorMask A��ֻ�ı���ɫ�������е�Aͨ����
        //  ƬԪ��ɫ��
        //      �����������
        //7.FallBack Off
        #endregion

        #region ֪ʶ��� ʵ�� �˶�ģ����Ļ���ڴ���Ч�� ��Ӧ C#
        //1.����C#�ű�����Ϊ�˶�ģ��MotionBlur
        //2.�̳���Ļ�������PostEffectBase
        //3.������Ա����
        //  ������ģ���̶�
        //  ˽�еĶѻ����� accumulation Texture�����ڴ洢��һ����Ⱦ�����
        //4.��дOnRenderImage����
        //  4-1.���ѻ�����Ϊ�ջ��߱仯 ���ʼ����Ⱦ����
        //      ������hideFlagsΪHideFlags.HideAndDontSave�����䲻���棩
        //  4-2.����ģ���̶�����
        //  4-3.��Դ�������ò���д�뵽�ѻ������У��൱�ڼ�¼������Ⱦ�����
        //  4-4.���ѻ�����д��Ŀ��������
        //5.���ʧ�������ٶѻ�����
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
