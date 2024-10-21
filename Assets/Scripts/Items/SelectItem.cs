using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Fetter _fetter;

    public UnityEvent SelectEvent;
    public UnityEvent DeselectEvent;

    public Action SelectAction;
    public Action DeselectAction;

    public bool IsSelected => _isSelected;
    public MonoBehaviour MB => _mb;
    public Fetter Fetter => _fetter;

    private bool _isSelected = false;
    private MonoBehaviour _mb;

    public void Init(MonoBehaviour mb)
    {
        _mb = mb;
    }

    public void Deselected()
    {
        _isSelected = false;

        DeselectEvent?.Invoke();
        DeselectAction?.Invoke();
    }

    public void Selected()
    {
        _isSelected = true;

        SelectEvent?.Invoke();
        SelectAction?.Invoke();
    }

    public void SetFetter(Fetter fetter)
    {
        _fetter = fetter;
    }
}