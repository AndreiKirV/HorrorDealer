using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomCFG
{
    public Vector2 Size;
    public Vector2 OffsetFloor;

    [Space(10)]
    [Header("prefs")]
    public GameObject FloorPref;
    public GameObject InteriorWall;
    public GameObject ExteriorWall;
    public GameObject Door;
    public OutlineViewRoomMB OutlinePref;

    [Space(10)]
    [Header("refs")]
    public List<GameObject> InteriorWalls = new List<GameObject>();
    public List<GameObject> Floors = new List<GameObject>();
    public List<GameObject> ExteriorWalls = new List<GameObject>();
    public List<GameObject> Doors = new List<GameObject>();
    public OutlineViewRoomMB Outline;
}