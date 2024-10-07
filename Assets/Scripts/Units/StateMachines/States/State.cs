using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class State
{
    protected StateMachine _stateMachine;
    protected StateInfo _stateInfo;
    protected UnitController _unitController;

    protected State(StateMachine stateMachine, UnitController unitController)
    {
        _stateMachine = stateMachine;
        _unitController = unitController;
    }

    public virtual void Entry(StateInfo cfg = null)
    {
        _stateInfo = cfg;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
    }

    public virtual void OnTriggerExit(Collider other)
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
    }
}