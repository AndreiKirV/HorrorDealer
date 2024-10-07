using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text Info;

    public void SetTextInfo(string text)
    {
        Info.text = text;
    }
}