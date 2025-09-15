using System;
using UnityEngine;

[SerializeField]
[Serializable]
public class PlayerParameters
{
    public Vector2 MoveDirectionInput = Vector2.zero;

    public Vector2Int MoveDirection = Vector2Int.zero;

    public Vector2Int GridPosition;
}
