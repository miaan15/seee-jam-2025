using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyWaveManager : MonoBehaviour
{
    public EnemyManager[] EnemyPrototypes;
    public Tile[] EnemySpawnTiles;

    public List<EnemyManager> Enemies { get; private set; } = new();

    public EnemyManager SpawnEnemy(Vector2Int pos, int type)
    {
        if (type < 0 || type >= EnemyPrototypes.Length)
        {
            Debug.LogError($"Enemy type {type} is out of range.");
            return null;
        }

        var enemy = Instantiate(EnemyPrototypes[type], transform);
        enemy.transform.position = GameManager.Instance.LayoutPosToPosition(pos);
        enemy.name = $"Enemy_{type}";

        Enemies.Add(enemy);

        return enemy;
    }

    public void RemoveEnemy(EnemyManager enemy)
    {
        enemy.ForceStopAllActivities();
        Destroy(enemy.gameObject);
        Enemies.Remove(enemy);
    }

    public void RemoveAllEnemies()
    {
        foreach (var enemy in Enemies)
        {
            enemy.ForceStopAllActivities();
            Destroy(enemy.gameObject);
        }
        Enemies.Clear();
    }
}