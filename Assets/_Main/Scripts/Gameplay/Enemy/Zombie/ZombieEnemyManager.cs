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
        Vector2Int policy = GameManager.Instance.PathFinding.GetMoveToPlayerPolicy(currentPosition);

        if (policy == Vector2Int.zero)
        {
            Vector2Int[] directions = new Vector2Int[]
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };
            int rng = Random.Range(0, directions.Length);

            Vector2Int _desiredPosition = currentPosition + directions[rng];
            while (GameManager.Instance.LevelLayout.GetFlag(_desiredPosition) == LevelLayoutFlag.Wall)
            {
                rng = (rng + 1) % directions.Length;
                _desiredPosition = currentPosition + directions[rng];
            }

            Collider2D _collider = Physics2D.OverlapBox(
            GameManager.Instance.LayoutPosToPosition(_desiredPosition),
            GameManager.Instance.LevelManager.Grid.cellSize * 0.9f, 0f,
            GameManager.Instance.DamageManager.EverythingLayerMask);
            if (_collider != null && _collider.gameObject != gameObject)
            {
                return;
            }
            SetDesiredPosition(_desiredPosition);
            return;
        }

        Vector2Int desiredPosition = currentPosition + policy;
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