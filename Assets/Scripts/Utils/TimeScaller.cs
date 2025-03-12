using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaller : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Button _add;
    [SerializeField] private Button _remove;

    int step = 1;

    private void Awake()
    {
        _add.onClick.AddListener(delegate { ChangeScaleTime(_inputField.text, step); });
        _remove.onClick.AddListener(delegate { ChangeScaleTime(_inputField.text, -step); }); ;
        _inputField.onValueChanged.AddListener(delegate { ChangeScaleTime(_inputField.text); });
    }

    private void ChangeScaleTime(string target, int value = 0)
    {
        float tempTarget;
        if (float.TryParse(target, out tempTarget) && tempTarget + value >= 0)
        {
            Time.timeScale = tempTarget + value;
            _inputField.text = Time.timeScale.ToString();
        }
    }

}