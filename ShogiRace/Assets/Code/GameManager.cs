using UnityEngine;
using System.Collections.Generic;

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
    }

    [Header("==== Parameter ====")]
    [SerializeField] private float m_appearanceTime;
    [SerializeField] private float m_getSetTime;   // The time from the start of the countdown to "GO"
    [SerializeField] private int m_maxParticipants;

    [Header("==== Information ====")]
    [SerializeField] private int m_participants;
    [SerializeField] private List<PieceRacer> m_racerList;

    [Header("Selected Piece")]
    public Enum_PieceType PlayersPieceType;

    public float AppearanceTime => m_appearanceTime;
    public float GetSetTime => m_getSetTime;
    public int Participants => m_participants;
    public List<PieceRacer> RacerList => m_racerList;


    private void RaceStart()
    {

    }

    private void RaceFinish()
    {

    }

    public void AddRacer(PieceRacer pieceRacer)
    {
        RacerList.Add(pieceRacer);
    }

    public void StopAllPiece(bool isStop)
    {
        foreach (var racer in RacerList)
        {
            racer.Controller.Stop(racer.Action);
        }
    }
}
