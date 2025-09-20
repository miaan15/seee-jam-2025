using UnityEngine;

public class ZombieEnemyManager : EnemyManager
{
    public ZombieEnemyData Data;
    private int movementBeatCount = 0;

    protected override void OnStart()
    {
        Stats.Health = Data.Health;
        Parameters.GridPosition = GameManager.Instance.PositionToLayoutPos(transform.position);
    }

    protected override void OnBeat()
    {
        base.OnBeat();
        UpdateDesiredPosition();
    }

    public void UpdateDesiredPosition()
    {
        movementBeatCount++;
        if (movementBeatCount < Data.Speed)
        {
            return;
        }
        movementBeatCount = 0;

        Vector2Int currentPosition = Parameters.GridPosition;
        Vector2Int desiredPosition = currentPosition + GameManager.Instance.PathFinding.GetMoveToPlayerPolicy(currentPosition);
        Collider2D collider = Physics2D.OverlapBox(
            GameManager.Instance.LayoutPosToPosition(desiredPosition),
            GameManager.Instance.LevelManager.Grid.cellSize * 0.9f, 0f,
            GameManager.Instance.DamageManager.EverythingLayerMask);
        if (collider != null && collider.gameObject != gameObject)
        {
            return;
        }

        SetDesiredPosition(desiredPosition);
    }
}