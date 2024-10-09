using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    [SerializeField]
    private Setting _settings;

    public Setting Settings => _settings;
    
    //private static GameStarter _instance;

    [Serializable]
    public class Setting
    {
        public Ref Refs;
        public Fillable Fillables;
        public Traceable Traceables;

        [Serializable]
        public class Fillable
        {

        }

        [Serializable]
        public class Ref
        {
            public WorldMB WorldMB;
            public CameraController CameraController;
            public Selecter Selecter;
        }

        [Serializable]
        public class Traceable
        {
            public List<UnitController> Units = new List<UnitController>();
            public List<ItemMB> Items = new List<ItemMB>();
        }
    }

    /* public static GameStarter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameStarter").AddComponent<GameStarter>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    } */

    public static GameStarter Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        if (_settings.Refs.WorldMB.CameraController == null)
        {
            _settings.Refs.WorldMB.CameraController = Instantiate(_settings.Refs.CameraController, _settings.Refs.WorldMB.transform);
        }
    }

    private void Update()
    {

    }
}