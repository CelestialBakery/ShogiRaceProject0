using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Select_Piece : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private RacerSaveManager m_racerSaveManager;

    [Header("==== Participants settings ====")]
    [SerializeField][Range(1, 8)] private int m_participants;

    [Header("==== Current state ====")]
    [SerializeField] private int m_currentPieceIndex;

    [Header("==== Piece list ====")]
    [SerializeField] private List<RectTransform> m_pieceList;

    [Header("==== Cursor ====")]
    [SerializeField] private RectTransform m_cursor;
    [SerializeField] private Vector3 m_cursorOffset;


    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log($"AÅI");
            PressUp();
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"DÅI");
            PressDown();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Spece!");
            PressDecide();
        }
    }

    public void PressUp()
    {
        m_currentPieceIndex -= 1;
        if (m_currentPieceIndex < 0 )
        {
            m_currentPieceIndex = m_pieceList.Count - 1;
        }
        m_cursor.position = m_pieceList[m_currentPieceIndex].position + m_cursorOffset;
    }

    public void PressDown() 
    {
        m_currentPieceIndex += 1;
        if (m_currentPieceIndex > m_pieceList.Count - 1)
        {
            m_currentPieceIndex = 0;
        }
        m_cursor.position = m_pieceList[m_currentPieceIndex].position + m_cursorOffset;
    }

    public void PressDecide()
    {
        m_gameManager.PieceType = (Enum_PieceType)Enum.ToObject(typeof(Enum_PieceType), m_currentPieceIndex);
        m_racerSaveManager.SetRacer(0, m_gameManager.PieceType);
        //m_gameManager.AddRacer()
        SceneManager.LoadScene("TestScene");
    }
}
