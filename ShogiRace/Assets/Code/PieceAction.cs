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
    //[SerializeField] private bool m_canTakeoff;
    [SerializeField] private float m_pushTime;   // Space key press duration
    [SerializeField] private float m_currentSpeed;
    [SerializeField] private float m_currentGroundGradient;
    [SerializeField] private float m_ownInclination;
    [SerializeField] private bool m_isGround;
    private Vector2 m_inputVector;
    [SerializeField]private float m_fallingVectorLimit;

    [Header("==== Velocity ====")]
    [SerializeField] private Vector3 m_velocity;
    [SerializeField] private Vector3 m_moveForwardVector;
    [SerializeField] private Vector3 m_fallingVector;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Initialize(PieceController pieceController, PieceSpec spec)
    {
        m_pieceController = pieceController;
        m_spec = spec;
        isStopped = true;
        m_fallingVectorLimit = m_pieceController.Gravity * (m_spec.Weight * 0.1f);
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
        m_velocity = m_moveForwardVector + m_fallingVector;
        SetVelocity(m_velocity);
    }

    private void AddMoveForward()
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

        m_moveForwardVector = (transform.forward * m_currentSpeed * Time.deltaTime);
    }

    public void UseVelocity()
    {
        AddMoveForward();
        AddGravity();
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

    public void Turning()
    {
        float value = m_inputVector.x * m_spec.Turning * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, value, 0);
        //Debug.Log($"Turning], value: {value}");
    }

    private void RestoreTilt()
    {

    }

    private void AddGravity()
    {
        if (m_isGround)
        {
            m_fallingVector = Vector3.zero;
            return;
        }

        if (m_fallingVector.y <= -m_fallingVectorLimit)
        {
            m_fallingVector.y = -m_fallingVectorLimit;
            return;
        }
        m_fallingVector += new Vector3(0, -m_pieceController.Gravity * (1 + m_spec.Weight * 0.01f) * Time.deltaTime, 0);
    }

    private void CalculateGradient()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Vector3 closestPoint = m_col.ClosestPoint(transform.position);
    //    Debug.Log($"[OCEnter] closestPoint: {closestPoint}");
    //    if (closestPoint.y <= transform.position.y)
    //    {
    //        m_isGround = true;
    //    }
    //    //if (collision.gameObject.tag == "Ground")
    //    //{
    //    //    m_isGround = true;
    //    //}
    //}

    private void OnCollisionExit(Collision collision)
    {
        //Vector3 closestPoint = m_col.ClosestPoint(transform.position);
        //Debug.Log($"[OCExit] closestPoint: {closestPoint}");
        //if (closestPoint.y <= transform.position.y)
        //{
        //    m_isGround = false;
        //}
        if (collision.gameObject.tag == "Ground")
        {
            m_isGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_isGround = true;
        }
    }

}
