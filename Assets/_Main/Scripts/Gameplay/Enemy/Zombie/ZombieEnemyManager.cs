using UnityEngine;

public class ZombieEnemyManager : EnemyManager
{
    public ZombieEnemyData Data;

    private int beatCount = 0;

    protected override void OnAwake()
    {

    }

    protected override void OnStart()
    {
        Stats.Health = Data.Health;
        Parameters.GridPosition = GameManager.Instance.PositionToLayoutPos(transform.position);
    }

    protected override void OnBeat()
    {
        beatCount++;

        if (beatCount >= Data.Speed)
        {
            beatCount = 0;

            desiredMoveToPos = Parameters.GridPosition + GameManager.Instance.PathFinding.GetMoveToPlayerPolicy(Parameters.GridPosition);
            GameManager.Instance.LayoutPosToPosition(Parameters.GridPosition);
        }
    }
}