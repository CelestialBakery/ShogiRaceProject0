using JetBrains.Annotations;
using UnityEngine;

public class PieceAction : MonoBehaviour
{
    private PieceController m_pieceController;
    [SerializeField] private InputKey m_key;

    public bool isStopped;

    private PieceSpec m_spec;

    private Collider m_col;
    private Rigidbody m_rb;

    [Header("==== Current state ====")]
    [SerializeField] private float m_pushTime;   // Space key press duration
    [SerializeField] private float m_currentSpeed;

    private void Awake()
    {
        m_col = GetComponent<Collider>();
        m_rb = GetComponent<Rigidbody>();
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

    public void AddForce(Vector3 addVel)
    {
        //transform.position += addVel;
        m_rb.AddForce(addVel);
    }

    public void MoveForward()
    {
        if (isStopped) return;

        if (m_currentSpeed < m_spec.TopSpeed)
        {
            m_currentSpeed += m_spec.Acceleration * Time.deltaTime;
            if (m_currentSpeed < m_spec.TopSpeed)
            {
                m_currentSpeed = m_spec.TopSpeed;
            }
        }

        AddForce(transform.forward * m_currentSpeed * Time.deltaTime);
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


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 closestPoint = m_col.ClosestPoint(transform.position);
        Debug.Log($"[OCE] closestPoint: {closestPoint}");
    }
}
