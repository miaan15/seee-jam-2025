using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerStats : MonoBehaviour, IDamageable
{
    private PlayerManager manager;

    public int Health;
    public float InvisibleDuration = 0.5f;

    private bool isInvisible = false;
    private float invisibleTimer = 0f;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        Health = manager.Data.Health;
    }

    private void Update()
    {
        if (invisibleTimer > 0)
        {
            invisibleTimer -= Time.deltaTime;
            if (invisibleTimer <= 0)
            {
                isInvisible = false;
                invisibleTimer = 0f;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvisible)
        {
            Debug.Log("Player is currently invincible and cannot take damage.");
            return;
        }

        Health -= amount;
        GameManager.Instance.SoundManager.PlaySFX("hurt");
        hurtAni.Hurt();
        ScreenShake.Shake(0.3f, 0.3f);

        isInvisible = true;
        invisibleTimer = InvisibleDuration;
        Debug.Log($"Player took {amount} damage. Remaining health: {Health}");
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.EndGame(false);
    }
}