using UnityEngine;
using TMPro; 

[RequireComponent(typeof(TextMeshProUGUI))] // ������������� ��������� ���������, ���� ��� ���
public class UICollectDisplay : MonoBehaviour
{

    [Header("������")]
    [SerializeField] private CollectHandler _collectHandler; // ������ �� CollectHandler
    private TextMeshProUGUI _collectText; // ��������� ������

    private void Awake()
    {
        // �������� ��������� TextMeshProUGUI
        _collectText = GetComponent<TextMeshProUGUI>();

        // ���� ������ �� CollectHandler �� ������ � ����������, ��������� ����� �������������
        if (_collectHandler == null)
        {
            _collectHandler = FindObjectOfType<CollectHandler>();
            if (_collectHandler == null)
            {
                Debug.LogError("UICollectDisplay: CollectHandler �� ������!");
                enabled = false; // ��������� ������, ����� �������� ������
                return;
            }
        }
    }

    private void OnEnable()
    {
        // ������������� �� ������� ��� ���������
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged += UpdateCollectText;
        }
    }

    private void OnDisable()
    {
        // ������������ ��� ����������
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged -= UpdateCollectText;
        }
    }

    // ��������� ����� ��� ��������� ���������� ���������
    private void UpdateCollectText(int collectedCount)
    {
        if (_collectText != null)
        {
            _collectText.text = collectedCount.ToString();
        }
    }
}