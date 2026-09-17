using UnityEngine;

[System.Serializable]
public class PieceSpec
{
    [SerializeField] private float m_acceleration;
    [SerializeField] private float m_velocityConvergence;
    [SerializeField] private float m_topSpeed;
    [SerializeField] private float m_turning;
    [SerializeField] private float m_grip;
    [SerializeField] private float m_charge;
    [SerializeField] private float m_jumpPower;
    [SerializeField] private float m_liftPower;
    [SerializeField] private float m_tiltUpwardLimit;
    [SerializeField] private float m_flightSpeed;
    [SerializeField] private float m_weight;
    [SerializeField] private float m_attackRange;

    public float Acceleration => m_acceleration;
    public float VelocityConvergence => m_velocityConvergence;
    public float TopSpeed => m_topSpeed;
    public float Turning => m_turning;
    public float Grip => m_grip;
    public float Charge => m_charge;
    public float JumpPower => m_jumpPower;
    public float LiftPower => m_liftPower;
    public float TiltUpwardLimit => m_tiltUpwardLimit;
    public float FlightSpeed => m_flightSpeed;
    public float Weight => m_weight;
    public float AttackRange => m_attackRange;
}
