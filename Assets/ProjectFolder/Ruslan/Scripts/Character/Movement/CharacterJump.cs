using System;
using UnityEngine;

public class CharacterJump : MonoBehaviour, IJumpable
{
    public Action OnJump;
    private Rigidbody2D _rb;
    private CharacterMovementHandler _movementHandler; // ��������� ������ �� CharacterMovementHandler

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private bool _isVelocityMode = false;
    private float _gravityAbs;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementHandler = GetComponent<CharacterMovementHandler>(); // �������� ������ �� CharacterMovementHandler
    }

    private void Start()
    {
        _gravityAbs = Mathf.Abs(Physics2D.gravity.y);
    }

    public void Jump()
    {
        if (_movementHandler.IsGrounded()) // ���������� ����� �� CharacterMovementHandler
        {
            _rb.linearVelocityY = _jumpForce;
            OnJump?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if(!_movementHandler.IsGrounded())
        {
            ApplyCustomGravity();
        }
    }

    private void ApplyCustomGravity()
    {
        if(_isVelocityMode)
        {
            if (_rb.linearVelocityY < 0)
            {
                _rb.linearVelocityY += Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (_rb.linearVelocityY > 0)
            {
                _rb.linearVelocityY += Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (_rb.linearVelocityY < 0)
            {
                _rb.AddForce(Vector2.down * (_fallMultiplier - 1) * _gravityAbs, ForceMode2D.Force);
            }
            else if (_rb.linearVelocityY > 0)
            {
                _rb.AddForce(Vector2.down * (_lowJumpMultiplier - 1) * _gravityAbs, ForceMode2D.Force);
            }
        }
    }
}
