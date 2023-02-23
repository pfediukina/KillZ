using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour, IMovable
{
    public float MoveSpeed { get => _info.StartSpeed;}

    private PlayerInput _input;

    [SerializeField] private PlayerInfo _info;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(_input != null)
            Move(_input.ReturnDirection());
    }

    public void Move(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        transform.eulerAngles = Vector3.up * (direction.x > 0 ? 0 : 180);

        transform.position += Vector3.right * direction.x * MoveSpeed * Time.deltaTime;
        transform.position += Vector3.up * direction.y * MoveSpeed * Time.deltaTime;
    }
}