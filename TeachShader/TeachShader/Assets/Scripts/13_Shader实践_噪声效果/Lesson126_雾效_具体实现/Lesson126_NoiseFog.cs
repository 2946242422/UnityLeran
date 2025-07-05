using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson126_NoiseFog : PostEffectBase
{
    //�����ɫ
    public Color fogColor = Color.gray;
    //���Ũ��
    [Range(0, 3)]
    public float fogDensity = 1f;
    //��ʼ�ľ���
    public float fogStart = 0f;
    //����Ũʱ�ľ���
    public float fogEnd = 5;

    //4x4�ľ��� ���ڴ��� 4����������
    private Matrix4x4 rayMatrix;

    public Texture noiseTexture;
    public float noiseAmount;
    public float fogXSpeed;
    public float fogYSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    protected override void UpdateProperty()
    {
        if (material != null)
        {
            //�õ������ �ӿ� �н�
            float fov = Camera.main.fieldOfView / 2f;
            //�õ����ü������
            float near = Camera.main.nearClipPlane;
            //�õ����ڱ���
            float aspect = Camera.main.aspect;

            //����ߵ�һ�� 
            float halfH = near * Mathf.Tan(fov * Mathf.Deg2Rad);
            //���һ��
            float halfW = halfH * aspect;

            //������ֱ���ϵĺ�ˮƽ���ҵ�ƫ������
            Vector3 toTop = Camera.main.transform.up * halfH;
            Vector3 toRight = Camera.main.transform.right * halfW;
            //���ָ���ĸ����������
            Vector3 TL = Camera.main.transform.forward * near + toTop - toRight;
            Vector3 TR = Camera.main.transform.forward * near + toTop + toRight;
            Vector3 BL = Camera.main.transform.forward * near - toTop - toRight;
            Vector3 BR = Camera.main.transform.forward * near - toTop + toRight;
            //Ϊ�������ֵ ����������������� ������Ҫ����һ������ֵ
            float scale = TL.magnitude / near;
            //������������Ҫ��������������
            TL = TL.normalized * scale;
            TR = TR.normalized * scale;
            BL = BL.normalized * scale;
            BR = BR.normalized * scale;

            rayMatrix.SetRow(0, BL);
            rayMatrix.SetRow(1, BR);
            rayMatrix.SetRow(2, TR);
            rayMatrix.SetRow(3, TL);

            //���ò������������(Shader����)
            material.SetColor("_FogColor", fogColor);
            material.SetFloat("_FogDensity", fogDensity);
            material.SetFloat("_FogStart", fogStart);
            material.SetFloat("_FogEnd", fogEnd);
            material.SetMatrix("_RayMatrix", rayMatrix);

            material.SetTexture("_Noise", noiseTexture);
            material.SetFloat("_NoiseAmount", noiseAmount);
            material.SetFloat("_FogXSpeed", fogXSpeed);
            material.SetFloat("_FogYSpeed", fogYSpeed);
        }
    }
}
