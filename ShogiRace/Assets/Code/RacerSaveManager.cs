using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RacerSaveManager : MonoBehaviour
{
    public static RacerSaveManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.LogWarning($"[RSM]DontDestroyOnLoad");
    }

    [SerializeField] private GameManager m_gameManager;
    //[SerializeField] private ObjectPoolManager m_poolManager;
    [SerializeField] private List<PieceRacer> m_racers; // Enpty object
    [SerializeField] private List<GameObject> m_prefabs;

    [Header("==== BaseStartPosition ====")]
    [SerializeField] private Vector3 m_baseStartPosition;
    [SerializeField] private List<Vector3> m_startPositionOffsets;
    [SerializeField] private List<Vector3> m_lotteryOffsets;

    private int m_participants = 1;

    public List<PieceRacer> Racers => m_racers;
    public Vector3 BaseStartPosition => m_baseStartPosition;

    private void Start()
    {
        
    }

    public void SetRacer(int playerNum, Enum_PieceType pieceType)
    {
        Debug.Log($"SetRacer");
        for (int i = m_racers.Count; i <= playerNum; i++)
        {
            m_racers.Add(new PieceRacer());
        }
        m_racers[playerNum] = m_prefabs[(int)pieceType].GetComponent<PieceRacer>();
        m_racers[playerNum].SetPieceType(pieceType);
    }

    //public void GameStart()
    //{
    //    m_participants = m_gameManager.Participants;
    //    for (int i = 0; i < m_participants; i++)
    //    {
    //        //m_gameManager.AddRacer(m_racers[i]);
    //        //m_racers[i].SetActive(true);

    //    }
    //}

    public void InstantiatePiece(RacerData data)
    {
        GameObject obj = Instantiate(m_prefabs[(int)data.PieceType]);
        //racer.SetActive(true);
    }

    private Vector3 GetRandomStartPosition()
    {
        int index = Random.Range(0, m_lotteryOffsets.Count);
        Vector3 position = m_baseStartPosition + m_lotteryOffsets[index];
        m_lotteryOffsets.RemoveAt(index);
        return position;
    }

    public void InitializeStartPositions(ref List<Vector3> positions)
    {
        m_lotteryOffsets = new();
        for (int i = 0; i < m_participants; i++)
        {
            m_lotteryOffsets.Add(m_startPositionOffsets[i]);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = GetRandomStartPosition();
        }
    }
}
