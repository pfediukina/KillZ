using UnityEngine;
using UnityEngine.Windows;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public override void FixedUpdateNetwork()
    {
        Move();
    }

    private void Move()
    {
        if (!GetInput(out NetworkInputData data)) return;
        if (data.Direction.x != 0) _spriteRenderer.flipX = data.Direction.x > 0 ? false : true;
        transform.Translate(data.Direction * _info.StartSpeed * Runner.DeltaTime);
    }
}