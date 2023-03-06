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

    public override void FixedUpdateNetwork()
    {
        if (CurrentState != null)
        {
            //CurrentState.Data = data;
            CurrentState.Update();
        }
    }

    public void Initialize(Unit unit)
    {
        _unit = unit;
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

        AddState(new IdleState(_unit));
        AddState(new DeadState(_unit));
    }

    private void SetStateByDefault()
    {
        SetState<IdleState>();
    }
}

