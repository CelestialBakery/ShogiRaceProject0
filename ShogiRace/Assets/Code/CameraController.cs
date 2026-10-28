using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private RacerSaveManager m_racerSaveManager;
    [SerializeField] private Transform m_target;

    [Header("Setting")]
    [SerializeField] private Vector3 m_targetOffset;
    [SerializeField] private float m_cameraDistance;
    [SerializeField] private float m_cameraHeight;
    [SerializeField] private float m_cameraPlayDistance;
    [SerializeField] private float m_followSpeed;
    [SerializeField] private float m_leaveSpeed;
    [SerializeField] private float m_lookPlayDistance;

    [Header("Confirm")]
    [SerializeField] private Vector3 m_cameraBaseLocalPosition;
    [SerializeField] private Vector3 m_behindOfTargetOnXZ;
    [SerializeField] private Vector3 m_lookPosition;

    [Header("Debug")]
    [SerializeField] private Vector3 _behind;

    // Cache
    private Vector3 m_cameraPosition;
    private Vector3 m_targetPosition;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        m_racerSaveManager = GameObject.Find("RacerSaveManager").GetComponent<RacerSaveManager>();
    }

    private void Start()
    {

        Invoke(nameof(SetTarget), 0.016f);
    }

    void Update()
    {
        if (m_target == null) return;

        UpdateCache();
        //UpdateLookPosition();
        UpdateCameraPosition();

        transform.LookAt(m_targetPosition);
    }

    private void SetTarget()
    {
        m_target = m_racerSaveManager.PieceRacers[0]?.transform;
    }

    private void UpdateCache()
    {
        m_cameraPosition = transform.position;
        m_targetPosition = m_target.position + m_targetOffset;
    }


    private void UpdateLookPosition()
    {
        Vector3 vector = m_targetPosition - m_cameraPosition - m_lookPosition;
        float distance = vector.magnitude;

        if (distance > m_lookPlayDistance)
        {
            float moveDistance = (distance - m_lookPlayDistance) * (Time.deltaTime * m_followSpeed);
            m_lookPosition += vector.normalized * moveDistance;
        }
        //m_transform.rotation = Quaternion.LookRotation(m_direction);
    }

    private void UpdateCameraPosition()
    {
        Vector3 horizontalVector = m_targetPosition - m_cameraPosition;
        horizontalVector.y = 0;
        float distance = horizontalVector.magnitude;

        float moveDistance = 0;
        if (distance > m_cameraDistance + m_cameraPlayDistance)
        {
            moveDistance = distance - (m_cameraDistance + m_cameraPlayDistance);
            moveDistance *= Time.deltaTime * m_followSpeed;
        }
        else if (distance < m_cameraDistance + m_cameraPlayDistance)
        {
            moveDistance = distance - (m_cameraDistance - m_cameraPlayDistance);
            moveDistance *= Time.deltaTime * m_leaveSpeed;
        }

        //UpdateBasePosition();
        UpdateBehindOfTarget();
        UpdateBaseLocalPosition();

        transform.position = m_targetPosition + m_cameraBaseLocalPosition;
        //Vector3 cameraPosition = m_cameraPosition + m_behindOfTargetXZ * moveDistance;
        //cameraPosition.y = m_lookPosition.y + m_cameraHeight;
        //transform.position = cameraPosition;
        //m_currentCameraBasePosition += (horizontalVector * moveDistance);

        //transform.position = m_currentCameraBasePosition;
    }

    private void UpdateBaseLocalPosition()
    {
        float distanceXZ = Mathf.Sqrt(m_cameraDistance * m_cameraDistance - m_cameraHeight * m_cameraHeight);
        m_cameraBaseLocalPosition = m_behindOfTargetOnXZ * distanceXZ;
        m_cameraBaseLocalPosition.y = m_cameraHeight;
    }

    // Get behind of target horizontal position.
    private void UpdateBehindOfTarget()
    {
        Vector3 behind = -m_target.forward;
        if (behind == Vector3.zero)
        {
            Vector3 axis = m_target.eulerAngles;
            behind = new Vector3(-Mathf.Sin(axis.y), 0, -Mathf.Cos(axis.y));
        }
        _behind = behind;
        Vector2 horizontalVector = new Vector2(behind.x, behind.z).normalized;
        m_behindOfTargetOnXZ = new Vector3(horizontalVector.x, 0, horizontalVector.y);
    }
}
