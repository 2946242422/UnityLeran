using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson101 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ�ع�
        //��Ļ���ڴ���Ч���Ļ���ʵ��ԭ��
        //�������� OnRenderImage���� �� Graphics.Blit����
        //����ȡ��ǰ��Ļ���沢����Shader�Ը���������Զ��崦��

        //������Ĺؼ� ���� void OnRenderImage(RenderTexture source, RenderTexture destination)

        //ʵ��Ч���Ĺؼ� ���� Graphics.Blit (Texture source, RenderTexture dest, Material mat, int pass= -1);
        #endregion

        #region ֪ʶ�㲹��
        //1.Shader.isSupported
        //  ����ж�Shader��Ŀ��ƽ̨��Ӳ�����Ƿ�����ȷ����
        //  ���ǿ���ͨ����ȡShader�����е�isSupported�����ж�
        //  �������false,��֧��
        //  �������true,֧��

        //2.[ExecuteInEditMode]����
        //  ����ʹ�ű��ڱ༭��ģʽ��Ҳ��ִ��

        //3.[RequireComponent(typeof(�����))]����
        //  ָ��ĳ���ű����������������ȷ�����㽫�ű����ӵ���Ϸ����ʱ��
        //  ��������Ҳ���Զ���ӵ�����Ϸ������
        //  �����Щ����Ѿ����ڣ����ǲ��ᱻ�ظ����
        //  ��Ϊ����ű�һ����ӵ�������ϣ���������������������

        //4.�������е� HideFlags ö��
        //  �Ӳ���������п��Ե�� HideFlags ö��
        //  HideFlags.None: ��������ȫ�ɼ��Ϳɱ༭�ġ�����Ĭ��ֵ��
        //  HideFlags.HideInHierarchy: �����ڲ㼶��ͼ�б����أ�����Ȼ�����ڳ����С�
        //  HideFlags.HideInInspector: �����ڼ�����б����أ�����Ȼ�����ڲ㼶��ͼ�С�
        //  HideFlags.DontSaveInEditor: ���󲻻ᱻ���浽�����С������ڱ༭��ģʽ������Ӱ�첥��ģʽ��
        //  HideFlags.NotEditable: �����ڼ��������ֻ���ģ����ܱ��޸ġ�
        //  HideFlags.DontSaveInBuild: ���󲻻ᱻ�����ڹ����С�
        //  HideFlags.DontUnloadUnusedAsset: ��������Դ����ʱ���ᱻж�أ���ʹ��û�б����á�
        //  HideFlags.DontSave: ���󲻻ᱻ���浽�����У������ڹ����б��棬Ҳ�����ڱ༭���б��档
        //                      ���� DontSaveInEditor | DontSaveInBuild | DontUnloadUnusedAsset ����ϡ�
        //  �����Ҫ����ö������������ ֱ�Ӷ��ö�� ����λ�����㼴�� |
        #endregion

        #region ֪ʶ��һ ΪʲôҪʵ����Ļ�������
        //ԭ��һ��
        //Ϊ��ʵ����Ļ���ڴ���Ч��
        //����ÿ�ζ���Ҫ��������һ����
        //1.ʵ��һ���̳���MonoBehaviour���Զ���C#�ű�
        //2.������Ӧ�Ĳ��������Shader
        //3.ʵ��OnRenderImage����
        //4.��OnRenderImage������ʹ��Graphics.Blit����
        //��ô��Щ��ͬ��������ȫ���Գ���һ��������ȥ���
        //�Ժ�ֻ��Ҫ��������ʵ�ָ��ԵĻ����߼�����

        //ԭ�����
        //���ǿ����ڻ������ô��붯̬����������
        //����ҪΪÿ������Ч�����ֶ�����������
        //ֻ��Ҫ��Inspector���ڹ�����Ӧʹ�õ�Shader����

        //ԭ������
        //�ڽ�����Ļ����֮ǰ������������Ҫ���һϵ�������Ƿ�����
        //���磺
        //��ǰƽ̨�Ƿ�֧�ֵ�ǰʹ�õ�Unity Shader
        //���ǿ����ڻ����н����жϣ�����ÿ����д��ͬ�߼�

        //ע�⣺
        //��һЩ�ϰ汾�У�����ܻ����ڻ������ж�Ŀ��ƽ̨�Ƿ�֧����Ļ�������Ⱦ����
        //һ��ͨ��Unity�е�SystemInfo���ж�
        //�����������ȷ���ײ�ƽ̨��Ӳ����صĹ����Ƿ�֧��
        //�ٷ�˵����https://docs.unity.cn/cn/2022.3/ScriptReference/SystemInfo.html

        //��������ʱ����չ��Ŀǰ�������е��ִ�ͼ��Ӳ������֧����Ļ�������Ⱦ������
        //������������ٽ������Ƶ��жϵ�
        //ֻ��Ҫ�ж�Shader�Ƿ�֧�ּ���
        #endregion

        #region ֪ʶ��� ʵ�ֻ��๦��
        //��ҪĿ��
        //1.�������࣬��������Camera�����������ڱ༭ģʽ�¿����У���֤���ǿ�����ʱ����Ч��
        //2.���������� ���� Shader��������Inspector���ڹ���
        //3.���������� ˽�� Material�����ڶ�̬����
        //4.������ʵ���ж�Shader�Ƿ���ã����Ҷ�̬����Material�ķ���
        //5.������ʵ��OnRenderImage���鷽������ɻ����߼�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
