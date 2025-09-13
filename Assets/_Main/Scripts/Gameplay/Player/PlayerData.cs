using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public PlayerMovementData Movement;
}

[Serializable]
public class PlayerMovementData
{
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float TurnAcceleration;
}