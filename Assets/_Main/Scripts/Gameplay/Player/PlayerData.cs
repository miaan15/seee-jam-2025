using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public PlayerMovementData Movement;

    [SerializeField]
    public PlayerCombatData Combat;
}

[Serializable]
public class PlayerMovementData
{
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float TurnAcceleration;
}

[Serializable]
public class PlayerCombatData
{
    public float AttackDuration;
    public float AttackCooldown;
    public int AttackDamage;
}