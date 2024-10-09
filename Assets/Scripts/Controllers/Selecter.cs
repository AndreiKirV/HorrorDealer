using System;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    [SerializeField]
    private Setting _settings;

    [Serializable]
    public class Setting
    {
        public UiController UiController;
        public GameObject PointPref;
        public LayerMask LayerMask;
    }

    public SelectItem SeclectedItem => _selectedItem;
    //public MonoBehaviour _previousSelectedItem;
    private SelectItem _selectedItem;
    //private GameObject _point;
    private InputSettings _input;
    private GameObject _point;

    private void Awake()
    {
        _input = new InputSettings();
        _input.Enable();
        _input.Mouse.LB.started += TrySelected;
    }

    private void Start()
    {
        _point = Instantiate(_settings.PointPref);
        _point.SetActive(false);
    }

    private void Update()
    {
    }

    //TODO кайфуй - ковыряйся
    public void TrySelected(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //Debug.Log("TrySelected LB");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, _settings.LayerMask);

        if (hits.Length > 1)
        {
            //Debug.Log($"первый {hits[0].collider.gameObject.name} последний {hits[hits.Length - 1].collider.gameObject.name} из {hits.Length}");

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.TryGetComponent<SelectItem>(out SelectItem item))
                {
                    _settings.UiController.SetTextInfo(item.gameObject.name);
                    Select(item);
                    return;
                }
            }
        }
        else if (_selectedItem)
        {
            MinionController temp = _selectedItem.MB as MinionController;

            if(!temp || !temp.IsControlled)
            Deselect();
        }
    }

    private void Select(SelectItem item)
    {
        if (_selectedItem)
        {
            _selectedItem.Deselected();

            MinionController tempMinion = _selectedItem.MB as MinionController;
            ItemMB tempItem = item.MB as ItemMB;

            if(tempMinion && tempItem && !tempMinion.IsControlled)
            tempMinion.TrySetFetter(tempItem);

            Debug.Log($"выбран {_selectedItem.MB.GetType()} пытаетесь выбрать {item.MB.GetType()}");
        }

        _selectedItem = item;
        _selectedItem.Selected();
    }

    private void Deselect()
    {
        _selectedItem?.Deselected();
        _selectedItem = null;
        _settings.UiController.SetTextInfo(null);
    }
}