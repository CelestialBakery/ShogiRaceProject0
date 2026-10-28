using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("==== Conponent ====")]
    [SerializeField] private PieceAction m_pieceAction;

    private GameInputs m_gameInputs;
    [Header("Debug")]
    [SerializeField] private Vector2 m_inputVector;

    private void Awake()
    {
        m_gameInputs = new GameInputs();

        m_gameInputs.Player.Move.started += OnMove;
        m_gameInputs.Player.Move.performed += OnMove;
        m_gameInputs.Player.Move.canceled += OnMove;
        m_gameInputs.Player.Push.performed += OnPush;

        m_gameInputs.Enable();
    }

    private void Start()
    {
        Invoke(nameof(SetPieceAction), 0.016f);   
    }

    private void SetPieceAction()
    {
        m_pieceAction = GameObject.Find("PieceRacer0")?.GetComponent<PieceAction>();
    }

    private void OnDestroy()
    {
        m_gameInputs?.Dispose();
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        m_inputVector = context.ReadValue<Vector2>();
        //m_pieceAction.SendInputVector(m_inputVector);
    }

    private void OnPush(InputAction.CallbackContext context)
    {
        m_pieceAction.Trample();
    }

    private void Update()
    {
        if (m_pieceAction == null) return;
        m_pieceAction.SendInputVector(m_inputVector);
        m_pieceAction.Turning();
    }
}
