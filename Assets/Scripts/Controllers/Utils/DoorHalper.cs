using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHalper : MonoBehaviour
{
    [ContextMenu("DestroyWalls")]
    public void DestroyWalls()
    {
        DestroyImmediate();
    }
}