using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public WorldMB WorldMB;
    public CameraController CameraController;

    public Selecter Selecter;

    public static GameStarter Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        if (WorldMB.CameraController == null)
        {
            WorldMB.CameraController = Instantiate(CameraController, WorldMB.transform);
        }
    }

    private void Update()
    {

    }
}