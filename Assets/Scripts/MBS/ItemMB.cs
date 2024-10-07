using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMB : MonoBehaviour
{
    public Outline Outline;
    public SelectItem _selectElement;

    private void Awake() 
    {
        if(!TryGetComponent<SelectItem>(out _selectElement))
        _selectElement = gameObject.AddComponent<SelectItem>();

        _selectElement.Init(this);

        _selectElement.SelectAction += Select;
        _selectElement.DeselectAction += Deselect;
    }

    public void Select()
    {
        Debug.Log("Жмакнули");
        Outline.enabled = !Outline.enabled;
    }

    public void Deselect()
    {
        Debug.Log("сняли");
        Outline.enabled = !Outline.enabled;
    }
}