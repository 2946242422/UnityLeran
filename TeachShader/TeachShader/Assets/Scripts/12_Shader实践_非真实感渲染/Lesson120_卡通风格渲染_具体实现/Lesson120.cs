
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson120 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� 
        //��ͨ�����Ⱦ ����ԭ��
        //�ù�Ĺ���Ч����Ӳ����ʵ��������ߣ�

        //�ؼ��㣺
        //1.����ù�Ĺ���Ч����Ӳ
        //  ���ý��������������䣬�����Զ���򻯹�ʽ����߹ⷴ��
        //2.���ʵ���������
        //  ������Pass��һ����Ⱦ����ʵ��������ߣ�һ��������Ⱦ����
        //  ����Ҫ�ر����д�룬ģ������������ܹ�ͨ����Ȳ���
        #endregion

        #region ֪ʶ��һ ʵ���������Ӳ��������Ч��
        //1.�½�Shader ������������ۺ�ʵ����ش���ֱ�Ӹ��ƹ���
        //2.�����ƹ����Ĵ����Pass ��Ϊֻ��Ⱦ����
        //3.������Ӱ�����������
        //  2-1.����ָ�� #pragma multi_compile_fwdbase
        //  2-2.�����ļ� #include "AutoLight.cginc"
        //  2-3.v2f�ṹ���м��� float3 worldPos �� SHADOW_COORDS(4)
        //  2-4.������ɫ�������м���
        //      ����ռ䶥���������
        //      TRANSFER_SHADOW(o);
        //  2-5.����˥����ؼ���
        //      UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
        //      ������halfLambertNum���г˷�����
        //  2-6.FallBack "Diffuse"

        //4.������Ⱦ����������ߣ�
        //  4-1.�����Ե����ɫ�Ϳ������ _OutLineColor _OutLineWidth
        //  4-2.���뱳����Ⱦ��Pass�����ڴ����������
        #endregion

        #region ֪ʶ��� ʵ�ָ߹��ӲЧ��
        //��Ҫ�޸ĸ߹ⷴ����ɫ�����������
        //1.������������
        //2.�÷��ߺͰ���������е��
        //3.�õ�˵Ľ����һ����ֵ���бȽ� ���С����ֵ ȡ0 ������ֵ ȡ1
        //4.�����������͸߹���ɫ���е���
        //5.�����뵽���ַ���ɫ��ʽ������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
