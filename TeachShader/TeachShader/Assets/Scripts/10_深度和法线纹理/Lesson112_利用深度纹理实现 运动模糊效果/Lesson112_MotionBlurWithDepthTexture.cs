using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson112_MotionBlurWithDepthTexture : PostEffectBase
{
    [Range(0,1)]
    public float blurSize = 0.5f;
    //���ڼ�¼��һ�εı任����ı���
    private Matrix4x4 frontWorldToClipMatrix;

    private void Start()
    {
        //�����������
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
        //��ʼ����һ�εı任���� �� �۲쵽�ü��任�����������͸�Ӿ��� * ���絽�۲�任����
        //�õ��� ���� ����ռ䵽�ü��ռ�ı任����
        //frontWorldToClipMatrix = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;
    }

    private void OnEnable()
    {
        //��ʱ���ǻ��ڽ������ýű�ʧ�ÿ�μ���ʱ ���Գ�ʼ��һ��
        frontWorldToClipMatrix = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            //����ģ���̶�
            material.SetFloat("_BlurSize", blurSize);
            //������һ֡����ռ䵽�ü��ռ�ľ���
            material.SetMatrix("_FrontWorldToClipMatrix", frontWorldToClipMatrix);
            //������һ֡�ı任����
            frontWorldToClipMatrix = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;
            //������һ֡�� �ü�������ռ�ı任����
            material.SetMatrix("_ClipToWorldMatrix", frontWorldToClipMatrix.inverse);
            //������Ļ���ڴ���
            Graphics.Blit(source, destination, material); 
        }
        else
            Graphics.Blit(source, destination);
    }
}
