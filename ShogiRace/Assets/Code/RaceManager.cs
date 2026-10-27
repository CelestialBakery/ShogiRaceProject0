using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Analytics.IAnalytic;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private RacerSaveManager m_racerSaveManager;
    //[SerializeField] private List<Vector3> m_lotteryStartPositions;
    [SerializeField] private List<Transform> m_racerObjects;

    [Header("==== Information ====")]
    [SerializeField] private float m_appearanceTime;
    [SerializeField] private float m_getSetTime;
    [SerializeField] private float m_time;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        //m_lotteryStartPositions = m_startPositions;

        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_racerSaveManager = GameObject.Find("RacerSaveManager").GetComponent<RacerSaveManager>();
    }

    private void Start()
    {
        Debug.Log("Ready");
        Debug.Log($"DataList.Count: {m_gameManager.DataList.Count}");
        for (int i = 0; i < m_gameManager.DataList.Count; i++)
        {
            if (m_gameManager.DataList[i] == null) break;
            m_racerSaveManager.InstantiatePiece(m_gameManager.DataList[i]);
        }
        m_racerSaveManager.InitializeStartPositions();
        //m_racerSaveManager.SetStartPosition();

        m_racerSaveManager.RaceStart();
    }

    private void Update()
    {
        
    }

    

    //public void StopAllPiece(bool isStop)
    //{
    //    foreach (var racer in m_racerSaveManager.Racers)
    //    {
    //        racer.Controller.Stop(racer.Action);
    //    }
    //}

    
}
