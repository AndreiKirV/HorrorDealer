using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FreeNPCState : State
{
    public FreeNPCState(StateMachine stateMachine, UnitController unitController) : base(stateMachine, unitController)
    {
    }

    public override void Entry(StateInfo cfg)
    {
        base.Entry(cfg);
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
}