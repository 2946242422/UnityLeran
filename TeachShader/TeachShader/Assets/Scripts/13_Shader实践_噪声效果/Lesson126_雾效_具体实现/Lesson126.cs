using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson126 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع�
        //������Ч���Ի�������֮ǰʵ�ֵ���Ļ����Ч����ȫ����Ч�����޸�
        //ͨ���������������Shader����ʱ�����ʵ����Ĳ������Լ���̬Ч��

        //�ؼ��㣺
        //1.������Ч����ʵ��
        //�����������в���ϵ�����ø�ϵ��������Ļ�����ӵļ�����
        //2.��̬Ч����ʵ��
        //�Զ���x���y��������ٶȱ���������Shader����ʱ�����_Time.y �õ��ۻ��仯��
        //�ü����������������н���ƫ�Ʋ������Ӷ��ﵽ��̬Ч����
        #endregion

        #region ֪ʶ��һ ������Ч Shaderʵ��
        //1.�� Lesson109����
        //2.�½�Shader Lesson126_NoiseFog ɾ����ش���
        //3.������Ļ����Ч����ȫ����Ч Lesson113_FogWithDepthTexture
        //4.�޸�Shader����
        //  4-1.�������
        //      �������� _Noise
        //      ����ֵƫ��ϵ�� _NoiseAmount
        //      X��Y���ƶ��ٶ� _FogXSpeed, FogYSpeed
        //      ����ӳ��
        //  4-2.�ٶȼ��㣬��������ƫ�Ʋ�����0~1 ת -0.5~0.5
        //  4-3.�����������Ӽ���
        #endregion

        #region ֪ʶ��� ������Ч C#ʵ��
        //1.�½�C#���� Lesson126_NoiseFog
        //2.������Ļ����Ч����ȫ����ЧC#���� Lesson113_FogWithDepthTexture
        //3.�޸�C#����
        //  3-1.��ӳ�Ա����
        //      ����ϵ�����ٶ�
        //  3-2.����Shader����
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
