using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson104 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع�
        //1.�Ҷ�ֵ L = 0.2126*R + 0.7152*G + 0.0722*B
        //2.��Ե���Ч���Ļ���ԭ��
        //  �õ� ��ǰ�����Լ��� �������ҡ��������¡��������¹�9�����صĻҶ�ֵ
        //  ����9���Ҷ�ֵ�� Sobel���� ���о������õ��ݶ�ֵ G = abs(Gx) + abs(Gy)
        //  ������ɫ = lerp��ԭʼ��ɫ�������ɫ���ݶ�ֵ��
        //3.��εõ���ǰ������Χ8������λ��
        //  ���� float4 ������_TexelSize ���� ��Ϣ�õ���ǰ������Χ8������λ��
        #endregion

        #region ׼������
        //1.����ͼƬ��Դ ����ΪSprite
        //2.�½�����
        //3.�ڳ�����ʹ�õ�����Դ�½�Sprite���� ���������Game���� ���ڲ���
        #endregion

        #region ֪ʶ��һ ʵ�ֱ�Ե�����Ļ���ڴ���Ч����Ӧ Shader
        //1.�½�Shader��ȡ����Ե���EdgeDetection��ɾ�����ô���
        //2.�������ԣ���������ӳ��
        //  ������ _MainTex
        //  ��Ե����õ���ɫ _EdgeColor
        //  ע������ӳ��ʱ ʹ���������ر��� _MainTex_TexelSize
        //3.��Ļ����Ч������
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //4.�ṹ�����
        //  ����
        //  uv���飬���ڴ洢9�����ص��uv����
        //5.������ɫ��
        //  ��������ת��
        //  ��uv����װ��9������uv����
        //6.ƬԪ��ɫ��
        //  ���þ����ȡ�ݶ�ֵ����������һ��Sobel���Ӽ��㺯����һ���Ҷ�ֵ���㺯����
        //  �����ݶ�ֵ��ԭʼ��ɫ�ͱ�Ե��ɫ֮����в�ֵ�õ�������ɫ
        //7.FallBack Off
        #endregion

        #region ֪ʶ��� ʵ�ֱ�Ե�����Ļ���ڴ���Ч����Ӧ C#����
        //1.����C#�ű�����ΪEdgeDetection����Ե��⣩
        //2.�̳���Ļ�������PostEffectBase
        //3.������Ե��ɫ���������ڿ���Ч���仯
        //4.��дUpdateProperty���������ò���������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
