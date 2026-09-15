using UnityEngine;

public class PieceMove : MonoBehaviour
{
    [SerializeField] private PieceSpec m_spec;
    [SerializeField] private InputKey m_key;
    private Collider m_col;
    private Rigidbody m_rb;

    private void Awake()
    {
        m_col = GetComponent<Collider>();
        m_rb = GetComponent<Rigidbody>();
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

    // Common operation
    public void TiltUpward()
    {

    }

    public void TiltDownward()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {   
        Vector3 closestPoint = m_col.ClosestPoint(transform.position);
        Debug.Log($"[OCE] closestPoint: {closestPoint}");
    }

}
