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
        StartCoroutine(DealDamageDelayed(pos, amount, type));
    }

    private System.Collections.IEnumerator DealDamageDelayed(Vector2Int pos, int amount, DamageType type)
    {
        yield return new WaitForSeconds(0.1f);

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
            if (damageable != null)
                damageable.TakeDamage(amount);
        }
    }
}
