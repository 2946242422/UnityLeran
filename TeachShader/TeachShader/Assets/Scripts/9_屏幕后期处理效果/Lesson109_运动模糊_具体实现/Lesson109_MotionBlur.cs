using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson109_MotionBlur : PostEffectBase
{
    [Range(0,0.9f)]
    public float blurAmount = 0.5f;
    //�ѻ����� ���ڴ洢֮ǰ��Ⱦ�Ľ���� ��Ⱦ����
    private RenderTexture accumulationTex;

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            //��ʼ���ѻ����� ���Ϊ�� ���� ��Ļ��߱仯�� ����Ҫ���³�ʼ��
            if( accumulationTex == null ||
                accumulationTex.width != source.width ||
                accumulationTex.height != source.height)
            {
                DestroyImmediate(accumulationTex);
                //��ʼ��
                accumulationTex = new RenderTexture(source.width, source.height, 0);
                accumulationTex.hideFlags = HideFlags.HideAndDontSave;
                //��֤��һ�� �ۻ�������Ҳ�������� ��Ϊ֮�� ������ɫ ����Ϊ��ɫ�������е���ɫ
                Graphics.Blit(source, accumulationTex);
            }
            //1 - ģ���̶ȵ�Ŀ�� ����Ϊ ϣ���󵽵�Ч���� ģ���̶�ֵԽ�� Խģ��
            //��ΪShader�еĻ�����ӵļ��㷽ʽ������ ��� ������Ҫ1 - ��
            material.SetFloat("_BlurAmount", 1.0f - blurAmount);

            //�������ǵĲ��� ���л�ϴ���
            //�ڶ������� ������ʱ  ������Ϊ��ɫ����������ɫ�����д���
            //û��ֱ��д��Ŀ���е�Ŀ�� Ҳ�ǿ���ͨ��accumulationTex��¼��ǰ��Ⱦ���
            //��ô����һ��ʱ �����൱������һ�εĽ����
            Graphics.Blit(source, accumulationTex, material); 

            Graphics.Blit(accumulationTex, destination);
        }
        else
            Graphics.Blit(source, destination);
    }

    /// <summary>
    /// ����ű�ʧ�� ��ô���ۻ�����ɾ����
    /// </summary>
    private void OnDisable()
    {
        DestroyImmediate(accumulationTex);
    }
}
