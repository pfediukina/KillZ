using UnityEngine;
using UnityEngine.Windows;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerInput _input;

    private void OnEnable()
    {
        _input.OnMovePerfomed += Move;
    }

    private void Move(Vector2 dir)
    {
        if (dir.x != 0) _spriteRenderer.flipX = dir.x > 0 ? false : true;

        Vector3 move = Vector3.zero;
        move.x = dir.x;
        move.y = dir.y;
        transform.Translate(dir * _info.StartSpeed * Runner.DeltaTime); // position += dir * _info.StartSpeed * Runner.DeltaTime;
        
    }
}