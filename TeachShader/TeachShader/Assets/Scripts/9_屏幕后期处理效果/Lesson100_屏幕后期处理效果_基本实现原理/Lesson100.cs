using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson100 : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ʲô����Ļ���ڴ���Ч��
        //��Ļ���ڴ���Ч���� Screen Post-Processing Effects����һ������Ⱦ���ߵ����׶�Ӧ�õ��Ӿ�Ч��
        //�������ڳ�����Ⱦ��ɺ������ͼ����и��ֵ�����Ч�������Ӷ���ǿ�Ӿ�����
        //��������Ļ���ڴ���Ч���У�
        //���ģ����ɫ�ʵ��� �ȵ�

        //˵�˻���
        //��Ļ���ڴ���Ч�����ǵ���Ϸ������Ⱦ��Ϻ�
        //ͨ����ȡ���û�����Ϣ���ж����Ч������
        #endregion

        #region ֪ʶ��� Unity�� ��Ļ���ڴ���Ч���� ����ʵ��ԭ��
        //��֪ʶ��һ�����ǿ���֪��
        //��Ҫ�����Ļ���ڴ���Ч��
        //��ؼ�����������
        //1.��λ�ȡ ��Ϸ������Ⱦ��Ϻ�Ļ�����Ϣ
        //2.���Ϊ ��ȡ���Ļ�����Ϣ����Զ���Ч��
        //ֻҪ����������㣬��Ȼ�������˻���ʵ��ԭ��

        //1.��λ�ȡ ��Ϸ������Ⱦ��Ϻ�Ļ�����Ϣ
        //  ����֮ǰ��ѧϰ��Ⱦ����ʱѧϰ��
        //  ��Unity�л�ȡ��Ⱦ����ĳ��÷���������
        //  RenderTexture��GrabPass��OnRenderImage
        //  �����ڴ�����Ļ���ڴ���Ч��ʱ��ʹ��
        //  OnRenderImage��������ȡ ��Ϸ������Ⱦ��Ϻ�Ļ�����Ϣ

        //2.���Ϊ ��ȡ���Ļ�����Ϣ����Զ���Ч��
        //  ��Ҫ˼·�ǽ���ȡ������Ϸ������Ϊ �Զ���Shader��������
        //  ͨ���Զ���Shader���ò���Ļ�����ʵ���Զ���Ч��
        #endregion

        #region ֪ʶ���� ������Ĺؼ�����OnRenderImage����
        //OnRenderImage����
        //�����ڼ̳���MonoBehaviour�Ľű����ܹ����Զ����õĺ����������������ں�����
        //������ͼ�����Ⱦ������ɺ����
        //���Ĺ̶�д���ǣ�
        //void OnRenderImage(RenderTexture source, RenderTexture destination)
        //��һ��������Դ��Ⱦ������ǰ��Ⱦ�õ�����Ļͼ��洢�ڸò�������
        //�ڶ���������Ŀ����Ⱦ����������������ͼ��д�뵽Ŀ���������������յ���ʾ

        //ͨ���ú������Ǳ���Եõ���ǰ��Ⱦ����Ϸ����
        //���ڸú����жԻ����Ӧ����Ⱦ������д��������������ʾ

        //ע�⣺
        //  �ú����õ���Դ����Ĭ���������еĲ�͸����͸����Passִ����Ϻ���õ�
        //  ���ڸ�Դ��������޸Ļ����Ϸ������������Ϸ�������Ӱ��
        //  �������Ҫ�ڲ�͸����Passִ����Ϻ�͵��øú�����ֻ��Ҫ�ڸú���ǰ��������
        //  [ImageEffectOpaque]
        //  �����Ͳ����͸���������Ӱ��
        #endregion

        #region ֪ʶ���� ʵ��Ч���Ĺؼ�����Graphics.Blit����
        //Graphics.Blit����
        //���ڽ�һ��ͼ���һ�������Ƶ���һ������
        //ͬʱ�������������������ɫ����ͼ����д���
        //���кܶ����أ�������Ҫ���⼸�����õģ�

        //1.��Դ����ֱ�Ӹ��Ƶ�Ŀ������
        //  Graphics.Blit (Texture source, RenderTexture dest)

        //2.��Դ�����Ƶ�Ŀ������Ӧ��һ������
        //  Graphics.Blit (Texture source, RenderTexture dest, Material mat, int pass= -1);
        //  sourceԴ����ᱻ���ݸ�mat������Shader����Ϊ_MainTex�������������ڽ��д���
        //  pass����Ĭ��ֵΪ-1����ʾ�����ε���Shader�ڵ�����Pass���д�������ֻ����ø���������Pass
        #endregion

        #region �ܽ�
        //��Ļ���ڴ���Ч���Ļ���ʵ��ԭ��
        //�������� OnRenderImage���� �� Graphics.Blit����
        //����ȡ��ǰ��Ļ���沢����Shader�Ը���������Զ��崦��
        #endregion
    }
    //��������� �Ͳ����͸���������Ӱ��
    //[ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        ////1.��Դ����ֱ�Ӹ��Ƶ�Ŀ������
        //Graphics.Blit(source, destination);
        //��Դ���� ͨ�� �������е�Shader����Ч������ Ȼ��д�뵽Ŀ�������� ���ճ�������Ļ��
        Graphics.Blit(source, destination, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
