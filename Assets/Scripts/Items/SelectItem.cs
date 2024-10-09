using System;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    public Action SelectAction;
    public Action DeselectAction;

    public bool IsSelected => _isSelected;
    public MonoBehaviour MB => _mb;

    private bool _isSelected = false;
    private MonoBehaviour _mb;

    public void Init(MonoBehaviour mb)
    {
        _mb = mb;
    }

    public void Deselected()
    {
        _isSelected = false;

        DeselectAction?.Invoke();
    }

    public void Selected()
    {
        _isSelected = true;

        SelectAction?.Invoke();
    }
}