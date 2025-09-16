using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    private EnemyManager manager;

    public int Health;

    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
    }

    private void Start()
    {
        Health = manager.Data.Health;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
    }
}
