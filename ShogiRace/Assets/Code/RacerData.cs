using UnityEngine;

[System.Serializable]
public class RacerData
{
    [SerializeField] private Enum_PieceType m_pieceType;

    [Header("==== State ====")]
    [SerializeField] private int m_point;
    [SerializeField] private float m_time;
    [SerializeField] private int m_countOfSteppedOn;
    [SerializeField] private int m_currentTeamNumber;

    public Enum_PieceType PieceType => m_pieceType;
    public int Point => m_point;
    public float Time => m_time;
    public int CountOfSteppedOn => m_countOfSteppedOn;
    

    public void Initialized(int initialTeamNumber)
    {
        m_pieceType = Enum_PieceType.King;
        m_point = 0;
        m_time = 0;
        m_currentTeamNumber = initialTeamNumber;
    }

    public void SetPieceType(Enum_PieceType pieceType)
    {
        m_pieceType = pieceType;
    }

    public void GotSteppedOn(int teamNumber)
    {
        m_countOfSteppedOn++;
        m_currentTeamNumber = teamNumber;
    }
}
