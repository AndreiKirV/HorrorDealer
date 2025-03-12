using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class RoomMb : MonoBehaviour
{
    public RoomCFG CFG;
    public SelectItem SelectElement;

    private void Start()
    {
        if (!GameStarter.Instance.Settings.Traceables.Rooms.Contains(this))
            GameStarter.Instance.Settings.Traceables.Rooms.Add(this);
    }

    private void OnDestroy()
    {
        if (GameStarter.Instance.Settings.Traceables.Rooms.Contains(this))
            GameStarter.Instance.Settings.Traceables.Rooms.Remove(this);
    }
}