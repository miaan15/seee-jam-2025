using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class EnemyStats : MonoBehaviour, IDamageable
{
    private EnemyManager manager;

    public int Health;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.EnemyWaveManager.RemoveEnemy(manager);
    }
}