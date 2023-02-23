using UnityEngine;

interface IMovable
{
    public float MoveSpeed { get; }
    public void Move(Vector2 direction);
}