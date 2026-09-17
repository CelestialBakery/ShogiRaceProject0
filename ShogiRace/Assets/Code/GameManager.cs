using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("==== Parameter ====")]
    [SerializeField] private float ReadyTime;   // The time from the start of the countdown to "GO"

    [Header("==== Information ====")]
    [SerializeField] private float m_time;
    [SerializeField] private int m_pieceCount;
    [SerializeField] private List<PieceRacer> RacerList; 

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

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
