using UnityEngine;

public class CharacterStateGrounded : CharacterStateBase
{
    public CharacterStateGrounded(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb)
        : base(stateMachine, movement, rb) { }

    public override void Enter()
    {
        Debug.Log("�������� ����� �� �����");
    }

    public override void Update()
    {
        if (!_movement.IsGrounded())
        {
            _stateMachine.Fall();
        }
    }

    public override void Move(Vector2 direction)
    {
        _movement.GroundMovement(direction.x);
    }
}
