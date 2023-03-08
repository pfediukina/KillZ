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
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private NetworkAnimator _anim;

    [HideInInspector][Networked(OnChanged = nameof(OnScoreChange))] public int Score { get; set; }

    public static void OnScoreChange(Changed<Player> changed)
    {
        if(changed.Behaviour.HasInputAuthority) 
            changed.Behaviour.UI.UpdateScore(changed.Behaviour.Score);
    }

    public PlayerInput Input
    {
        get
        {
            if (_input != null)
                return _input;
            else
                _input = GetComponent<PlayerInput>();
            return _input;

        }
    }
    private PlayerInput _input;

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

    public Action OnPlayerPressedMenu;

    protected override void Awake()
    {
        base.Awake();
        States.AddState(new MoveState(this));

        Input.OnAttackPressed += ctx =>
        {
            if (Weapon != null && States.CurrentState is not DeadState)
            {
#if UNITY_ANDROID
                Weapon.Shoot(ctx);
                _anim.RotateWeaponToDirection(ctx, Weapon.GetSprite());
#else
                var mouse = Camera.main.ScreenToWorldPoint(ctx);
                Weapon.Shoot(mouse - transform.position);
#endif
            }
        };

        Input.OnBackPressed += PressedMenu;
        Input.OnViewChanged += ctx => {
            if (Weapon != null)
            {
#if UNITY_ANDROID
#else
                _anim.CalculateAndRotateWeapon(ctx, Weapon.GetSprite());
#endif
            }
        };
        Input.OnViewChanged += UI.FollowPoint;
    }

    private void Start()
    {
        if (HasInputAuthority)
        {
            _camera.Priority = 10;
        }
        Score = 0;

        Health.OnHealthChanged += UI.HealthUI.UpdateHealth;
        UI.OnDisconnect = OnDisconnect;
        Health.ResetHealth();
        UI.ShowStartGameButton(this);
    }
    private void FixedUpdate()
    {
        if (!GameMaster.EnableTimer) OnDisconnect(0);
    }

    public void OnDisconnect()
    {
        Debug.Log("Dead exit");
        Runner.Shutdown();
        SceneManager.LoadScene(0);
    }

    public void OnDisconnect(int state)
    {
        Debug.Log("Dead");
        UI.ShowGameOver(state, OnDisconnect);
    }


    private void PressedMenu()
    {
        if (HasInputAuthority)
        {
            UI.SwitchPlayerMenu();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data) && !IsDead)
        {
            if (data.Direction != Vector2.zero && States.CurrentState is not MoveState)
            {
                States.SetState<MoveState>();
            }
            else if (data.Direction == Vector2.zero && States.CurrentState is not IdleState)
            {
                States.SetState<IdleState>();
            }
        }
    }


    public override void TakeDamage(Unit from, int damage)
    {
        Health.CurrentHealth -= damage;
    }
}