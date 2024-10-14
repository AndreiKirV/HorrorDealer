using UnityEngine;

public class TetheredNPCState : State
{
    public TetheredNPCState(StateMachine stateMachine, MinionController unitController) : base(stateMachine, unitController)
    {
    }

    public override void Entry()
    {
        base.Entry();
        _unitController.MB.Agent.enabled = false;

        if (_unitController.CurrentFetter.TPTransform)
            _unitController.transform.position = _unitController.CurrentFetter.TPTransform.position;
        else
            _unitController.transform.position = _unitController.CurrentFetter.transform.position;

        _unitController.MB.Animator.Play("Emergence");//TODO словарь для анимашек
        _unitController.MB.Agent.enabled = true;
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
}