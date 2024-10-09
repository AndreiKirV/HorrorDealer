using System.Collections.Generic;
using UnityEngine;

public class ItemMB : MonoBehaviour
{
    public List<Fetter> Fetters = new List<Fetter>();
    
    public Outline Outline;
    public SelectItem _selectElement;

    public List <MinionController> _currentMinions = new List<MinionController>();

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

    public void AddMinion(MinionController minion)
    {
        if(!_currentMinions.Contains(minion))
        _currentMinions.Add(minion);
    }

    public void RemoveMinion(MinionController minion)
    {
        _currentMinions.Remove(minion);
    }
}