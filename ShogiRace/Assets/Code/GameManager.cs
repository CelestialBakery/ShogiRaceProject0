using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.LogWarning($"[GM]DontDestroyOnLoad");
        Application.targetFrameRate = 60;
        Initialize();
    }

    [Header("==== Parameter ====")]
    [SerializeField] private float m_appearanceTime;
    [SerializeField] private float m_getSetTime;   // The time from the start of the countdown to "GO"
    [SerializeField] private int m_maxParticipants;

    [Header("==== Information ====")]
    [SerializeField] private int m_participants;
    [SerializeField] private List<RacerData> m_dataList;

    [Header("Selected Piece")]
    public Enum_PieceType PlayersPieceType;

    public float AppearanceTime => m_appearanceTime;
    public float GetSetTime => m_getSetTime;
    public int MaxParticipants => m_maxParticipants;
    public int Participants => m_participants;
    public List<RacerData> DataList => m_dataList;


    private void RaceStart()
    {

    }

    private void RaceFinish()
    {

    }

    private void Initialize()
    {
        AddRacer(new RacerData());
        for (int i = 0; i < m_dataList.Count; i++)
        {
            SetData(i, Enum_PieceType.King);
        }
    }

    public void AddRacer(RacerData data)
    {
       
        m_dataList.Add(data);
    }

    public void RemoveRacer(RacerData data)
    {
        m_dataList.Remove(data);
    }

    public void RemoveRacerAtLast()
    {
        if (m_dataList.Count <= 0)
        {
            Debug.LogError("m_dataList.Count was empty");
            return;
        }
        m_dataList.Remove(m_dataList[m_dataList.Count - 1]);
    }

    public void SetData(int playerNum, Enum_PieceType pieceType)
    {
        Debug.Log("SetData");
        m_dataList[playerNum].SetPieceType(pieceType);
    }

    //prov
    public void LoadRaceScene()
    {
        SceneManager.LoadScene("TestField");
    }
}
