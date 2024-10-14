using System.Collections.Generic;
using UnityEngine;

public class ItemMB : MonoBehaviour
{
    public Fetter Fetter;

    public Outline Outline;
    public SelectItem _selectElement;

    private void Awake()
    {
        if (!TryGetComponent<SelectItem>(out _selectElement))
            _selectElement = gameObject.AddComponent<SelectItem>();

        _selectElement.Init(this);

        _selectElement.SelectAction += Select;
        _selectElement.DeselectAction += Deselect;
    }

    private void Start()
    {
        GameStarter.Instance.Settings.Traceables.Items.Add(this);
    }

    private void OnDestroy()
    {
        GameStarter.Instance.Settings.Traceables.Items.Remove(this);
    }

    public void Select()
    {
        Outline.enabled = !Outline.enabled;
    }

    public void Deselect()
    {
        Outline.enabled = !Outline.enabled;
    }
}