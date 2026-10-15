using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private RacerSaveManager m_racerSaveManager;
    [SerializeField] private List<Vector3> m_startPositions;
    [SerializeField] private List<Vector3> m_lotteryStartPositions;
    [SerializeField] private List<GameObject> m_racerObjects;

    [Header("==== Information ====")]
    [SerializeField] private float m_appearanceTime;
    [SerializeField] private float m_getSetTime;
    [SerializeField] private float m_time;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        m_lotteryStartPositions = m_startPositions;
    }

    private void Start()
    {
        Debug.Log("Ready");
        for (int i = 0; i < m_racerSaveManager.Racers.Count; i++)
        {
            if (m_racerSaveManager.Racers[i] == null) break;
            m_racerSaveManager.InstantiatePiece(m_gameManager.DataList[i]);
        }
        m_racerSaveManager.InitializeStartPositions(ref m_startPositions);
    }

    private void Update()
    {
        
    }



    public void StopAllPiece(bool isStop)
    {
        foreach (var racer in m_racerSaveManager.Racers)
        {
            racer.Controller.Stop(racer.Action);
        }
    }
}
