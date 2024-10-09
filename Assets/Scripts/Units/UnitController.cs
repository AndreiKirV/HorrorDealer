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

    protected virtual void Start()
    {
        GameStarter.Instance.Settings.Traceables.Units.Add(this);
    }

    protected virtual void OnDestroy()
    {
        GameStarter.Instance.Settings.Traceables.Units.Remove(this);
    }

    protected virtual void OnDisable()
    {
        GameStarter.Instance.Settings.Traceables.Units.Remove(this);
    }

    protected virtual void OnEnable()
    {

    }
}