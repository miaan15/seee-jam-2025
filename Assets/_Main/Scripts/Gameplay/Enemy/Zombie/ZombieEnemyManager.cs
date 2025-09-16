using UnityEngine;

public class ZombieEnemyManager : EnemyManager
{
    public ZombieEnemyData Data;

    protected override void OnAwake()
    {

    }

    protected override void OnStart()
    {
        Stats.Health = Data.Health;
    }

    protected override void OnBeat()
    {

    }
}