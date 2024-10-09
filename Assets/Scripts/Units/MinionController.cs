using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionController : UnitController
{
    public List<Fetter> Fetters = new List<Fetter>();

    [SerializeField]
    private bool _isControlled = false;

    public bool IsControlled => _isControlled;

    private InputSettings _input;
    private StateMachine _stateMachine = new StateMachine();
    public ItemMB _currentFetter;

    bool _isStart = false;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _selectElement.SelectAction -= Select;
        _selectElement.DeselectAction -= Deselect;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _selectElement.SelectAction += Select;
        _selectElement.DeselectAction += Deselect;

        _stateMachine.SetState(StateDictionary.FreeNPCState);
    }

    public void TrySetFetter(ItemMB targetFetter)
    {
        if (targetFetter.Fetters.Any(item =>  Fetters.Contains(item)))
        {
            _currentFetter?.RemoveMinion(this);
            _currentFetter = targetFetter;
            _currentFetter.AddMinion(this);
            Debug.Log("привязал");
        }
        else
        Debug.Log("не совпало");
    }

    public void Select()
    {
        MB.Outline.enabled = !MB.Outline.enabled;
        _input.Mouse.LB.canceled += GoMove;
    }

    public void Deselect()
    {
        MB.Outline.enabled = !MB.Outline.enabled;
        _input.Mouse.LB.canceled -= GoMove;
        _isStart = false;
    }

    private void GoMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_isStart)
            _stateMachine.SetState(StateDictionary.MoveNPCState);
        else
            _isStart = true;
    }

    private void Init()
    {
        _input = new InputSettings();
        _input.Enable();

        _stateMachine.RemoveStates();
        _stateMachine.AddState(StateDictionary.FreeNPCState, new FreeNPCState(_stateMachine, this));
        _stateMachine.AddState(StateDictionary.MoveNPCState, new MoveNPCState(_stateMachine, this));
    }
}