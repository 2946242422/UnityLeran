using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson138 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ������ɫ���е�����ṹ��ָʲô
        //�����Ͻڿ���Ҫѧϰ�˱���ָ��
        //#pragma surface ���溯���� ����ģ�� ��ѡ�������
        //�����ڽ�����溯����ʱ�ᵽ����Ҫ��������������
        //�����Ĳ��������̶ֹ���ʽ
        //1. void ���溯����(Input IN, inout SurfaceOutput o)
        //2. void ���溯����(Input IN, inout SurfaceOutputStandard o)
        //3. void ���溯����(Input IN, inout SurfaceOutputStandardSpecular o)
        //�����е�SurfaceOutput��SurfaceOutputStandard��SurfaceOutputStandardSpecular�ṹ��
        //�������ǵ�����ṹ��

        //���ǿ��������Ͻڿ���ѧϰ������ṹ��Input�е������ڱ��溯���н��м����
        //����������ֵ�洢������ṹ����
        //֮��Unity���������������������Ϊ����ģ�ͺ��������������и��ֹ�����صļ���
        //����������ṹ������Unity��ǰ�����õģ������Լ����Ӻͼ���
        //�������û�ж����е�ĳЩ������ֵ����ʹ��Ĭ��ֵ
        #endregion

        #region ֪ʶ��� ����ṹ��������Щ��Ա
        //SurfaceOutput�ṹ�壨��Unity�����ļ� Lighting.cginc ��������
        //�������ڱ���ָ����ʹ�� Lambert �� BlinnPhong ����ģ��ʱ������Ҫʹ�øýṹ��
        //struct SurfaceOutput
        //{
        //    fixed3 Albedo;    //������
        //    fixed3 Normal;    //���߿ռ䷨��
        //    fixed3 Emission;  //�Է��⣺һ��Unity����ƬԪ��ɫ��������ǰ �������ɫ��ֱ�ӵ����Է�����ɫ
        //    half Specular;    //���淴��ָ������Χ0~1
        //    fixed Gloss;      //���淴��ǿ��
        //    ������ص��������������ʹ����BlinnPhong����ģ��
        //    ��ʹ�øù�ʽ����߹ⷴ��ǿ�ȣ�float spec = pow(nh, s.Specular*128) * s.Gloss;
        //    fixed Alpha;      //͸��ͨ�������������͸���ȵĻ������ø�ֵ����ɫ���л��
        //}

        //SurfaceOutputStandard�ṹ�壨��Unity�����ļ� UnityPBSLighting.cginc ��������
        //�������ڱ���ָ����ʹ�� Standard ����ģ��ʱ������Ҫʹ�øýṹ��
        //����һ�����Ϊ��������������ṹ��
        //struct SurfaceOutputStandard
        //{
        //    fixed3 Albedo;    //������
        //    fixed3 Normal;    //���߿ռ䷨��
        //    fixed3 Emission;  //�Է���
        //    half Metallic;    //0��ʾ�ǽ�����1��ʾ����
        //    half Smoothness;  //0��ʾ��ֲڣ�1��ʾ��⻬
        //    half Occlusion;   //�������ڱΣ�Ĭ��Ϊ1
        //    fixed Alpha;      //͸��ͨ��      
        //}

        //SurfaceOutputStandardSpecular�ṹ�壨��Unity�����ļ� UnityPBSLighting.cginc ��������
        //�������ڱ���ָ����ʹ�� StandardSpecular ����ģ��ʱ������Ҫʹ�øýṹ��
        //����һ�����Ϊ�߹⹤��������ṹ��
        //struct SurfaceOutputStandardSpecular
        //{
        //    fixed3 Albedo;    //������
        //    fixed3 Normal;    //���߿ռ䷨��
        //    fixed3 Emission;  //�Է���
        //    half Smoothness;  //0��ʾ��ֲڣ�1��ʾ��⻬
        //    half Occlusion;   //�������ڱΣ�Ĭ��Ϊ1
        //    fixed Alpha;      //͸��ͨ��      
        //}
        #endregion

        #region ֪ʶ���� ������ɫ������Ⱦ����
        //ͨ���⼸�ڿεĽ��⣬�����Ѿ��˽���
        //����ָ��ṹ�塢�Զ��庯��������ָ���еı��溯��������ģ�͡����㺯����������ɫ�޸ĺ�����
        //����ֻ��Ҫ�˽����ǵľ������ñ����֪����α�д������ɫ��
        //����������ͨ��һ��ͼ���˽���
        //������ɫ�����յ���Ⱦ��������ΰ����Ǵ���������
        #endregion

        #region ֪ʶ���� Ϊʲô��һ��ʼѧϰ������ɫ��
        //������ɫ���Ƕ���/ƬԪ��ɫ���ķ�װ��Ҫ�˽��˶���/ƬԪ��ɫ���������˽������ɫ���ı���ԭ��
        //ѧ���˸����ײ㡱�Ķ���/ƬԪ��ɫ����д���Ժ�ѧϰ������Shader���ԣ��Ż���ӵ���Ӧ�֣�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


