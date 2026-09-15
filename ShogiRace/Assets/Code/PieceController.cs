using UnityEngine;

public class PieceController : MonoBehaviour
{
    private IPieceBehaviour m_pieceBehaviour;

    void Awake()
    {
        m_pieceBehaviour = GetComponent<IPieceBehaviour>();

        if (m_pieceBehaviour != null )
        {
            m_pieceBehaviour.Initialized();
        }
    }

    private void Update()
    {
        
    }

    #region Attack

    public void Trample()
    {

    }
    #endregion

    public interface IPieceBehaviour
    {
        public void Initialized();
        public void Execute();
    }
}
