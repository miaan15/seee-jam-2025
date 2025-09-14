using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerParameters parameters;

    // =================================================================
    private TimeStamp endAttackTimestamp = new();
    private TimeStamp startCanAttackTimestamp = new();

    private void Start()
    {
        manager = GetComponent<PlayerManager>();
        parameters = manager.Parameters;
    }

    private void Update()
    {
        parameters.MoveDirectionInput = manager.Input.Player.Move.ReadValue<Vector2>().normalized;
        if (manager.Input.Player.Attack.triggered)
            parameters.AttackInput = true;

        // =================================================================
        parameters.MoveDirection = parameters.MoveDirectionInput;
    }

    private void FixedUpdate()
    {
        // =================================================================
        if (!parameters.IsAttacking)
        {
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

        // Move
        Vector3 force;
        if (parameters.MoveDirection == Vector2.zero)
        {
            force = -manager.Body.linearVelocity.normalized * manager.Data.Movement.Deceleration;
            force = Vector2.ClampMagnitude(force, manager.Body.linearVelocity.magnitude / Time.fixedDeltaTime);
        }
        else if (Vector2.Dot(parameters.MoveDirection, manager.Body.linearVelocity.normalized) < -Mathf.Cos(Mathf.PI / 4))
        {
            force = parameters.MoveDirection * manager.Data.Movement.TurnAcceleration;
        }
        else
        {
            force = parameters.MoveDirection * manager.Data.Movement.Acceleration;
        }

        manager.Body.AddForce(force, ForceMode2D.Force);
        manager.Body.linearVelocity = Vector2.ClampMagnitude(manager.Body.linearVelocity, manager.Data.Movement.MaxSpeed);

        // Attack
        if (parameters.AttackInput && parameters.CanAttack)
        {
            endAttackTimestamp.Set(manager.Data.Combat.AttackDuration);
            startCanAttackTimestamp.Set(manager.Data.Combat.AttackDuration + manager.Data.Combat.AttackCooldown);
            parameters.IsAttacking = true;
            parameters.CanAttack = false;
        }

        if (endAttackTimestamp.Reached())
            parameters.IsAttacking = false;
        if (startCanAttackTimestamp.Reached())
            parameters.CanAttack = true;

        parameters.AttackInput = false;
    }
}
