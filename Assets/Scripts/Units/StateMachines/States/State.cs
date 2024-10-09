using Unity.VisualScripting;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;
    protected MinionController _unitController;

    protected State(StateMachine stateMachine, MinionController unitController)
    {
        _stateMachine = stateMachine;
        _unitController = unitController;
    }

    public virtual void Entry()
    {
        _unitController.MB.StateInfo.text = GetType().Name;
        Debug.Log(GetType().Name);
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
        _unitController.MB.StateInfo.text = null;
    }
}