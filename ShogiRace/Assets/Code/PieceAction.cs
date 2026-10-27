using UnityEngine;

public class PieceAction : MonoBehaviour
{
    private PieceController m_pieceController;

    public bool isStopped;

    private PieceSpec m_spec;

    [Header("==== Component ====")]
    [SerializeField] private Collider m_col;
    [SerializeField] private Rigidbody m_rb;

    [Header("==== Current state ====")]
    [SerializeField] private bool m_isPushed;
    [SerializeField] private float m_pushTime;   // Space key press duration
    [SerializeField] private float m_currentSpeed;
    private Vector2 m_inputVector;

    [Header("==== Velocity ====")]
    [SerializeField] private Vector3 m_velocity;
    [SerializeField] private Vector3 m_moveForwardVector;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Initialize(PieceController pieceController, PieceSpec spec)
    {
        m_pieceController = pieceController;
        m_spec = spec;
        isStopped = true;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void AddVelocity(Vector3 addVel)
    {
        //transform.position += addVel;
        //m_rb.AddForce(addVel);
        //m_rb.linearVelocity = addVel;
        m_velocity += addVel;
    }

    private void SetVelocity(Vector3 setVel)
    {
        m_rb.linearVelocity = setVel;
    }

    public void SumVelocity()
    {
        m_velocity = m_moveForwardVector;
        SetVelocity(m_velocity);
    }

    public void AddMoveForward()
    {
        if (isStopped) return;

        if (m_currentSpeed < m_spec.TopSpeed)
        {
            m_currentSpeed += m_spec.Acceleration * Time.deltaTime;
            if (m_currentSpeed > m_spec.TopSpeed)
            {
                m_currentSpeed = m_spec.TopSpeed;
            }
        }

        m_moveForwardVector = (-transform.forward * m_currentSpeed * Time.deltaTime);
    }

    // Common operation
    public void TiltUpward()
    {

    }

    public void TiltDownward()
    {

    }

    // Attack
    public void Trample()
    {

    }

    public void SendInputVector(Vector2 inputVector)
    {
        m_inputVector = inputVector;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 closestPoint = m_col.ClosestPoint(transform.position);
        Debug.Log($"[OCE] closestPoint: {closestPoint}");
    }
}
