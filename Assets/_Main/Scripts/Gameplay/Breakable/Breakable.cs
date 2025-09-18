using UnityEngine;

public class Breakable : MonoBehaviour, IDamageable
{
    public Vector2Int GridPosition;

    public void TakeDamage(int amount)
    {
        GameManager.Instance.LevelLayout.SetFlag(
            GridPosition,
            LevelLayoutFlag.None
        );

        gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
}