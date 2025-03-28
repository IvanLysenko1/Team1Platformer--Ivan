using System.Collections;
using UnityEngine;

public class DinosaurTrap : TrapBase
{
    [SerializeField] private Transform _leftPart;
    [SerializeField] private Transform _rightPart;
    [SerializeField] private float _leftTargetAngle = -90;
    [SerializeField] private float _rigthtTargetAngle = 90;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _deathDelay = 0.5f;
    private bool _isStoneInTrap = false;
    private Rigidbody2D _stoneInTrapRigidbody;
    private Collider2D _trapTriggerCollider;
    private Transform _stoneInTrapCollider;
    private Quaternion _leftStartRotation;
    private Quaternion _rightStartRotation;

    private void Start()
    {
        _trapObject.gameObject.SetActive(false);
        _trapTriggerCollider = GetComponent<Collider2D>();
        _leftStartRotation = _leftPart.rotation;
        _rightStartRotation = _rightPart.rotation;
    }

    protected override void HandleTriggerEnter(Collider2D collision)
    {
        if(collision.TryGetComponent(out Interactable movable))
        {
            _isStoneInTrap = true;
            _trapTriggerCollider.enabled = false;
            _stoneInTrapRigidbody = movable.GetComponent<Rigidbody2D>();
            _stoneInTrapCollider = movable.GetComponent<Transform>();
            movable.enabled = false;
            StartCoroutine(CloseJaws());
        }
        else if (collision.TryGetComponent(out CharacterFire character))
        {
            StartCoroutine(CloseJaws());
        }
    }

    private IEnumerator CloseJaws()
    {
        Debug.Log("Start close");
        Quaternion leftRotation = _leftStartRotation;
        Quaternion leftTargetRotation = Quaternion.Euler(0, 0, _leftTargetAngle);

        Quaternion rightRotation = _rightStartRotation;
        Quaternion rightTargetRotation = Quaternion.Euler(0, 0, _rigthtTargetAngle);

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            float t = elapsedTime / _duration;
            _leftPart.rotation = Quaternion.Lerp(leftRotation, leftTargetRotation, t);
            _rightPart.rotation = Quaternion.Lerp(rightRotation, rightTargetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _leftPart.rotation = leftTargetRotation;
        _rightPart.rotation = rightTargetRotation;

        if (_isStoneInTrap)
        {
            _stoneInTrapRigidbody.bodyType = RigidbodyType2D.Static;
            _stoneInTrapCollider.position = transform.position;
            yield break;
        }
        yield return new WaitForSeconds(_deathDelay);
        _trapObject.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        _leftPart.rotation = _leftStartRotation;
        _rightPart.rotation = _rightStartRotation;
        _trapObject.gameObject.SetActive(false);
    }
}

