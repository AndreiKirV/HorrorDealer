using UnityEngine;

public class Selecter : MonoBehaviour
{
    public UiController UiController;
    public GameObject PointPref;

    public MonoBehaviour SeclectedItem => _selectedItem;

    private MonoBehaviour _selectedItem;
    private GameObject _point;

    public void SetSelected(MonoBehaviour selectedItem)
    {
        _selectedItem = selectedItem;
        UiController.SetTextInfo(selectedItem.gameObject.name);

        if(!_point)
        _point = Instantiate(PointPref);

        _point.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_selectedItem)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _point.transform.position = hit.point;
            }
        }
    }

    public void ResetSelected(MonoBehaviour selectedItem)
    {
        if (selectedItem != _selectedItem)
            return;

        _selectedItem = null;
        UiController.SetTextInfo(null);
        _point.gameObject.SetActive(false);
    }
}