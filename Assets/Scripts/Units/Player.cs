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

    private void Move()
    {
        GetInput(out NetworkInputData data);
        if (data.Direction.x != 0) _spriteRenderer.flipX = data.Direction.x > 0 ? false : true;
        transform.Translate(data.Direction * _info.StartSpeed * Runner.DeltaTime);
    }
}