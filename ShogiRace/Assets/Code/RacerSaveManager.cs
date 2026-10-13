using UnityEngine;
using System.Collections.Generic;

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
    [SerializeField] private List<GameObject> m_racerObjes; // Enpty object
    [SerializeField] private List<GameObject> m_prefabs;

    private int m_participants = 0;

    public List<GameObject> RacerObjes => m_racerObjes;

    private void Start()
    {
        
    }

    public void SetRacer(int playerNum, Enum_PieceType pieceType)
    {
        m_racerObjes[playerNum] = m_prefabs[(int)pieceType];
        m_racerObjes[playerNum].GetComponent<PieceRacer>().SetPieceType(pieceType);
    }

    public void GameStart()
    {
        m_participants = m_gameManager.Participants;
        for (int i = 0; i < m_participants; i++)
        {
            m_gameManager.AddRacer(m_racerObjes[i].GetComponent<PieceRacer>());
            m_racerObjes[i].SetActive(true);
            
        }
    }
}
