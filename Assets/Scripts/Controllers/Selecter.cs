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

    private SelectItem _selectedItem;
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

    public void TrySelected(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, _settings.LayerMask);
        //TODO получать по компонентам
        Debug.Log($"TrySelected LB {hits.Length}");

        if (hits.Length >= 1)//TODO здесь должен быть тоже деселект
        {
            Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

            foreach (RaycastHit hit in hits)
            {
                //Debug.Log($"{hit.collider.gameObject.name} ");

                if (hit.collider.TryGetComponent<SelectItem>(out SelectItem item))// && item.MB.GetType() != typeof(RoomMb) ^ _selectedItem.MB.GetType() == typeof(MinionController) && _selectedItem.GetComponent<MinionController>().IsControlled)//^
                {
                    _settings.UiController.SetTextInfo(item.gameObject.name);

                   //_selectedItem.TryGetComponent<MinionController>(out MinionController tempMinion);

                    //if(!minionController || minionController != null && !minionController.IsControlled || item.MB.GetType() != typeof(RoomMb))
                    Select(item);

                    _point.transform.position = hit.point;
                    _point.SetActive(true);
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
            Fetter tempItem = item.Fetter;

            if(tempMinion && tempItem && !tempMinion.IsControlled && tempItem)
            tempMinion.TrySetFetter(tempItem);
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