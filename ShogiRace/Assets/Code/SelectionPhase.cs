using System;
using System.Collections.Generic;
using System.Net;
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
    [SerializeField] private List<Sprite> m_PieceSprites;

    [Header("==== Participants settings ====")]
    [SerializeField][Range(1, 8)] private int m_participants;

    [Header("==== Current state ====")]
    [SerializeField] private int m_currentMapIndex;
    [SerializeField] private int m_currentPieceIndex;
    [SerializeField] private Phase m_currentPhase;
    [SerializeField] private int m_currentButtonIndex;

    [Header("==== Piece list ====")]
    [SerializeField] private List<RectTransform> m_pieceList;

    [Header("==== Cursor ====")]
    [SerializeField] private RectTransform m_cursor;
    [SerializeField] private Vector3 m_cursorOffset;

    [Header("==== PieceConfirmation ====")]
    [SerializeField] private List<Image> m_currentPieces;
    [SerializeField] private List<RectTransform> m_pCButtonList;

    [Header("==== MapSelection ====")]
    [SerializeField] private List<RectTransform> m_mapList;

    private void Awake()
    {
        m_currentPhase = Phase.MapSelection;
        m_currentMapIndex = 0;
        m_currentPieceIndex = 0;
        m_currentButtonIndex = 0;
        SetCursorPosition(m_mapList[0]);
    }

    private void Update()
    {
        switch (m_currentPhase)
        {
            case Phase.MapSelection:
                {
                    MapSelectionScreen();
                }
                break;
            case Phase.MyPieceConfirmation:
                {
                    MyPieceConfirmationScreen();
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

    private void CursorMove(ref int index, int direction, int arrayLength, List<RectTransform> rt)
    {
        if (Mathf.Abs(direction) > 1)
        {
            Debug.LogWarning($"[CM]'direction' absolute value is not 1. direction: {direction}");
            direction = 0;
        }
        index += direction;
        if (index < 0)
        {
            index = arrayLength - 1;
        }
        if (index >= arrayLength)
        {
            index = 0;
        }
        m_cursor.position = rt[index].position + m_cursorOffset;
    }

    public void PieceDecide(int racerID)
    {
        Enum_PieceType pieceType = (Enum_PieceType)Enum.ToObject(typeof(Enum_PieceType), m_currentPieceIndex);
        m_gameManager.DataList[racerID].SetPieceType(pieceType);
        m_racerSaveManager.SetRacer(racerID, pieceType);
        m_currentPieces[racerID].sprite = m_PieceSprites[m_currentPieceIndex];

        switch (m_currentPhase)
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

    private void MapSelectionScreen()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log($"[MSS]A！");
            CursorMove(ref m_currentMapIndex, -1, m_mapList.Count, m_mapList);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"[MSS]D！");
            CursorMove(ref m_currentMapIndex, 1, m_mapList.Count, m_mapList);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[MSS]Space!");
            SetPhase(Phase.MyPieceConfirmation);
            SetCursorPosition(m_pCButtonList[0]);
        }
    }

    private void SetCursorPosition(RectTransform rt)
    {
        m_cursor.position = rt.position + m_cursorOffset;
    }

    private void MyPieceConfirmationScreen()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log($"[MPCS]A！");
            m_currentButtonIndex = 0;
            m_cursor.position = m_pCButtonList[m_currentButtonIndex].position + m_cursorOffset;

        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"[MPCS]D！");
            m_currentButtonIndex = 1;
            m_cursor.position = m_pCButtonList[m_currentButtonIndex].position + m_cursorOffset;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[MPCS]Spece!");
            switch (m_currentButtonIndex)
            {
                case 0:
                    {
                        SetPhase(Phase.MyPieceSelection);
                        SetCursorPosition(m_pieceList[m_currentPieceIndex]);
                    }
                    break;

                case 1:
                    {
                        SetPhase(Phase.CPUSettings);
                        //SetCursorPosition()
                    }
                    break;
            }
        }
    }

    private void PieceSelectionScreen(int racerID)
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log($"[PSS]A！");
            CursorMove(ref m_currentPieceIndex, -1, m_pieceList.Count, m_pieceList);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"[PSS]D！");
            CursorMove(ref m_currentPieceIndex, 1, m_pieceList.Count, m_pieceList);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[PSS]Space!");
            PieceDecide(racerID);
        }
    }

    private void SetPhase(Phase setPhase)
    {
        m_phaseScreens[(int)m_currentPhase].SetActive(false);
        m_currentPhase = setPhase;
        m_phaseScreens[(int)m_currentPhase].SetActive(true);
    }
}
