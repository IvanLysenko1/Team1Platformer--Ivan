using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingEffect : MonoBehaviour
{
    public Light2D lightningLight; // �������� �����
    public float minDelay = 0.3f; // ����������� ����� ����� ��������
    public float maxDelay = 8f; // ������������ ����� ����� ��������
    public float flashDuration = 0.1f; // ������������ �������

    void Start()
    {
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay)); // ���� ��������� �����

            // �������� �������
            lightningLight.enabled = true;
            yield return new WaitForSeconds(flashDuration);
            lightningLight.enabled = false;

            // �������������� ������� (�����)
            if (Random.value > 0.5f)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
                lightningLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                lightningLight.enabled = false;
            }
        }
    }
}
