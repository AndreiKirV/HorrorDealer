using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : UnitController
{
    private InputSettings _input;
    private StateMachine _stateMachine = new StateMachine();

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Start()
    {
        _stateMachine.SetState("FreeNPCState");
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void Select()
    {
        Debug.Log("Жмакнули");
        MB.Outline.enabled = !MB.Outline.enabled;
        
        _input.Mouse.RB.canceled += GoMove;
    }

    public void Deselect()
    {
        Debug.Log("сняли");
        MB.Outline.enabled = !MB.Outline.enabled;
        _input.Mouse.RB.canceled -= GoMove;
    }

    private void GoMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("поехал");
        /* if (!_selectElement.IsSelected)
            return; */

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MB.Agent.SetDestination(hit.point);
        }
    }

    private void Init()
    {
        Debug.Log("Init");
        _input = new InputSettings();
        _input.Enable();

        _selectElement.SelectAction += Select;
        _selectElement.DeselectAction += Deselect;

        _stateMachine.RemoveStates();
        _stateMachine.AddState("FreeNPCState", new FreeNPCState(_stateMachine, this));

    }
}