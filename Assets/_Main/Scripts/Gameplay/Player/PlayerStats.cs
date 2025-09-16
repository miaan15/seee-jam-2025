using UnityEngine;

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
    }
}
