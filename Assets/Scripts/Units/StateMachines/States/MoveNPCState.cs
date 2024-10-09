using UnityEngine;

public class MoveNPCState : State
{
    public MoveNPCState(StateMachine stateMachine, MinionController unitController) : base(stateMachine, unitController)
    {
    }

    public override void Entry()
    {
        base.Entry();

        if (_unitController.IsControlled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _unitController.MB.Agent.SetDestination(hit.point);
            }
        }
    }

    public override void Update()
    {
        if (_unitController.MB.Agent.pathPending)
            return;

        if (_unitController.MB.Agent.remainingDistance <= _unitController.MB.Agent.stoppingDistance)
        {
            _stateMachine.ResetStateToStart();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}