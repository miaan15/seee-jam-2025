using UnityEngine;

public enum DamageType
{
    PlayerOnly,
    EnemyOnly,
    Everything,
}

public class DamageManager : MonoBehaviour
{
    public LayerMask PlayerOnlyLayerMask;
    public LayerMask EnemyOnlyLayerMask;
    public LayerMask EverythingLayerMask;

    public void DealDamage(Vector2Int pos, int amount, DamageType type)
    {
        var center = GameManager.Instance.LayoutPosToPosition(pos);
        var layermask = type switch
        {
            DamageType.PlayerOnly => PlayerOnlyLayerMask,
            DamageType.EnemyOnly => EnemyOnlyLayerMask,
            DamageType.Everything => EverythingLayerMask,
            _ => EverythingLayerMask
        };

        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, 0.05f, layermask);
        foreach (var collider in colliders)
        {
            var damageable = collider.GetComponent<IDamageable>();
            damageable.TakeDamage(amount);
        }
    }
}
