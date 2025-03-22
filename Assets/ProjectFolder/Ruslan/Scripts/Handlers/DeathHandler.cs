using System;
using UnityEngine;

public class DeathHandler : MonoBehaviour 
{
    public Action OnDeath;
    private CharacterDeath _characterDeath;

    //public DeathHandler(CharacterDeath characterDeath)
    //{
    //    _characterDeath = characterDeath;
    //    _characterDeath.OnDeathTriggerEntered += Death;
    //}

    private void Awake()
    {
        _characterDeath = FindFirstObjectByType<CharacterDeath>();
    }

    private void OnEnable()
    {
        _characterDeath.OnDeathTriggerEntered += Death;
    }

    private void OnDisable()
    {
        _characterDeath.OnDeathTriggerEntered -= Death;
    }

    public void Unsubscribe()
    {
        _characterDeath.OnDeathTriggerEntered -= Death;
    }

    private void Death()
    {
        // ��� ��� ��� ���������� ��� ������, ���� ���� ������� ����, ��������� ���������� ������� ��������� � �.�.
        OnDeath?.Invoke();
    }
}
