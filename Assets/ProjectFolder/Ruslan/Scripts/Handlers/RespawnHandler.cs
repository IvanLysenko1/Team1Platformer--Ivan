using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] private CharacterRespown _characterRespown;
    [SerializeField] private Transform _initialSpownPosition;
    private CharacterMoveController _characterController;
    private CharacterAnimationController _characterAnimationController;
    private Transform _characterTransform;
    private DeathHandler _deathHandler;
    private Animator _animator;
    private Vector2 _respawnPosition;
    private Coroutine _coroutine;

    private void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
        _characterRespown = FindFirstObjectByType<CharacterRespown>();
        _animator = _characterRespown.GetComponentInChildren<Animator>();
        _characterController = _characterRespown.GetComponent<CharacterMoveController>();
        _characterAnimationController = _animator.GetComponent <CharacterAnimationController>();
    }
    private void OnEnable()
    {
        _deathHandler.OnDeath += RespawnCharacter;
        _characterRespown.OnRespownPoindFound += SetRespanPoint;
        _characterAnimationController.OnDeathAnimCompleat += RespawnCharacter;
        _characterAnimationController.OnRespawnAnimCompleat += ReturnControl;
    }

    private void OnDisable()
    {
        _deathHandler.OnDeath -= RespawnCharacter;
        _characterRespown.OnRespownPoindFound -= SetRespanPoint;
        _characterAnimationController.OnDeathAnimCompleat -= RespawnCharacter;
        _characterAnimationController.OnRespawnAnimCompleat -= ReturnControl;
    }

    private void Start()
    {
        _respawnPosition = _initialSpownPosition.position;
        _deathHandler = GetComponentInChildren<DeathHandler>();
        _characterTransform = _characterRespown.transform;
    }
    public void RespawnCharacter()
    {
        // ��� ��� ��� ������ ����������� ��� �������� ����� ������� �������� ���� ���-�� �������� ���� �������
        _characterTransform.position = _respawnPosition;
        _animator.SetTrigger("respawn");
        //if (_coroutine == null)
        //{
        //    StartCoroutine(RespawnRoutine());
        //}
    }

    public void SetRespanPoint(Vector2 newRespownPoint)
    {
        _respawnPosition = newRespownPoint;
    }

    private void ReturnControl()
    {
        _characterController.enabled = true;
    }
    //IEnumerator RespawnRoutine()
    //{
    //    RespawnCharacter();
    //    _animator.SetTrigger("respawn");
    //    yield return new WaitForSeconds(1);
    //    Debug.Log("�������� ���������!");
    //    _characterController.enabled = true;
    //    _coroutine = null;
    //}
}
