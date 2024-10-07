using System;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    public Action SelectAction;
    public Action DeselectAction;

    public bool IsSelected => _isSelected;

    private bool _isSelected = false;
    private InputSettings _input;

    private MonoBehaviour _mb;

    public void Init(MonoBehaviour mb)
    {
        _mb = mb;
    }

    private void Awake()
    {
        _input = new InputSettings();
        _input.Enable();
    }

    private void OnMouseDown()
    {
        Selected();
    }

    private void Deselect(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _isSelected = false;
        _input.Mouse.LB.started -= Deselect;

        GameStarter.Instance.Selecter.ResetSelected(_mb);
        
        DeselectAction?.Invoke();
    }

    public void Selected()
    {
        _isSelected = true;
        _input.Mouse.LB.started += Deselect;
        _input.Enable();

        GameStarter.Instance.Selecter.SetSelected(_mb);

        SelectAction?.Invoke();
    }
}