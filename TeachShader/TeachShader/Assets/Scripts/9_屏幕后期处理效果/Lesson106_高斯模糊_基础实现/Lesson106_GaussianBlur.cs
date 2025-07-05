using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson106_GaussianBlur : PostEffectBase
{
    [Range(1,8)]
    public int downSample = 1;
    [Range(1,16)]
    public int iterations = 1;
    [Range(0,3)]
    public float blurSpread = 0.6f;

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //base.OnRenderImage(source, destination);
        if (material != null)
        {
            int rtW = source.width / downSample;
            int rtH = source.height / downSample;

            //׼��һ��������
            RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
            //����˫���Թ���ģʽ������ ����������Ч����ƽ��
            buffer.filterMode = FilterMode.Bilinear;
            //ֱ������д�뵽����������
            Graphics.Blit(source, buffer);
            //��ʹ�ò�����֮ǰ���� ģ���뾶����
            //material.SetFloat("_BlurSpread", blurSpread);

            //���ȥִ�� ��˹ģ���߼�
            for (int i = 0; i < iterations; i++)
            {
                //�����Ҫģ���뾶Ӱ��ģ�������ǿ�� ��ƽ��
                //һ����������ǵĵ����н������� �൱��ÿ�ε��������˹ģ��ʱ �����������ǵļ������
                material.SetFloat("_BlurSpread", 1 + i * blurSpread);

                //������һ���µĻ�����
                RenderTexture buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);

                //��Ϊ������Ҫ������Pass ����ͼ������ 
                //���е�һ�� ˮƽ�������
                Graphics.Blit(buffer, buffer1, material, 0); //Color1
                //��ʱ �ؼ����ݶ���buffer1�� bufferû���� �ͷŵ�
                RenderTexture.ReleaseTemporary(buffer);

                buffer = buffer1;
                buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                //���еڶ��� ��ֱ�������
                Graphics.Blit(buffer, buffer1, material, 1);//��Color1�Ļ����ϳ���Color2 �õ����յĸ�˹ģ��������
                                                                //�ͷŻ�����
                RenderTexture.ReleaseTemporary(buffer);
                //buffer��buffer1ָ��Ķ�����һ�θ�˹ģ������Ľ��
                buffer = buffer1;
            }
            //��forѭ���еõ����յ�ģ����� Ȼ��д�뵽Ŀ��������
            Graphics.Blit(buffer, destination);
            //�ͷŵ�������
            RenderTexture.ReleaseTemporary(buffer);
        }
        else
            Graphics.Blit(source, destination);
    }
}
