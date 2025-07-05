using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson108 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� BloomЧ������ԭ��
        //����4��Pass����3��������
        //��ȡ(1��Pass) ����ȡԭͼ���е���������洢��һ����������
        //ģ��(2��Pass) ������ȡ�������������ģ������һ����ø�˹ģ����
        //�ϳ�(1��Pass) ����ģ�����������������Դ���������ɫ��
        #endregion

        #region ֪ʶ�� BloomЧ������ʵ��

        #region ׼������
        //�½�Shader��ȡ�� Bloom ��ɾ�����ô���
        #endregion

        #region ��һ�� ��ȡ
        //��ҪĿ�ģ���ȡԭͼ���е���������洢��һ����������
        //Shader����
        //1.��������
        //  ������ _MainTex
        //  ������������ _Bloom
        //  ������ֵ _LuminanceThreshold
        //2.��CGINCLUDE...ENDCG��ʵ�ֹ���CG����
        //  2-1������ӳ��
        //  2-2���ṹ�壨���㣬uv��
        //  2-3: �Ҷ�ֵ������ֵ�����㺯��
        //3.��Ļ�������
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //4.��ȡPass ʵ��
        //  ������ɫ�� 
        //      ����ת����UV��ֵ
        //  ƬԪ��ɫ��
        //      ��ɫ���������ȹ���ֵ���㡢��ɫ*���ȹ���ֵ

        //C#����
        //1.����C#�ű�����ΪBloom
        //2.�̳���Ļ�������PostEffectBase
        //3.����������ֵ��Ա����
        //3.��дOnRenderImage����
        //5.���ò������������ֵ
        //4.����������
        //  Graphics.Blit��RenderTexture.GetTemporary�� RenderTexture.ReleaseTemporary
        //  �������������Pass���� 
        #endregion

        #region �ڶ��� ģ��
        //��ҪĿ�ģ�����ȡ�������������ģ������һ����ø�˹ģ����
        //Shader����
        //1.���ģ���뾶���� _BlurSize����������ӳ�䣨ע�⣺��Ҫ�õ����أ�
        //2.�޸�֮ǰ��˹ģ��Shader��Ϊ���е�����Pass����
        //3.��Bloom Shader�У�����UsePass ���� ��˹ģ��Shader������Pass

        //C#����
        //1.���Ƹ�˹ģ���е�3������
        //2.���Ƹ�˹ģ����C#�����д����˹ģ�����߼�
        //3.��ģ�������������洢_Bloom��������
        #endregion

        #region ������ �ϳ�
        //��ҪĿ�ģ���ģ�����������������Դ���������ɫ����
        //Shader����
        //�ϲ�Passʵ��
        //1.�ṹ��
        //  �������꣬4ά��uv��xy��������wz��������ȡ����
        //2.������ɫ��
        //  ��������ת�����������긳ֵ
        //
        //  ע�⣺���������uv������Ҫ�ж��Ƿ����Y�ᷭת
        //  ��Ϊʹ��RenderTextureд�뵽Shader���������ʱ
        //  Unity���ܻ�������Y�ᷭת
        //  ���ǿ�������Unity�ṩ��Ԥ���������ж�
        //  #if UNITY_UV_STARTS_AT_TOP
        //  
        //  #endif
        //  �������걻���壬˵����ǰƽ̨����������ϵ��Y��ԭ���ڶ���
        //  �������ڸú��������ؽ����ж�
        //  ������ص� y С��0��Ϊ��������ʾ��Ҫ��Y����е���
        //  �������ʹ�þ���
        //  #if UNITY_UV_STARTS_AT_TOP
        //  if (_MainTex_TexelSize.y < 0.0)
        //      ��תuv��y������
        //  #endif
        //  ��������Ҫ���ǽ��ж��⴦��һ��Unity���Զ�����
        //  һ��ֻ��Ҫ��ʹ��RenderTextureʱ�ſ��Ǹ�����

        //3.ƬԪ��ɫ��
        //  ����������ɫ���������
        //4.FallBack Off

        //C#����
        //��Դ������кϲ�����
        #endregion

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
