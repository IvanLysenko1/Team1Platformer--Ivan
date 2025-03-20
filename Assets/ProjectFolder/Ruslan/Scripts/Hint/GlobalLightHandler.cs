using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightHandler : MonoBehaviour
{
    [SerializeField] private Light2D _globalLigth;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _spiritualColor;
    [SerializeField] private Coroutine _coroutine;
    [SerializeField] private CharacterHintActivator _characterHintActivator;

    private void Awake()
    {
        _characterHintActivator = FindFirstObjectByType<CharacterHintActivator>();
    }

    private void OnEnable()
    {
        _characterHintActivator.OnShowHint += SetSpiritLighting;
    }

    private void OnDisable()
    {
        _characterHintActivator.OnShowHint -= SetSpiritLighting;
    }

    private void Start()
    {
        _globalLigth = GetComponent<Light2D>();
        _globalLigth.color = _normalColor;
    }

    public void SetSpiritLighting(float duration)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(SpiritLighting(duration));
    }

    private IEnumerator SpiritLighting(float duration)
    {
        // ����� ������������� ���� �� _spiritualColor
        _globalLigth.color = _spiritualColor;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // ������������ �������� ������������
            float progress = elapsedTime / duration;

            // ������������� ����� �� _spiritualColor � _normalColor
            _globalLigth.color = Color.Lerp(_spiritualColor, _normalColor, progress);

            elapsedTime += Time.deltaTime;
            yield return null; // ���� ��������� ����
        }

        // ��������, ��� � ����� ����� ����� ����� ����������� ��� _normalColor
        _globalLigth.color = _normalColor;
    }
}
