using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private RacerSaveManager m_racerSaveManager;
    [SerializeField] private Transform m_player;

    [Header("Setting")]
    [SerializeField] private float m_cameraDistance;
    [SerializeField] private float m_cameraHeight;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
    }

}
