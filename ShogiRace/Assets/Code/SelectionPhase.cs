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
    [SerializeField] private List<Sprite> m_pieceSprites;

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
    [SerializeField] private List<RectTransform> m_pCButtonList;    // Piece confirmation phase
    [SerializeField] private List<RectTransform> m_cPUSButtonList;  // CPU settings phase
    private int[][] m_cPUSButtonIndexes;
    private int m_currentX;
    private int m_currentY;
    private int m_minCPUIndex = 2;
    private int m_maxCPUIndex = 8;

    [Header("==== MapSelection ====")]
    [SerializeField] private List<RectTransform> m_mapList;

    private void Awake()
    {
        m_currentPhase = Phase.MapSelection;
        m_currentMapIndex = 0;
        m_currentPieceIndex = 0;
        m_currentButtonIndex = 0;
        SetCursorPosition(m_mapList[0]);
        SetPieceSprite(0, 0);   // (player0, King)
        SetButtonArray();
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

    private void CursorMoveV2(ref int index, int x, int y, List<RectTransform> rt)
    {
        ChangeCurrentButtonIndex(x, y);
        m_cursor.position = rt[index].position + m_cursorOffset;
    }

    public void PieceDecide(int racerID)
    {
        Enum_PieceType pieceType = (Enum_PieceType)Enum.ToObject(typeof(Enum_PieceType), m_currentPieceIndex);
        for (int i = m_gameManager.DataList.Count; i <= racerID; i++)
        {
            Debug.Log($"Add.RacerData, count: {m_gameManager.DataList.Count}");
            m_gameManager.AddRacer(new RacerData());
        }
        m_gameManager.DataList[racerID].SetPieceType(pieceType);
        m_racerSaveManager.SetRacer(racerID, pieceType);
        SetPieceSprite(racerID, m_currentPieceIndex);

        switch (m_currentPhase)
        {
            case Phase.MyPieceSelection:
                {
                    SetPhase(Phase.MyPieceConfirmation);
                    SetCursorPosition(m_pCButtonList[0]);
                }
                break;

            case Phase.CPUPieceSettings:
                {
                    SetPhase(Phase.CPUSettings);
                }
                break;

            default:
                {
                    Debug.LogError($"Unforeseen phase. Current phase: {m_currentPhase}");
                    SetPhase(Phase.MapSelection);
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

    private void CPUSettingScreen()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]W！");
            CursorMoveV2(ref m_currentButtonIndex, 0, -1, m_cPUSButtonList);
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]S！");
            CursorMoveV2(ref m_currentButtonIndex, 0, 1, m_cPUSButtonList);
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]A！");
            CursorMoveV2(ref m_currentButtonIndex, -1, 0, m_cPUSButtonList);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]D！");
            CursorMoveV2(ref m_currentButtonIndex, 1, 0, m_cPUSButtonList);
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[CPUSS]Space!");
            switch (m_currentButtonIndex)
            {
                case 0: // left arrow
                    {
                        ChangeParticipants(-1);
                    }
                    break;

                case 1: // right arrow
                    {
                        ChangeParticipants(1);
                    }
                    break;

                case 2: // CPU1
                    {
                        SetPhase(Phase.CPUPieceSettings);
                    }
                    break;
            }
        }
    }

    private void SetPhase(Phase setPhase)
    {
        m_phaseScreens[(int)m_currentPhase].SetActive(false);
        m_currentPhase = setPhase;
        m_phaseScreens[(int)m_currentPhase].SetActive(true);
    }

    private void SetPieceSprite(int racerID, int index)
    {
        m_currentPieces[racerID].sprite = m_pieceSprites[index];
    }


    private void SetButtonArray()
    {
        int[] array1 = { 0, 1, 2, 3, 4 };
        int[] array2 = { 0, 1, 5, 6, 7 };
        int[] array3 = { 0, 1, 8, 9, 9 };

        m_cPUSButtonIndexes = new int[3][]
            {
                array1,
                array2,
                array3
            };
    }

    private void ChangeCurrentButtonIndex(int x, int y)
    {
        m_currentX += x;
        m_currentY += y;
        int xLength = m_cPUSButtonIndexes.Length;
        int yLength = m_cPUSButtonIndexes[0].Length;

        if (m_currentX < 0) m_currentX = 0;
        if (m_currentY < 0) m_currentY = 0;
        if (m_currentX >= xLength) m_currentX = xLength - 1;
        if (m_currentY >= yLength) m_currentY = yLength - 1;

        m_currentButtonIndex = m_cPUSButtonIndexes[m_currentX][m_currentY];
    }

    private void ChangeParticipants(int addend)
    {
        m_participants += addend;

        for (int i = m_minCPUIndex; i < m_maxCPUIndex; i++)
        {
            if (i < m_participants + m_minCPUIndex)
            {
                m_cPUSButtonList[i].GetComponent<GameObject>().SetActive(true);
            }
            else if (i >= m_participants + m_minCPUIndex)
            {
                m_cPUSButtonList[i].GetComponent<GameObject>().SetActive(false);
            }
        }
        //m_cPUSButtonList
    }
}
