using UnityEngine;

public enum PlayerState
{
}

public class PlayerParameters
{
    public PlayerMovementParameters Movement = new PlayerMovementParameters();
}

public class PlayerMovementParameters
{
    public Vector2 LookDirection = Vector2.right;
    public float LookAngle = 0f;
}