using UnityEngine;
using UnityEngine.Windows;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private float _speed;
    //public PlayerInput Input { get; private set; }
    //private NetworkCharacterControllerPrototype _cc;

    //private void Awake()
    //{
    //    _cc = GetComponent<NetworkCharacterControllerPrototype>();
    //}

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.Direction != Vector2.zero)
            {
                Move(data.Direction);
            }
        }
    }

    private void Move(Vector3 dir)
    {
        if (dir.x != 0) transform.eulerAngles = Vector3.up * (dir.x > 0 ? 0 : 180);
        Vector3 move = Vector3.zero;
        move.x = dir.x;
        move.y = dir.y;
        //_cc.Move(move);
        transform.position += dir * _info.StartSpeed * Runner.DeltaTime;
        
    }
}