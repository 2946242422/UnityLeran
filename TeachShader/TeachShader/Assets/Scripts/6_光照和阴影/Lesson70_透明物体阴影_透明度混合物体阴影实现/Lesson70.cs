using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson70 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� ͸���Ȼ��
        //͸���Ȳ���ֻ�ܴ�����Ч������ȫ͸������ȫ��͸����
        //��͸���Ȼ�Ͽ��԰�������ʵ�ְ�͸��Ч��
        //���Ļ���ԭ��
        //�ر����д�룬������ϣ���ƬԪ��ɫ����ɫ����������ɫ���л�ϼ���
        #endregion

        #region ֪ʶ��һ ׼������
        //�½�һ��Shader
        //��֮ǰ��д��͸���Ȼ�����Shader����(Lesson59_Transparent_Both)���ƽ���

        //�ڳ����д���һ����֮ǰ͸���Ȳ���һ�������������
        //���ڲ���
        #endregion

        #region ֪ʶ��� ͸���Ȼ��Shader������Ӱ
        //�����Ͻڿε�˼·
        //���������Ҫ���������ӰЧ���޷Ǿ�������
        //1.Ͷ����Ӱ
        //  ��FallBack�й������õĶ�ӦShader��"Transparent/VertexLit"��
        //2.������Ӱ
        //  ��Shader����д������Ӱ˥��ֵ����ش���
        //��˶���͸���Ȼ�ϵ�ShaderҲ��ʹ��ͬ����˼·ȥ����

        //���ǣ�����
        //����͸���Ȼ����Ҫ�ر����д��
        //����Ӱ��صĴ�����Ҫ�õ����ֵ�������
        //���Unity�д����ܷ��濼�ǣ�Ҫ�����͸������ĵ���Ӱ����Ч������Ը��ӵģ�
        //���е����ð�͸��Shader�����������ӰЧ�������� Transparent/VertexLit��
        //���
        //2-1.͸�����Shader��Ҫ Ͷ����Ӱʱ
        //    ��������FallBack��д�������Դ��İ�͸�����Shader
        //    ��������Ͷ����Ӱ��Ч������Ϊ��Ȳ���д��

        //2-2.͸�����Shader��Ҫ ������Ӱʱ
        //    Unity���ù�����Ӱ���ռ������غ�
        //    ������㴦�� ͸�����Shader
        //    ������� ����Ϊ��͸��Ч��(Blend SrcAlpha OneMinusSrcAlpha)��Shader 
        //    ��Ϊ͸�������������ֵ���ڵ���ϵ�޷�ֱ���ô�ͳ����Ȼ������Ӱ��ͼ������

        //���ۣ�
        //Unity�в���ֱ��Ϊ͸���Ȼ��Shader������Ӱ
        #endregion

        #region ֪ʶ���� ǿ��������Ӱ
        //���ǿ��Գ�����͸�����Shaderǿ��Ͷ����Ӱ

        //��FallBack������һ����͸��Shader������VertexLit��Diffuse��
        //�����еĵƹ�ģʽ����Ϊ��ӰͶ�����Ⱦͨ����������Ӱӳ������ļ���
        //�Ѹ����嵱��һ��ʵ�����崦��

        //���ǣ�����Ч��������ʵ��������ʹ��
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
