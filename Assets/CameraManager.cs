using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Vector3 LeftCameraPosition;
    public Vector3 MiddleCameraPosition;
    public Vector3 RightCameraPosition;
    public int PanelNumber;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

