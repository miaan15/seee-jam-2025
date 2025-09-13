using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerMovementData movementData;

    private Vector2 inputMoveDirection;

    // private Vector2 inputLookPoint;

    // private float turnSmoothVelocity = 0f;

    private void Start()
    {
        manager = GetComponent<PlayerManager>();
        movementData = manager.Data.Movement;
    }

    private void Update()
    {
        inputMoveDirection = manager.Input.Player.Move.ReadValue<Vector2>().normalized;
        // inputLookPoint = manager.MainCamera.ScreenToWorldPoint(manager.PlayerInputMap.FindAction("LookPoint", true).ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        // // Look
        // Vector2 lookAtDelta = inputLookPoint - (Vector2)transform.position;
        // float desiredLookAngle = Mathf.Atan2(lookAtDelta.y, lookAtDelta.x) * Mathf.Rad2Deg;

        // float trueLookAngle = transform.rotation.eulerAngles.z;
        // trueLookAngle = Mathf.SmoothDampAngle(trueLookAngle, desiredLookAngle, ref turnSmoothVelocity, 1f / manager.Data.Combat.TurnRate);
        // transform.rotation = Quaternion.Euler(0f, 0f, trueLookAngle);

        // manager.Parameters.Movement.LookAngle = trueLookAngle;
        // manager.Parameters.Movement.LookDirection = transform.right;

        // Move
        Vector3 force;
        if (inputMoveDirection == Vector2.zero)
        {
            force = -manager.Body.linearVelocity.normalized * movementData.Deceleration;
            force = Vector2.ClampMagnitude(force, manager.Body.linearVelocity.magnitude / Time.fixedDeltaTime);
        }
        else if (Vector2.Dot(inputMoveDirection, manager.Body.linearVelocity.normalized) < -Mathf.Cos(Mathf.PI / 4))
        {
            force = inputMoveDirection * movementData.TurnAcceleration;
        }
        else
        {
            force = inputMoveDirection * movementData.Acceleration;
        }

        manager.Body.AddForce(force, ForceMode2D.Force);
        manager.Body.linearVelocity = Vector2.ClampMagnitude(manager.Body.linearVelocity, movementData.MaxSpeed);
    }
}
