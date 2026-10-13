using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private RacerSaveManager m_racerSaveManager;
    [SerializeField] private List<Vector3> m_startPositions;
    [SerializeField] private List<Vector3> m_lotteryStartPositions;

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
        for (int i = 0; i < m_racerSaveManager.RacerObjes.Count; i++)
        {
            if (m_racerSaveManager.RacerObjes[i] == null) break;
            InstantiatePiece(m_racerSaveManager.RacerObjes[i]);
        }
    }

    private void Update()
    {
        
    }

    private void InstantiatePiece(GameObject racer)
    {
        GameObject obj = Instantiate(racer);
        //racer.SetActive(true);
        obj.transform.position = GetRandomStartPosition();
    }

    private Vector3 GetRandomStartPosition()
    {
        int index = Random.Range(0, m_lotteryStartPositions.Count);
        Vector3 position = m_lotteryStartPositions[index];
        m_lotteryStartPositions.RemoveAt(index);
        return position;
    }
}
