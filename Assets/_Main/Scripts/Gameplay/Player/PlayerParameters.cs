using System;
using UnityEngine;

public enum PlayerState
{
}

[SerializeField]
[Serializable]
public class PlayerParameters
{
    public Vector2 MoveDirectionInput = Vector2.down;
    public bool AttackInput = false;

    public Vector2 MoveDirection = Vector2.down;

    public Vector2 LookDirection = Vector2.down;
    public int LastFacingDirection = 1;

    public bool CanAttack = true;
    public bool IsAttacking = false;
}