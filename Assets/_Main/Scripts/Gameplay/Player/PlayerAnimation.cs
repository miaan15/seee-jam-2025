using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerAnimation : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerMovementData movementData;
    private PlayerParameters parameters;

    private Animator animator;

    private void Start()
    {
        manager = GetComponent<PlayerManager>();
        movementData = manager.Data.Movement;
        parameters = manager.Parameters;

        animator = manager.Animator;
    }

    private void Update()
    {
        if (parameters.LastFacingDirection == 1)
        {
            manager.SpriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (parameters.LastFacingDirection == -1)
        {
            manager.SpriteTransform.localScale = new Vector3(-1, 1, 1);
        }

        if (parameters.MoveDirection == Vector2.zero)
        {
            if (parameters.LookDirection.x > 0)
            {
                animator.Play("IdleSide");
            }
            else if (parameters.LookDirection.x < 0)
            {
                animator.Play("IdleSide");
            }
            else if (parameters.LookDirection.y > 0)
            {
                animator.Play("IdleUp");
            }
            else if (parameters.LookDirection.y < 0)
            {
                animator.Play("IdleDown");
            }
        }
        else
        {
            if (parameters.LookDirection.x > 0)
            {
                animator.Play("RunSide");
            }
            else if (parameters.LookDirection.x < 0)
            {
                animator.Play("RunSide");
            }
            else if (parameters.LookDirection.y > 0)
            {
                animator.Play("RunUp");
            }
            else if (parameters.LookDirection.y < 0)
            {
                animator.Play("RunDown");
            }
        }
    }
}