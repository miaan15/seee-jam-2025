using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerMovementData movementData;
    private PlayerParameters parameters;

    private void Start()
    {
        manager = GetComponent<PlayerManager>();
        movementData = manager.Data.Movement;
        parameters = manager.Parameters;
    }

    private void Update()
    {
        parameters.MoveDirection = manager.Input.Player.Move.ReadValue<Vector2>().normalized;

        if (parameters.MoveDirection != Vector2.zero)
        {
            if (parameters.MoveDirection.x > 0)
                parameters.LookDirection = Vector2.right;
            else if (parameters.MoveDirection.x < 0)
                parameters.LookDirection = Vector2.left;
            else if (parameters.MoveDirection.y > 0)
                parameters.LookDirection = Vector2.up;
            else if (parameters.MoveDirection.y < 0)
                parameters.LookDirection = Vector2.down;
        }
        if (parameters.LookDirection.x > 0)
            parameters.LastFacingDirection = 1;
        else if (parameters.LookDirection.x < 0)
            parameters.LastFacingDirection = -1;
    }

    private void FixedUpdate()
    {
        // Move
        Vector3 force;
        if (parameters.MoveDirection == Vector2.zero)
        {
            force = -manager.Body.linearVelocity.normalized * movementData.Deceleration;
            force = Vector2.ClampMagnitude(force, manager.Body.linearVelocity.magnitude / Time.fixedDeltaTime);
        }
        else if (Vector2.Dot(parameters.MoveDirection, manager.Body.linearVelocity.normalized) < -Mathf.Cos(Mathf.PI / 4))
        {
            force = parameters.MoveDirection * movementData.TurnAcceleration;
        }
        else
        {
            force = parameters.MoveDirection * movementData.Acceleration;
        }

        manager.Body.AddForce(force, ForceMode2D.Force);
        manager.Body.linearVelocity = Vector2.ClampMagnitude(manager.Body.linearVelocity, movementData.MaxSpeed);
    }
}
