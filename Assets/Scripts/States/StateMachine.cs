using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : NetworkBehaviour
{
    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set;}

    private Dictionary<Type, IState> _statesMap;
    private Unit _unit;

    public StateMachine(Unit unit)
    {
        _unit = unit;
        Initialize();
    }

    public override void FixedUpdateNetwork()
    {
        if (CurrentState != null &&
            Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data))
        {
            CurrentState.Data = data;
            CurrentState.Update();
        }
    }

    private void Initialize()
    {
        InitStates();
        SetStateByDefault();
    }

    public void AddState<T>(T state) where T : IState
    {
        _statesMap[typeof(T)] = state;
    }

    public void SetState<T>() where T : IState
    {
        var newState = GetState<T>();
        if (CurrentState != null)
            CurrentState.Exit();

        PreviousState = CurrentState;
        CurrentState = newState;

        newState.Enter();
    }

    public void SetState<T>(NetworkInputData data) where T : IState
    {
        var newState = GetState<T>();
        newState.Data = data;
        SetState<T>();
    }

    public T GetState<T>() where T : IState
    {
        var type = typeof(T);
        return (T)_statesMap[type];
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IState>();

        _statesMap[typeof(IdleState)] = new IdleState(_unit);
    }

    private void SetStateByDefault()
    {
        SetState<IdleState>();
    }
}

