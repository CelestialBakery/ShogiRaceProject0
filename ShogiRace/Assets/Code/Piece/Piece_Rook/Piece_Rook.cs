using UnityEngine;
using static PieceController;

public class Piece_Rook : MonoBehaviour, IPieceBehaviour
{
    private PieceController m_pieceController;
    private PieceAction m_pieceAction;
    [SerializeField] private PieceSpec m_spec;

    public void Initialized(PieceController pieceController)
    {
        m_pieceController = pieceController;
        m_pieceAction.Initialize(pieceController, m_spec);
    }

    public void Execute()
    {
        m_pieceAction.MoveForward();
    }

    public void Stop(PieceAction pieceAction) => m_pieceController.Stop(m_pieceAction);
}
