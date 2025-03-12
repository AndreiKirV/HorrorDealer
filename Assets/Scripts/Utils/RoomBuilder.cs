using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private RoomCFG _cfg;

    [SerializeField] private Transform _wallsParent;
    [SerializeField] private Transform _floorParent;

    private SelectItem _selectElement;
    private RoomMb _room;
    private List<BoxCollider> _coliders = new List<BoxCollider>();

    [ContextMenu("Init")]
    public void Init()
    {
        if (!_room && !TryGetComponent<RoomMb>(out RoomMb room))
        {
            _room = this.AddComponent<RoomMb>();
            _room.CFG = _cfg;
        }

        if (!_selectElement && !TryGetComponent<SelectItem>(out SelectItem selectElement))
        {
            _selectElement = this.AddComponent<SelectItem>();
            _room.SelectElement = _selectElement;
            _selectElement?.Init(_room);
            _selectElement.SetFetter(_selectElement.AddComponent<Fetter>());
            _selectElement.Fetter.Types.Add(FetterType.Room);
        }
    }

    [ContextMenu("Create room")]
    public void CreateRoom()
    {
        ClearList(_cfg.Floors);
        ClearList(_cfg.InteriorWalls);
        ClearList(_cfg.ExteriorWalls);
        ClearList(_cfg.Doors);

        if(_coliders.Count < 0)
        _coliders.AddRange(GetComponents<BoxCollider>());

        ClearList(_coliders);

        if (_cfg.Outline != null)
            DestroyImmediate(_cfg.Outline.gameObject);

        if(!_room)
        _room = GetComponent<RoomMb>();

        _room.CFG = _cfg;

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

            GameObject tempDoor = Instantiate(_cfg.Door, Vector3.zero, Quaternion.identity, _wallsParent);
            tempDoor.transform.localPosition = new Vector3(0, 0, -0.3f);
            _cfg.Doors.Add(tempDoor);

            OutlineViewRoomMB outlineView = Instantiate(_cfg.OutlinePref, Vector3.zero, Quaternion.identity, this.transform);
            outlineView.transform.localPosition = Vector3.zero;
            _cfg.Outline = outlineView;
            _cfg.Outline.LengthX = _cfg.OffsetFloor.x * _cfg.Size.x / 2;
            _cfg.Outline.LengthZ = _cfg.OffsetFloor.y * _cfg.Size.y / 2;

            _coliders.Add(this.AddComponent<BoxCollider>());
            _coliders[0].size = new Vector3(_cfg.OffsetFloor.x * _cfg.Size.x, 0.1f, _cfg.OffsetFloor.y * _cfg.Size.y);
            _coliders[0].center = new Vector3(_cfg.OffsetFloor.x * _cfg.Size.x / 2, 0, _cfg.OffsetFloor.y * _cfg.Size.y / 2);
            _coliders[0].isTrigger = true;
            //TODO сделать 5 коллайдеров на пол и стены

            for (int i = 0; i < _cfg.Size.x; i++)
            {
                for (int e = 0; e < _cfg.Size.y; e++)
                {
                    // Создаем пол
                    GameObject tempFloor = Instantiate(_cfg.FloorPref, new Vector3(i, 0, e), Quaternion.identity, _floorParent);
                    tempFloor.transform.localPosition = new Vector3(i * _cfg.OffsetFloor.x + _cfg.OffsetFloor.x / 2, 0, e * _cfg.OffsetFloor.y + _cfg.OffsetFloor.y / 2);
                    _cfg.Floors.Add(tempFloor);

                    // Создаем стены
                    if (e == 0)
                    {
                        GameObject exteriorWall = Instantiate(_cfg.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        GameObject interiorWall = Instantiate(_cfg.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, -180, 0);
                        exteriorWall.transform.localPosition = new Vector3(i * _cfg.OffsetFloor.x, 0, 0);

                        interiorWall.transform.localPosition = new Vector3(i * _cfg.OffsetFloor.x + _cfg.OffsetFloor.x, 0, 0);
                        interiorWall.transform.rotation = Quaternion.Euler(0, 0, 0);

                        _cfg.ExteriorWalls.Add(exteriorWall);
                        _cfg.InteriorWalls.Add(interiorWall);

                        exteriorWall = Instantiate(_cfg.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        interiorWall = Instantiate(_cfg.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, 0, 0);
                        exteriorWall.transform.localPosition = new Vector3(i * _cfg.OffsetFloor.x + _cfg.OffsetFloor.x, 0, _cfg.Size.y * _cfg.OffsetFloor.y);

                        interiorWall.transform.localPosition = new Vector3(i * _cfg.OffsetFloor.x, 0, _cfg.Size.y * _cfg.OffsetFloor.y);
                        interiorWall.transform.rotation = Quaternion.Euler(0, -180, 0);

                        _cfg.ExteriorWalls.Add(exteriorWall);
                        _cfg.InteriorWalls.Add(interiorWall);
                    }

                    if (i == 0)
                    {
                        GameObject exteriorWall = Instantiate(_cfg.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        GameObject interiorWall = Instantiate(_cfg.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, -90, 0);
                        exteriorWall.transform.localPosition = new Vector3(0, 0, e * _cfg.OffsetFloor.y + _cfg.OffsetFloor.y);

                        interiorWall.transform.localPosition = new Vector3(0, 0, e * _cfg.OffsetFloor.y);
                        interiorWall.transform.rotation = Quaternion.Euler(0, 90, 0);

                        _cfg.ExteriorWalls.Add(exteriorWall);
                        _cfg.InteriorWalls.Add(interiorWall);

                        exteriorWall = Instantiate(_cfg.ExteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);
                        interiorWall = Instantiate(_cfg.InteriorWall, Vector3.zero, Quaternion.identity, _wallsParent);

                        exteriorWall.transform.rotation = Quaternion.Euler(0, 90, 0);
                        exteriorWall.transform.localPosition = new Vector3(_cfg.Size.x * _cfg.OffsetFloor.x, 0, e * _cfg.OffsetFloor.y);

                        interiorWall.transform.localPosition = new Vector3(_cfg.Size.x * _cfg.OffsetFloor.x, 0, e * _cfg.OffsetFloor.y + _cfg.OffsetFloor.y);
                        interiorWall.transform.rotation = Quaternion.Euler(0, -90, 0);

                        _cfg.ExteriorWalls.Add(exteriorWall);
                        _cfg.InteriorWalls.Add(interiorWall);
                    }
                }
            }
        }
    }

    private void ClearList<T>(List<T> list) where T : Object
    {
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    DestroyImmediate(list[i]);
                }
            }
        }
        
        list.Clear();
    }

    private bool CheckPrefs()
    {
        if (_cfg.FloorPref && _cfg.InteriorWall && _cfg.ExteriorWall)
            return true;
        else
            return false;
    }
}