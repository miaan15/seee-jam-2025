using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerStats : MonoBehaviour, IDamageable
{
    private PlayerManager manager;

    public int Health;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        Health = manager.Data.Health;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log($"Player took {amount} damage. Remaining health: {Health}");
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}