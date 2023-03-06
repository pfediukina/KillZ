using Cinemachine;
using Fusion;
using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [Networked(OnChanged = nameof(OnScoreChanged))] public int Score { get; set; }

    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private NetworkAnimator _anim;

    public Transform WeaponPlace;

    public BaseWeapon Weapon
    {
        get
        {
            if (_weapon != null)
                return _weapon;
            else
            {
                _weapon = WeaponPlace.GetComponentInChildren<BaseWeapon>();
            }
            return _weapon;
        }
        set
        {
            _weapon = value;
            _weapon.transform.parent = WeaponPlace;
        }
    }
    private BaseWeapon _weapon;
    public PlayerUI UI
    {
        get
        {
            if (_ui != null)
                return _ui;
            else
                _ui = FindObjectOfType<PlayerUI>();
            return _ui;

        }
    }
    private PlayerUI _ui;

    public PlayerInput Input { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        GetComponents();

        States.AddState(new MoveState(this));
    }

    private void Start()
    {

        BindEvents();
        UI.UpdateScore(Score);

        if (HasInputAuthority)
        {
            _camera.Priority = 10;
            Score = 0;
        }
    }

    public void OnDisconnect()
    {
        Runner.Shutdown();
        SceneManager.LoadScene(0);
    }

    private static void OnScoreChanged(Changed<Player> changed)
    {
        Debug.Log("Here");
        changed.Behaviour.UI.UpdateScore(changed.Behaviour.Score);
    }
    
    private void GetPlayerInput(Vector2 direction)
    {
        if (States.CurrentState is DeadState) return;
        States.GetState<MoveState>().Direction = direction;
        if (direction != Vector2.zero && States.CurrentState is not MoveState)
        {
            States.SetState<MoveState>();
        }
        else if (direction == Vector2.zero && States.CurrentState is not IdleState)
        {
            States.SetState<IdleState>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out var bullet))
        {
            if (States.CurrentState is DeadState) return;
            if (bullet.Owner is Player) return;
            Runner.Despawn(bullet.Object);
            Health.CurrentHealth--;
        }
    }

    private void GetComponents()
    {
        Input = GetComponent<PlayerInput>();
    }

    private void BindEvents()
    {
        Input.OnAttackPressed += ctx =>
        {
            if (Weapon != null)
            {
                Weapon.Shoot(ctx);
            }
        };
        Input.OnViewChanged += ctx => { if (Weapon != null) _anim.CalculateAndRotateWeapon(ctx, Weapon.GetSprite()); };
        Input.OnViewChanged += UI.FollowPoint;
        Input.OnMoved += GetPlayerInput;
        Input.OnBackPressed += () =>  UI.MenuUI.EnableMenu(!UI.MenuUI.IsOpened);

        Health.OnHealthChanged += UI.HealthUI.UpdateHealth;
        UI.MenuUI.OnDisconnect += OnDisconnect;
    }
}