using UnityEngine;
using static PieceController;

public class PieceRacer : MonoBehaviour, IPieceBehaviour
{
    [SerializeField] private PieceController m_controller;
    [SerializeField] private Enum_PieceType m_pieceType;
    [SerializeField] private PieceAction m_pieceAction;
    [Header("==== PieceSpec ====")]
    [SerializeField] private PieceSpec m_spec;

    public PieceController Controller => m_controller;
    public Enum_PieceType PieceType => m_pieceType;
    public PieceAction Action => m_pieceAction;

    public void Initialized(PieceController controller)
    {
        m_controller = controller;
        m_pieceAction.Initialize(m_controller, m_spec);
    }

    public void Execute()
    {
        m_pieceAction.MoveForward();
    }

    public void Stop(PieceAction pieceAction) => m_controller.Stop(m_pieceAction);
}
