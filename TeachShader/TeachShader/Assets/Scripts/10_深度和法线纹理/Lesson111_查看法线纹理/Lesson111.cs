using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson111 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع� ���������ʹ��
        //��һ������C#��������������������ģʽ
        //�������+��������
        //Camera.main.depthTextureMode = DepthTextureMode.DepthNormals;

        //�ڶ�������Shader�����а�������������
        //sampler2D _CameraDepthNormalsTexture;

        //��������ʹ��
        //���ڴ洢���ֵ�ı���
        //float depth;
        //���ڴ洢���ߵı���
        //float3 normals;

        //�����+����������в���������xy�Ƿ�����Ϣ��zw�������Ϣ��
        //float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);

        //UnityCG.cginc �����ļ��еķ��� ���ڵõ����ֵ(0~1)�ͷ�����Ϣ(�۲�ռ���)
        //�൱��һ���Դ�����Ⱥͷ���
        //  DecodeDepthNormal(depthNormal, depth, normals);
        //�����õ����
        //  depth = DecodeFloatRG(depthNormal.zw);
        //�����õ�����
        //  normals = DecodeViewNormalStereo(depthNormal);
        #endregion

        #region ֪ʶ��һ ��β鿴����������Ϣ
        //���ǿ�������Ļ���ڴ�����������ѧϰ���Ļ�ȡ������Ϣ��֪ʶ��
        //��������Ϊ��ɫ��ʾ����Ļ��
        //���ܷ��������д洢������
        #endregion

        #region ֪ʶ��� ʵ�� �鿴����������Ļ���ڴ���Ч�� ��Ӧ Shader
        //1.�½�Shader��ɾ�����ô���
        //2.��������
        //  ��Shader��ͨ������_CameraDepthNormalsTexture��ȡ���+��������
        //3.������ɫ��
        //  ֻ��Ҫ�޸Ĵ���ṹ������
        //4.ƬԪ��ɫ��
        //  �����+����������в���������xy�Ƿ�����Ϣ��zw�������Ϣ��
        //  float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
        //  UnityCG.cginc �����ļ��еķ��� ���ڵõ����ֵ(0~1)�ͷ�����Ϣ(�۲�ռ���)
        //  �൱��һ���Դ�����Ⱥͷ���
        //  ���ڴ洢���ֵ�ı���
        //  float depth;
        //  ���ڴ洢���ߵı���
        //  float3 normals;
        //  DecodeDepthNormal(depthNormal, depth, normals);
        //  �����õ����
        //      depth = DecodeFloatRG(depthNormal.zw);
        //  �����õ�����
        //      normals = DecodeViewNormalStereo(depthNormal);
        //5.Fallback Off
        #endregion

        #region ֪ʶ���� ʵ�� �鿴����������Ļ���ڴ���Ч�� ��Ӧ C#����
        //1.�½�C#�ű���������Shader��ͬ
        //2.�̳�PostEffectBase
        //3.��Start�����������������ģʽ
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
