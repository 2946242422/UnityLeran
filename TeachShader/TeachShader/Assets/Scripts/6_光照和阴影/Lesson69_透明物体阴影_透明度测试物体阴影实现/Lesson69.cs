using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson69 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� ͸���Ȳ���
        //����Ϸ������
        //�����ĳЩ��λ��ȫ͸����������λ��ȫ��͸��
        //����͸��������������Ҫ��͸��Ч��
        //��ԱȽϼ��ˣ�ֻ�п��ü��Ϳ�����֮��
        //������Ҷ���ݡ�դ���ȵ�

        //����ԭ��
        //  ͨ��һ����ֵ��������Щ����Ӧ�ñ���������ЩӦ�ñ�����
        #endregion

        #region ֪ʶ��һ ׼������
        //�½�һ��Shader
        //��֮ǰ��д��͸���Ȳ������Shader����(Lesson59_AlphaTest_Both)���ƽ���

        //�ڳ����д���һ����֮ǰ͸���Ȳ���һ�������������
        //���ڲ���
        #endregion

        #region ֪ʶ��� ��͸���Ȳ���ShaderͶ����Ӱ
        //1.ͬ������ʹ��FallBack����ʽͶ����Ӱ
        //  ������Ҫע�����
        //  FallBack������Ϊ��Transparent/Cutout/VertexLit
        //  ��Ĭ��Shader�л�Ѳü�������������Ϣд�뵽 ��Ӱӳ���������������ͼ��
        //  ע�⣺
        //  ʹ�ø�Ĭ��Shader����Ͷ����Ӱʱ����Ҫʹ��_Cutoff���� �� _Color������������ؼ���
        //  ������Ǳ��뱣֤���ǵ�Shader��������Ϊ_Cutoff����ֵ���� �� _Color����������ɫ����
        //  �����޷��õ���ȷ��Ӱ���

        //2.Ϊ�˵õ���ȷ����ӰЧ����������Ҫ���������Cast Shadows��Ͷ����Ӱ����������ΪTwo Sided��˫�棩
        //  ǿ����Unity������Ӱ��������ʱ����������������Ϣ��
        //  ��Ϊ��������ã�Ĭ�Ͻ�������Ⱦ����Ӱ�����������������ͼʱֻ�ῼ�����������
        //  ���Թ�Դ���治�������㣬����Ϊ˫��󼴿ɲ�����㣬�õ���ȷ�Ľ��
        #endregion

        #region ֪ʶ���� ��͸���Ȳ���Shader������Ӱ
        //������֮ǰ���������Ӱ�ķ�ʽһ��
        //��Ҫ��5���裺
        //1.����ָ��
        //  #pragma multi_compile_fwdbase
        //  ���ڰ������Ǳ������б��� ���ұ�֤˥����ع��ձ����ܹ���ȷ��ֵ����Ӧ�����ñ�����
        //2.���������ļ�
        //  #include "AutoLight.cginc"
        //3.�ṹ����������Ӱ�����
        //  SHADOW_COORDS(n)
        //  nΪ��һ�����õĲ�ֵ�Ĵ���������ֵ���ṹ��ǰ���м���TEXCOORD�����
        //4.����ת����
        //  TRANSFER_SHADOW(o);
        //5.Unity����˥�������
        //  UNITY_LIGHT_ATTENUATION(atten, v2f�ṹ��, ������������λ��);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
