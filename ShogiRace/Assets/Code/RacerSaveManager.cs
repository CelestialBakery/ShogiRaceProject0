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
    //[SerializeField] private List<PieceRacer> m_racers; // Enpty object
    [SerializeField] private List<GameObject> m_prefabs;

    [Header("==== BaseStartPosition ====")]
    [SerializeField] private Vector3 m_baseStartPosition;
    [SerializeField] private List<Vector3> m_startPositionOffsets;
    [SerializeField] private List<Vector3> m_lotteryOffsets;
    //[SerializeField] private List<Vector3> m_startPositions;
    [SerializeField] private List<PieceRacer> m_pieceRacers;

    [SerializeField] private int m_participants;

    //public List<PieceRacer> Racers => m_racers;
    public Vector3 BaseStartPosition => m_baseStartPosition;

    private void Start()
    {
        
    }

    //public void SetRacer(int playerNum, Enum_PieceType pieceType)
    //{
    //    Debug.Log($"SetRacer");
    //    for (int i = m_racers.Count; i <= playerNum; i++)
    //    {
            //m_racers.Add(new PieceRacer());
    //    }
    //    m_racers[playerNum] = m_prefabs[(int)pieceType].GetComponent<PieceRacer>();
    //    m_racers[playerNum].SetPieceType(pieceType);
    //}

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
        m_pieceRacers.Add(obj.GetComponent<PieceRacer>());
        //racer.SetActive(true);
    }

    private Vector3 GetRandomStartPosition()
    {
        int index = Random.Range(0, m_lotteryOffsets.Count);
        Vector3 position = m_baseStartPosition + m_lotteryOffsets[index];
        m_lotteryOffsets.RemoveAt(index);
        return position;
    }

    public void InitializeStartPositions()
    {
        //Debug.Log($"[ISP]ref positions.Count: {positions.Count}");
        m_lotteryOffsets = new();
        for (int i = 0; i < m_participants; i++)
        {
            m_lotteryOffsets.Add(m_startPositionOffsets[i]);
        }
        //Debug.Log($"[ISP]lotteryOffsets.Count: {m_lotteryOffsets.Count}");

        for (int i = 0; i < m_participants; i++)
        {
            m_pieceRacers[i].GetComponentInParent<Transform>().position = GetRandomStartPosition();
        }
    }

    public void SetParticipants(int participants)
    {
        //Debug.Log($"SetParticipants: {participants}");
        m_participants = participants;
    }

    //public void AddRacer(Transform transform)
    //{
    //    m_pieces.Add(transform);
    //}

    public void ClearPieceObjects()
    {
        m_pieceRacers.Clear();
    }

    public void RaceStart()
    {
        Debug.Log("[RaceStart]");
        for (int i = 0; i < m_participants;i++)
        {
            m_pieceRacers[i].Resume(m_pieceRacers[i].Action);
        }
    }

}
