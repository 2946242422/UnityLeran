using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson94 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع�
        //ʵ��2D����Ч���Ĺؼ���ʽ:
        //ĳ��λ��ƫ���� = sin(_Time.y * ����Ƶ�� + ����ĳ������ * �����ĵ���) * ��������
        #endregion

        #region ֪ʶ����
        //��Ⱦ��ǩ
        //"DisableBatching" = "True"
        //��Ҫ���ã�
        //�Ƿ��SubShader�ر�������
        //�������������㶯��ʱ����ʱ��Ҫ�رո�Shader��������
        //��Ϊ�������������㶯��ʱ����ʱ��Ҫʹ��ģ�Ϳռ��µ�����
        //���������ϲ�������ص�ģ�ͣ���Щģ�͸��Ե�ģ�Ϳռ�ᶪʧ�����������޷���ȷʹ��ģ�Ϳռ����������

        //��ʵ�����̵�2D����Ч��ʱ�����Ǿ���Ҫ�ö�����ģ�Ϳռ��½���ƫ��
        //���������Ҫʹ�øñ�ǩ��Ϊ��Shader�ر�������
        #endregion

        #region ֪ʶ��һ ������Դ �۲���Դ
        //1.������������Դ
        //2.�۲���Դģ�Ϳռ�����
        //  ��ģ�͵�ģ�Ϳռ����겢������Unity�����׼
        //  ����������x�� ������z�� ǰ����y��
        #endregion

        #region ֪ʶ��� ������2D����Ч�� ����ʵ��
        //1.�½�Shader��ɾ�����ô���
        //2.�������ԡ�ӳ������
        //  ������(_MainTex)
        //  ���ӵ���ɫ(_Color)
        //  ��������(_WaveAmplitude)
        //  ����Ƶ��(_WaveFrequency)
        //  �����ĵ���(_InvWaveLength)
        //3.͸��Shader���
        //  ��Ⱦ��ǩ���
        //  Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "DisableBatching"="True"}
        //  ���д�롢͸��������
        //  ZWrite Off
        //  Blend SrcAlpha OneMinusSrcAlpha
        //4.�ṹ�����
        //  �����uv
        //5.������ɫ��
        //  ���������н���Ĺ�ʽ�������Ӧ����ƫ��λ��
        //  ע�⣬��ģ�Ϳռ���ƫ��
        //6.ƬԪ��ɫ��
        //  ֱ�ӽ�����ɫ��������ɫ����
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
