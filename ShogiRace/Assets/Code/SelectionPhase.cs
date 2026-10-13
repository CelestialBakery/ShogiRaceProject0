using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionPhase : MonoBehaviour
{
    [System.Serializable]
    private enum Phase
    {
        MapSelection,
        MyPieceConfirmation,
        MyPieceSelection,
        CPUSettings,
        CPUPieceSettings
    }

    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private RacerSaveManager m_racerSaveManager;
    [SerializeField] private List<GameObject> m_phaseScreens;

    [Header("==== Participants settings ====")]
    [SerializeField][Range(1, 8)] private int m_participants;

    [Header("==== Current state ====")]
    [SerializeField] private int m_currentPieceIndex;
    [SerializeField] private Phase m_currentPhase;

    [Header("==== Piece list ====")]
    [SerializeField] private List<RectTransform> m_pieceList;

    [Header("==== Cursor ====")]
    [SerializeField] private RectTransform m_cursor;
    [SerializeField] private Vector3 m_cursorOffset;

    [Header("==== Image ====")]
    [SerializeField] private List<Image> m_currentPieces;


    private void Update()
    {
        switch (m_currentPhase)
        {
            case Phase.MapSelection: break;
            case Phase.MyPieceConfirmation:
                {

                }
                break;
            case Phase.MyPieceSelection:
                {
                    PieceSelectionScreen(0);
                }
                break;
            case Phase.CPUSettings: break;
            case Phase.CPUPieceSettings: break;
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

    public void PressDecide(int racerID)
    {
        Enum_PieceType pieceType = (Enum_PieceType)Enum.ToObject(typeof(Enum_PieceType), m_currentPieceIndex);
        m_gameManager.RacerList[racerID].SetPieceType(pieceType);
        m_racerSaveManager.SetRacer(racerID, pieceType);

        switch(m_currentPhase)
        {
            case Phase.MyPieceSelection:
                {
                    m_currentPhase = Phase.MyPieceConfirmation;
                }
                break;

            case Phase.CPUPieceSettings:
                {
                    m_currentPhase = Phase.CPUSettings;
                }
                break;

            default:
                {
                    Debug.LogError($"Unforeseen phase. Current phase: {m_currentPhase}");
                    m_currentPhase = Phase.MapSelection;
                }
                break;
        }
        //m_gameManager.AddRacer()
        //SceneManager.LoadScene("TestField");
    }

    private void PieceSelectionScreen(int racerID)
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
            PressDecide(racerID);
        }
    }
}
