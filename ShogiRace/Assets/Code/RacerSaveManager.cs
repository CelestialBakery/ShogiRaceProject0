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
    [SerializeField] private List<GameObject> m_soullessObjes; // Enpty object
    [SerializeField] private List<GameObject> m_prefabs;

    private int m_participants = 0;

    private void Start()
    {
        
    }

    public void SetRacer(int playerNum, Enum_PieceType pieceType)
    {
        m_soullessObjes[playerNum] = m_prefabs[playerNum];
    }

    public void GameStart()
    {
        m_participants = m_gameManager.Participants;
        for (int i = 0; i < m_participants; i++)
        {
            m_gameManager.AddRacer(m_soullessObjes[i].GetComponent<PieceRacer>());
            m_soullessObjes[i].SetActive(true);
            
        }
    }
}
