using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public Bomb BombPrototype;

    private Transform bombSpawner;

    private List<Bomb> allBombs = new();

    public void SpawnBomb(Vector2Int pos, int power, int damage = 1)
    {
        if (bombSpawner == null)
        {
            bombSpawner = new GameObject("BombSpawner").transform;
        }

        Bomb bomb = Instantiate(BombPrototype, bombSpawner).GetComponent<Bomb>();
        bomb.Power = power;
        bomb.Damage = damage;
        bomb.GridPosition = pos;

        bomb.transform.position = GameManager.Instance.LayoutPosToPosition(pos);

        allBombs.Add(bomb);
    }

    public void Detonate()
    {
        foreach (var bomb in allBombs)
        {
            bomb.Explode();
        }
        allBombs.Clear();
    }
}
