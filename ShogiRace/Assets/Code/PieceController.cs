using System;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [NonSerialized] public float Gravity = 9.81f;
    private IPieceBehaviour m_pieceBehaviour;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        m_pieceBehaviour = GetComponent<IPieceBehaviour>();

        if (m_pieceBehaviour != null )
        {
            m_pieceBehaviour.Initialized(this);
        }
    }

    private void Update()
    {
        m_pieceBehaviour.Execute();
    }

    public void Stop(PieceAction pieceAction)
    {
        pieceAction.isStopped = true;
    }

    public void StopAllPiece(bool isStop)
    {

    }

    public interface IPieceBehaviour
    {
        public void Initialized(PieceController pieceController);
        public void Execute();
        public void Stop(PieceAction pieceAction);
    }
}
