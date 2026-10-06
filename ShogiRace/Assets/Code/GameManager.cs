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
    [SerializeField] private float m_readyTime;   // The time from the start of the countdown to "GO"
    [SerializeField] private int m_maxParticipants;

    [Header("==== Information ====")]
    [SerializeField] private float m_time;
    [SerializeField] private int m_pieceCount;
    [SerializeField] private List<PieceRacer> RacerList;

    [Header("Selected Piece")]
    public Enum_PieceType PieceType;


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
