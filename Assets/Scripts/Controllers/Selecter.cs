using UnityEngine;

public class Selecter : MonoBehaviour
{
    public UiController UiController;

    public MonoBehaviour SeclectedItem => _selectedItem;

    private MonoBehaviour _selectedItem;

    public void SetSelected(MonoBehaviour selectedItem)
    {
        _selectedItem = selectedItem;
        UiController.SetTextInfo(selectedItem.gameObject.name);
    }

    public void ResetSelected(MonoBehaviour selectedItem)
    {
        if(selectedItem != _selectedItem)
        return;

        _selectedItem = null;
        UiController.SetTextInfo(null);
    }
}