using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookerToCamera : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}