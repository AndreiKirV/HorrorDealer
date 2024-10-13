using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leveler : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;
    [SerializeField] private List<LayerMask> _levels = new List<LayerMask>();

    private int _currentLevel = 1;

    private void Awake()
    {
        _up.onClick.AddListener(delegate { ChangeLayer(1); });
        _down.onClick.AddListener(delegate { ChangeLayer(-1); });
    }

    private void ChangeLayer(int target)
    {
        if (_currentLevel + target >= 0 && _currentLevel + target < _levels.Count)
        {
            _currentLevel = _currentLevel + target;
            Camera.main.cullingMask = _levels[_currentLevel];
        }
    }
}