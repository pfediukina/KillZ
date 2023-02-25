using UnityEngine;
using UnityEngine.Windows;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int inner = 0;

    public override void FixedUpdateNetwork()
    {
        Move();
    }

    private void Move()
    {
        GetInput(out NetworkInputData data);
        if(data.Direction == Vector2.zero) return;

        inner++;
        Debug.Log(inner);
        if (data.Direction.x != 0) _spriteRenderer.flipX = data.Direction.x > 0 ? false : true;
        transform.Translate(data.Direction * _info.StartSpeed * Runner.DeltaTime);
    }
}