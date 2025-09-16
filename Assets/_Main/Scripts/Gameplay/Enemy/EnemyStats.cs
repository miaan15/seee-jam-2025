using UnityEngine;

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
    }
}
