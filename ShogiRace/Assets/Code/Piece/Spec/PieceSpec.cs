using UnityEngine;

[CreateAssetMenu(fileName = "PieceSpec", menuName = "Scriptable Objects/PieceSpec")]
public class PieceSpec : ScriptableObject
{
    [SerializeField] private float m_acceleration;
    [SerializeField] private float m_velocityConvergence;
    [SerializeField] private float m_topSpeed;
    [SerializeField] private float m_turning;
    [SerializeField] private float m_grip;
    [SerializeField] private float m_charge;
    [SerializeField] private float m_releaseAcceleration;
    [SerializeField] private float m_jumpPower;
    [SerializeField] private float m_liftPower;
    [SerializeField] private float m_tiltUpwardLimit;
    [SerializeField] private float m_takeoffAcceleration;
    [SerializeField] private float m_flightVelocityConvergence;
    [SerializeField] private float m_flightSpeed;
    [SerializeField] private float m_flightTurning;
    [SerializeField] private float m_weight;
    [SerializeField] private float m_attackRange;

    public float Acceleration => m_acceleration;
    public float VelocityConvergence => m_velocityConvergence;
    public float TopSpeed => m_topSpeed;
    public float Turning => m_turning;
    public float Grip => m_grip;
    public float Charge => m_charge;
    public float ReleaseAcceleration => m_releaseAcceleration;
    public float JumpPower => m_jumpPower;
    public float LiftPower => m_liftPower;
    public float TiltUpwardLimit => m_tiltUpwardLimit;
    public float TakeoffAcceleration => m_takeoffAcceleration;
    public float FlightVelocityConvergence => m_flightVelocityConvergence;
    public float FlightSpeed => m_flightSpeed;
    public float FlightTurning => m_flightTurning;
    public float Weight => m_weight;
    public float AttackRange => m_attackRange;
}
