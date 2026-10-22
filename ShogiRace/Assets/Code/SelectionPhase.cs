using System;
using System.Collections.Generic;
using TMPro;
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
        CPUPieceSelection
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
    [SerializeField] private int m_currentPieceButtonIndex;
    [SerializeField] private int m_currentCSButtonIndex;    // confirmation screen button

    [Header("==== Piece list ====")]
    [SerializeField] private List<RectTransform> m_pieceList;

    [Header("==== Cursor ====")]
    [SerializeField] private RectTransform m_cursor;
    [SerializeField] private Vector3 m_cursorOffset;

    [Header("==== PieceConfirmation ====")]
    [SerializeField] private List<Image> m_currentPieces;
    [SerializeField] private List<RectTransform> m_pCButtonList;    // Piece confirmation phase
    [SerializeField] private List<GameObject> m_cPUSButtonList;  // CPU settings phase
    [SerializeField] private TextMeshProUGUI m_participantsText;
    private int[][] m_cPUSButtonIndexes;
    private int m_currentX;
    private int m_currentY;
    private int m_minCPUIndex = 4;

    [Header("==== MapSelection ====")]
    [SerializeField] private List<RectTransform> m_mapList;

    private void Awake()
    {
        Initialize();
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
            case Phase.CPUSettings:
                {
                    CPUSettingScreen();
                }
                break;
            case Phase.CPUPieceSelection:
                {
                    PieceSelectionScreen(m_currentCSButtonIndex - m_minCPUIndex + 1);
                }
                break;
        }
    }

    private void Initialize()
    {
        m_currentPhase = Phase.MapSelection;
        m_currentMapIndex = 0;
        m_currentPieceIndex = 0;
        m_currentPieceButtonIndex = 0;
        m_currentCSButtonIndex = 0;
        SetCursorPosition(m_mapList[0]);
        SetPieceSprite(0, 0);   // (player0, King)
        SetButtonArray();
        m_participantsText.text = m_participants.ToString();
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
        SetCursorPosition(rt[index]);
    }

    private void CursorMoveV2(ref int index, int x, int y, List<GameObject> objs)
    {
        ChangeCurrentButtonIndex(x, y);
        m_cursor.position = objs[index].transform.position + m_cursorOffset;
    }

    public void PieceDecide(int racerID)
    {
        Enum_PieceType pieceType = (Enum_PieceType)Enum.ToObject(typeof(Enum_PieceType), m_currentPieceIndex);

        Debug.Log($"racerID: {racerID}");
        m_gameManager.DataList[racerID].SetPieceType(pieceType);
        m_gameManager.SetData(racerID, pieceType);
        SetPieceSprite(racerID, m_currentPieceIndex);

        switch (m_currentPhase)
        {
            case Phase.MyPieceSelection:
                {
                    SetPhase(Phase.MyPieceConfirmation);
                    SetCursorPosition(m_pCButtonList[0]);
                }
                break;

            case Phase.CPUPieceSelection:
                {
                    SetPhase(Phase.CPUSettings);
                    SetCursorPosition(m_cPUSButtonList[m_currentCSButtonIndex].GetComponent<RectTransform>());
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
            m_currentCSButtonIndex = 0;
            SetCursorPosition(m_pCButtonList[m_currentCSButtonIndex]);

        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"[MPCS]D！");
            m_currentCSButtonIndex = 1;
            SetCursorPosition(m_pCButtonList[m_currentCSButtonIndex]);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[MPCS]Spece!");
            switch (m_currentCSButtonIndex)
            {
                case 0: // piece
                    {
                        SetPhase(Phase.MyPieceSelection);
                        SetCursorPosition(m_pieceList[m_currentPieceIndex]);
                    }
                    break;

                case 1: // next
                    {
                        SetPhase(Phase.CPUSettings);
                        SetCPUButtonIndex(3);
                        //SetCursorPosition(m_cPUSButtonList[m_currentCSButtonIndex].GetComponent<RectTransform>());
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
            CursorMoveV2(ref m_currentCSButtonIndex, 0, -1, m_cPUSButtonList);
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]S！");
            CursorMoveV2(ref m_currentCSButtonIndex, 0, 1, m_cPUSButtonList);
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]A！");
            CursorMoveV2(ref m_currentCSButtonIndex, -1, 0, m_cPUSButtonList);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"[CPUSS]D！");
            CursorMoveV2(ref m_currentCSButtonIndex, 1, 0, m_cPUSButtonList);
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[CPUSS]Space!");
            switch (m_currentCSButtonIndex)
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

                case 2: // back
                    {
                        SetPhase(Phase.MyPieceConfirmation);
                    }
                    break;

                case 3: // decide
                    {
                        Debug.Log("Race start");
                        m_gameManager.LoadRaceScene();
                    }
                    break;

                default: // CPU
                    {
                        SetPhase(Phase.CPUPieceSelection);
                        m_currentPieceIndex = (int)m_gameManager.DataList[m_currentCSButtonIndex - m_minCPUIndex + 1].PieceType;
                        SetCursorPosition(m_pieceList[m_currentPieceIndex]);
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
        int[] array1 = { 0, 1, 4, 5, 6 };
        int[] array2 = { 0, 1, 7, 8, 9 };
        int[] array3 = { 2, 2, 10, 3, 3 };

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
        int xLength = m_cPUSButtonIndexes[0].Length;
        int yLength = m_cPUSButtonIndexes.Length;

        if (m_currentX < 0) m_currentX = 0;
        if (m_currentY < 0) m_currentY = 0;
        if (m_currentX >= xLength) m_currentX = xLength - 1;
        if (m_currentY >= yLength) m_currentY = yLength - 1;
        Debug.Log($"[CCBI] (x: {m_currentX}, y: {m_currentY})");

        m_currentCSButtonIndex = m_cPUSButtonIndexes[m_currentY][m_currentX];
    }

    private void SetCPUButtonIndex(int index)
    {
        Debug.Log("SCPUBI");
        m_currentCSButtonIndex = index;
        for (int y = 0; y < m_cPUSButtonIndexes.Length; y++)
        {
            for(int x = 0; x < m_cPUSButtonIndexes[0].Length; x++)
            {
                if (m_cPUSButtonIndexes[y][x] != index) continue;
                Debug.Log($"[SCPUBI]index: {index}, (x: {x}, y: {y}) = {m_cPUSButtonIndexes[y][x]}");
                m_currentX = x;
                m_currentY = y;
                return;
            }
        }
    }

    private void ChangeParticipants(int addend)
    {
        if (Mathf.Abs(addend) != 1)
        {
            Debug.LogWarning($"The absolute value of the addend must be 1. addend: {addend}");
        }

        m_participants += addend;
        if (m_participants > m_gameManager.MaxParticipants)
        {
            m_participants = m_gameManager.MaxParticipants;
            return;
        }
        if (m_participants < 1)
        {
            m_participants = 1;
            return;
        }

        if (addend > 0)
        {
            m_gameManager.AddRacer(new RacerData());
            m_gameManager.SetData(m_gameManager.DataList.Count - 1, Enum_PieceType.King);
        }
        else
        {
            m_gameManager.RemoveRacerAtLast();
        }

        for (int i = m_minCPUIndex; i < m_cPUSButtonList.Count; i++)
        {
            if (i < m_participants - 1 + m_minCPUIndex)
            {
                m_cPUSButtonList[i].SetActive(true);
            }
            else if (i >= m_participants - 1 + m_minCPUIndex)
            {
                m_cPUSButtonList[i].SetActive(false);
            }
        }

        m_participantsText.text = m_participants.ToString();
        //m_cPUSButtonList
    }
}
