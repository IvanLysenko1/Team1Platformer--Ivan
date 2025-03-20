using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    private float _timeElapsed; // �����, ��������� � ������ �������
    private bool _isRunning; // ����, �����������, ������� �� ������

    public event Action<int, int> OnTimeChanged; // ������� ��� ���������� ������� (������, �������)

    void Start()
    {
        _timeElapsed = 0f;
        _isRunning = true;
        StartCoroutine(UpdateTimerCoroutine()); // ��������� �������� ��� ���������� �������
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (_isRunning)
        {
            _timeElapsed++; // ����������� ����� �� 1 �������
            UpdateTimerText(); // ��������� ����� �������
            yield return new WaitForSeconds(1f); // ���� 1 �������
        }
    }

    private void UpdateTimerText()
    {
        // ����������� ����� � ������ � �������
        int minutes = Mathf.FloorToInt(_timeElapsed / 60);
        int seconds = Mathf.FloorToInt(_timeElapsed % 60);
        OnTimeChanged?.Invoke(minutes, seconds); // ���������� ����������� �� ��������� �������
    }

    public void StopTimer()
    {
        _isRunning = false; // ������������� ������
    }

    public void StartTimer()
    {
        _isRunning = true; // ��������� ������
    }
}
