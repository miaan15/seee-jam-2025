using System;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(BeatManager))]
[RequireComponent(typeof(DamageManager))]
[RequireComponent(typeof(BombManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<GameManager>();

                if (instance == null)
                {
                    Debug.LogError("NO GAME MANAGER !? YET !?");
                }
            }

            return instance;
        }
    }

    public LevelManager LevelManager { get; private set; }
    public BeatManager BeatManager { get; private set; }
    public DamageManager DamageManager { get; private set; }
    public PathFindingManager PathFinding { get; private set; }
    public BombManager BombManager { get; private set; }

    public PlayerManager Player { get; private set; }

    public LevelLayout LevelLayout => LevelManager.Layout;
    public Vector2 LayoutPosToPosition(Vector2Int pos)
    {
        Vector2 cellSize = LevelManager.Grid.cellSize;
        return new Vector2(
            pos.x * cellSize.x,
            pos.y * cellSize.y
        ) + cellSize * 0.5f;
    }
    public Vector2Int PositionToLayoutPos(Vector2 pos)
    {
        Vector2 cellSize = LevelManager.Grid.cellSize;
        return new Vector2Int(
            (int)(pos.x / cellSize.x),
            (int)(pos.y / cellSize.y)
        );
    }

    public bool AcceptInput => BeatManager.AcceptInput;
    public void AddOnBeatCallback(Action callback) => BeatManager.AddOnBeatCallback(callback);

    public void DealDamage(Vector2Int pos, int amount, DamageType type) => DamageManager.DealDamage(pos, amount, type);

    public Vector2Int[] GetPathToMove(Vector2Int from, Vector2Int to, bool ignoreEntity = true)
        => PathFinding.GetPathToMove(from, to, ignoreEntity);

    public GameObject TestSprite;

    private void Awake()
    {
        LevelManager = GetComponent<LevelManager>();
        BeatManager = GetComponent<BeatManager>();
        DamageManager = GetComponent<DamageManager>();
        PathFinding = GetComponent<PathFindingManager>();
        BombManager = GetComponent<BombManager>();

        Player = FindFirstObjectByType<PlayerManager>();
    }
}
