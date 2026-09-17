using System;
using UnityEngine;

public class Select_Piece : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;

    [Header("==== Current state ====")]
    [SerializeField] private Enum_PieceType m_pieceType;
    [SerializeField] private int m_currentPieceIndex;

    public void PressUp()
    {
        m_currentPieceIndex -= 1;
        if (m_currentPieceIndex < 0 )
        {
            m_currentPieceIndex = (int)Enum_PieceType.Max;
        }
    }

    public void PressDown() 
    {
        m_currentPieceIndex += 1;
        if (m_currentPieceIndex > (int)Enum_PieceType.Max )
        {
            m_currentPieceIndex = 0;
        }
    }

    public void PressDecide()
    {
        m_pieceType = (Enum_PieceType)Enum.ToObject(typeof(Enum_PieceType), m_currentPieceIndex);
        //m_gameManager.AddRacer()
    }
}
