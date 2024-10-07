using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitMB MB;
    protected SelectItem _selectElement;

    protected virtual void Awake()
    {
        if (!TryGetComponent<SelectItem>(out _selectElement))
            _selectElement = gameObject.AddComponent<SelectItem>();

        _selectElement.Init(this);
    }
}