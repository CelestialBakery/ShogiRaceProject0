using UnityEngine;

[System.Serializable]
public class RacerData
{
    private Enum_PieceType m_pieceType;
    private int m_point;
    private float m_time;

    public Enum_PieceType PieceType => m_pieceType;
    public int Point => m_point;
    public float Time => m_time;

    public void Initialized()
    {
        m_pieceType = Enum_PieceType.King;
        m_point = 0;
        m_time = 0;
    }

    public void SetPieceType(Enum_PieceType pieceType)
    {
        m_pieceType = pieceType;
    }
}
