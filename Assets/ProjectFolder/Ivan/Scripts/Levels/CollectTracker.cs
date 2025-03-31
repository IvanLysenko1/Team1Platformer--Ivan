using UnityEngine;

public class CollectTracker : MonoBehaviour
{
    [SerializeField] private CollectHandler _collectHandler; // ������ �� CollectHandler

    private int _currentCollected; // ������� ���������� ��������� ���������

    private void OnEnable()
    {
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged += HandleCollectUpdate;
        }
    }

    private void OnDisable()
    {
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged -= HandleCollectUpdate;
        }
    }

    // ���������� ������� ��������� ���������� ���������
    private void HandleCollectUpdate(int collectedCount)
    {
        _currentCollected = collectedCount;
        Debug.Log($"������� ���������: {_currentCollected}");      
    }

    // ����� ��� ��������� �������� ���������� (���� ����� �� ������� �������)
    public int GetCurrentCollected()
    {
        return _currentCollected;
    }
}