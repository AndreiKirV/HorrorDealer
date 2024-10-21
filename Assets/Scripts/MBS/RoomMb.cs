using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class RoomMb : MonoBehaviour
{
    [SerializeField] private Setting _settings;

    [SerializeField] private Transform _wallsParent;
    [SerializeField] private Transform _floorParent;

    [Serializable]
    public class Setting
    {
        public Vector2 Size;
        public Vector2 OffsetFloor;
        public GameObject FloorPref;
        public GameObject InteriorWall;
        public GameObject ExteriorWall;
        public GameObject Door;
    }

    private SelectItem _selectElement;
    private List<GameObject> _interiorWalls = new List<GameObject>();
    private List<GameObject> _floors = new List<GameObject>();
    private List<GameObject> _exteriorWalls = new List<GameObject>();
    private List<GameObject> _doors = new List<GameObject>();

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

    private void Awake()
    {
        _selectElement?.Init(this);
    }

    [ContextMenu("Init")]
    public void Init()
    {
        _selectElement = this.AddComponent<SelectItem>();
        _selectElement.SetFetter(_selectElement.AddComponent<Fetter>());
        _selectElement.Fetter.Types.Add(FetterType.Room);
    }

    [ContextMenu("Create room")]
    public void CreateRoom()
    {
        ClearList(_floors);
        ClearList(_interiorWalls);
        ClearList(_exteriorWalls);
        ClearList(_doors);


        if (CheckPrefs())
        {
            if (_floorParent == null)
            {
                _floorParent = new GameObject("Floor").transform;
                _floorParent.parent = transform;
                _floorParent.localPosition = Vector3.zero;
            }

            if (_wallsParent == null)
            {
                _wallsParent = new GameObject("Walls").transform;
                _wallsParent.parent = transform;
                _wallsParent.localPosition = Vector3.zero;
            }

            GameObject tempDoor = Instantiate(_settings.Door, new Vector3(0, 0, 0), Quaternion.identity, _wallsParent);
            tempDoor.transform.localPosition = Vector3.zero;
            _doors.Add(tempDoor);


            for (int i = 0; i < _settings.Size.x; i++)
            {
                for (int e = 0; e < _settings.Size.y; e++)
                {
                    // Создаем пол
                    GameObject tempFloor = Instantiate(_settings.FloorPref, new Vector3(i, 0, e), Quaternion.identity, _floorParent);
                    tempFloor.transform.localPosition = new Vector3(i * _settings.OffsetFloor.x + _settings.OffsetFloor.x / 2, 0, e * _settings.OffsetFloor.y + _settings.OffsetFloor.y / 2);
                    _floors.Add(tempFloor);

                    // Создаем внешние стены по периметру комнаты

                    if (e == 0)
                    {
                        GameObject exteriorWall = Instantiate(_settings.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        GameObject interiorWall = Instantiate(_settings.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, -180, 0);
                        exteriorWall.transform.localPosition = new Vector3(i * _settings.OffsetFloor.x, 0, 0);

                        interiorWall.transform.localPosition = new Vector3(i * _settings.OffsetFloor.x + _settings.OffsetFloor.x, 0, 0);
                        interiorWall.transform.rotation = Quaternion.Euler(0, 0, 0);

                        _exteriorWalls.Add(exteriorWall);
                        _interiorWalls.Add(interiorWall);

                        exteriorWall = Instantiate(_settings.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        interiorWall = Instantiate(_settings.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, 0, 0);
                        exteriorWall.transform.localPosition = new Vector3(i * _settings.OffsetFloor.x + _settings.OffsetFloor.x, 0, _settings.Size.y * _settings.OffsetFloor.y);

                        interiorWall.transform.localPosition = new Vector3(i * _settings.OffsetFloor.x, 0, _settings.Size.y * _settings.OffsetFloor.y);
                        interiorWall.transform.rotation = Quaternion.Euler(0, -180, 0);

                        _exteriorWalls.Add(exteriorWall);
                        _interiorWalls.Add(interiorWall);
                    }

                    if (i == 0)
                    {
                        GameObject exteriorWall = Instantiate(_settings.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        GameObject interiorWall = Instantiate(_settings.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, -90, 0);
                        exteriorWall.transform.localPosition = new Vector3(0, 0, e * _settings.OffsetFloor.y + _settings.OffsetFloor.y);

                        interiorWall.transform.localPosition = new Vector3(0, 0, e * _settings.OffsetFloor.y);
                        interiorWall.transform.rotation = Quaternion.Euler(0, 90, 0);

                        _exteriorWalls.Add(exteriorWall);
                        _interiorWalls.Add(interiorWall);

                        exteriorWall = Instantiate(_settings.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        interiorWall = Instantiate(_settings.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, 90, 0);
                        exteriorWall.transform.localPosition = new Vector3(_settings.Size.x * _settings.OffsetFloor.x, 0, e * _settings.OffsetFloor.y);

                        interiorWall.transform.localPosition = new Vector3(_settings.Size.x * _settings.OffsetFloor.x, 0, e * _settings.OffsetFloor.y + _settings.OffsetFloor.y);
                        interiorWall.transform.rotation = Quaternion.Euler(0, -90, 0);

                        _exteriorWalls.Add(exteriorWall);
                        _interiorWalls.Add(interiorWall);
                    }
                }
            }
        }
    }

    private void ClearList(List<GameObject> list)
    {
        if (list != null && list.Count > 0)
            for (int i = 0; i < list.Count; i++)
                DestroyImmediate(list[i]);

        list.Clear();
    }

    private bool CheckPrefs()
    {
        if (_settings.FloorPref && _settings.InteriorWall && _settings.ExteriorWall)
            return true;
        else
            return false;
    }
}