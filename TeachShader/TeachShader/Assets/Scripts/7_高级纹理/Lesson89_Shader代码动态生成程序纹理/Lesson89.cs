using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson89 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��ع�
        //1.�����������̸����
        //  ���ӵ����б��ͬ��ͬż��Ϊ��ɫ����ͬ��Ϊ��ɫ

        //2.��ѧ֪ʶ�ع�
        //  2-1:����������ӽ��Ϊż��
        //  2-2:����ż����ӽ��Ϊż��
        //  2-2:һ��������һ��ż����ӵĽ��������
        #endregion

        #region ������֪ʶ��
        //  Shader�е����ú���floor������UnityCG.cginc��
        //  �ú��� ����һ����ֵ floor(��ֵ)
        //  ��Դ�����ֵ����ȡ��
        //  ���磺
        //  floor(2.6)  ����  2
        //  floor(0.4)  ����  0
        //  floor(-2.3) ���� -3
        #endregion

        #region ֪ʶ�� Shader���붯̬��������������̸�����
        //1.�½�Shader��ɾ�����ô���
        //2.��������
        //  ƽ������(������) _TileCount
        //  ������ɫ1 _Color1
        //  ������ɫ2 _Color2
        //3.v2f�ṹ��
        //  �����uv
        //4.������ɫ��
        //  ��������ת��
        //  uvֱ�Ӹ�ֵ
        //5.ƬԪ��ɫ��
        //  uv * ������ ��0~1��Χ ��Ϊ 0~_TileCount��Χ
        //  ����floor�õ����и��ӱ��
        //  ������ż��ӹ��ɵõ� 0��1 ֵ��0����ͬ���ͬż��1����ͬ
        //  ���ø�ֵ����������ʹ��������ɫ
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
