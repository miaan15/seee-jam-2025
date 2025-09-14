using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerAnimation : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerParameters parameters;

    private Animator animator;

    private void Start()
    {
        manager = GetComponent<PlayerManager>();
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

        if (parameters.IsAttacking)
        {
            animator.Play("Attack" + GetDirectionalAnimationString());
            return;
        }
        else
        {
            if (parameters.MoveDirectionInput == Vector2.zero)
            {
                animator.Play("Idle" + GetDirectionalAnimationString());
            }
            else
            {
                animator.Play("Run" + GetDirectionalAnimationString());
            }
        }
    }

    private string GetDirectionalAnimationString()
    {
        if (parameters.LookDirection.x > 0)
            return "Side";
        else if (parameters.LookDirection.x < 0)
            return "Side";
        else if (parameters.LookDirection.y > 0)
            return "Up";
        else if (parameters.LookDirection.y < 0)
            return "Down";
        return "Down";
    }
}