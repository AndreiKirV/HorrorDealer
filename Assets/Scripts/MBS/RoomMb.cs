using UnityEngine;

public class RoomMb : MonoBehaviour
{
    public SelectItem _selectElement;
    
    private void Start()
    {
        GameStarter.Instance.Settings.Traceables.Rooms.Add(this);
    }

    private void OnDestroy()
    {
        GameStarter.Instance.Settings.Traceables.Rooms.Remove(this);
    }

    private void Awake()
    {
        _selectElement.Init(this);
    }
}