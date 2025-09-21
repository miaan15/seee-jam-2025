using UnityEngine;

public class ZombieEnemyManager : EnemyManager
{
    public ZombieEnemyData Data;
    private int movementBeatCount = 0;
    private int recoveryBeatCount = 0;
    private bool hasHitPlayer = false;

    protected override void OnStart()
    {
        Stats.Health = Data.Health;
        Parameters.GridPosition = GameManager.Instance.PositionToLayoutPos(transform.position);
    }

    protected override void OnBeat()
    {
        base.OnBeat();
        UpdateDesiredPosition();
        DamagePlayerOnContact();
    }

    public void UpdateDesiredPosition()
    {
        movementBeatCount++;
        if (movementBeatCount < Data.MovementSpeed)
        {
            return;
        }
        movementBeatCount = 0;

        Vector2Int currentPosition = Parameters.GridPosition;
        Vector2Int policy = GameManager.Instance.PathFinding.GetMoveToPlayerPolicy(currentPosition);

        Collider2D collider;

        if (policy == Vector2Int.zero)
        {
            Vector2Int[] directions = new Vector2Int[]
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };
            int range = Random.Range(0, directions.Length);

            Vector2Int _desiredPosition = currentPosition + directions[range];
            while (GameManager.Instance.LevelLayout.GetFlag(_desiredPosition) == LevelLayoutFlag.Wall)
            {
                range = (range + 1) % directions.Length;
                _desiredPosition = currentPosition + directions[range];
            }

            collider = Physics2D.OverlapBox(
                GameManager.Instance.LayoutPosToPosition(_desiredPosition),
                GameManager.Instance.LevelManager.Grid.cellSize * 0.9f, 0f,
                GameManager.Instance.DamageManager.EverythingLayerMask);
            if (collider != null && collider.gameObject != gameObject)
            {
                return;
            }
            SetDesiredPosition(_desiredPosition);
            return;
        }

        Vector2Int desiredPosition = currentPosition + policy;
        collider = Physics2D.OverlapBox(
            GameManager.Instance.LayoutPosToPosition(desiredPosition),
            GameManager.Instance.LevelManager.Grid.cellSize * 0.9f, 0f,
            GameManager.Instance.DamageManager.EnemyOnlyLayerMask);
        if (collider != null && collider.gameObject != gameObject)
        {
            return;
        }
        SetDesiredPosition(desiredPosition);
    }

    private void DamagePlayerOnContact()
    {
        Vector2Int currentPosition = Parameters.GridPosition;
        Collider2D collider = Physics2D.OverlapBox(
            GameManager.Instance.LayoutPosToPosition(currentPosition),
            GameManager.Instance.LevelManager.Grid.cellSize * 0.9f, 0f,
            GameManager.Instance.DamageManager.PlayerOnlyLayerMask);
        if (collider == null || collider.gameObject != GameManager.Instance.Player.gameObject)
        {
            recoveryBeatCount = 0;
            return;
        }

        if (hasHitPlayer)
        {
            recoveryBeatCount++;
            if (recoveryBeatCount < Data.RecoverySpeed)
            {
                return;
            }
            hasHitPlayer = false;
            recoveryBeatCount = 0;
        }

        Debug.Log($"Zombie hit player for {Data.Damage} damage.");
        GameManager.Instance.Player.PlayerStats.TakeDamage(Data.Damage);
        hasHitPlayer = true;
    }
}