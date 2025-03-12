using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OutlineViewRoomMB : MonoBehaviour
{
    public List<GameObject> _angles;
    public List<GameObject> _lines;

    public float LengthX;
    public float LengthZ;
    public float Width = 0.18f;
    public float Height = 0.1f;

    //[ContextMenu("Init")]
    public void Update()
    {
        if (_angles != null && _angles.Count > 3 && _lines != null && _lines.Count > 3)
        {
            _lines[0].transform.localScale = new Vector3(Width, LengthX, Height);
            _lines[0].transform.localPosition = new Vector3(LengthX, _lines[0].transform.localPosition.y, 0);
            _lines[0].transform.localRotation = Quaternion.Euler(90, 90, 0);

            _lines[1].transform.localScale = new Vector3(Width, LengthX, Height);
            _lines[1].transform.localPosition = new Vector3(LengthX, _lines[0].transform.localPosition.y, LengthZ * 2);
            _lines[1].transform.localRotation = Quaternion.Euler(90, 90, 0);

            _lines[2].transform.localScale = new Vector3(Width, LengthZ, Height);
            _lines[2].transform.localPosition = new Vector3(0, _lines[0].transform.localPosition.y, LengthZ);
            _lines[2].transform.localRotation = Quaternion.Euler(90, 0, 0);

            _lines[3].transform.localScale = new Vector3(Width, LengthZ, Height);
            _lines[3].transform.localPosition = new Vector3(LengthX * 2, _lines[0].transform.localPosition.y, LengthZ);
            _lines[3].transform.localRotation = Quaternion.Euler(90, 0, 0);

            _angles[0].transform.localScale = new Vector3(Width, Height, Width);
            _angles[0].transform.localPosition = new Vector3(0, _angles[0].transform.localPosition.y, 0);

            _angles[1].transform.localScale = new Vector3(Width, Height, Width);
            _angles[1].transform.localPosition = new Vector3(LengthX * 2, _angles[0].transform.localPosition.y, 0);

            _angles[2].transform.localScale = new Vector3(Width, Height, Width);
            _angles[2].transform.localPosition = new Vector3(0, _angles[0].transform.localPosition.y, LengthZ * 2);

            _angles[3].transform.localScale = new Vector3(Width, Height, Width);
            _angles[3].transform.localPosition = new Vector3(LengthX * 2, _angles[0].transform.localPosition.y, LengthZ * 2);
        }
        else
        {
            Debug.Log("Не все ссылки");
        }
    }
}