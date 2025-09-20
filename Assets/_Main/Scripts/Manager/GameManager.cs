using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(BeatManager))]
[RequireComponent(typeof(DamageManager))]
[RequireComponent(typeof(BombManager))]
[RequireComponent(typeof(PathFindingManager))]
[RequireComponent(typeof(EnemyWaveManager))]
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
    public EnemyWaveManager EnemyWaveManager { get; private set; }
    public SoundManager SoundManager { get; private set; }

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
    public void RemoveOnBeatCallback(Action callback) => BeatManager.RemoveOnBeatCallback(callback);

    public void DealDamage(Vector2Int pos, int amount, DamageType type) => DamageManager.DealDamage(pos, amount, type);

    public GameObject TestSprite;
    public Animator LevelCoverAnimator;

    private void Awake()
    {
        LevelManager = GetComponent<LevelManager>();
        BeatManager = GetComponent<BeatManager>();
        DamageManager = GetComponent<DamageManager>();
        PathFinding = GetComponent<PathFindingManager>();
        BombManager = GetComponent<BombManager>();
        EnemyWaveManager = GetComponent<EnemyWaveManager>();
        SoundManager = GetComponent<SoundManager>();

        Player = FindFirstObjectByType<PlayerManager>();
    }

    private void Start()
    {
        LoadLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    private IEnumerator LoadLevelCoroutine()
    {
        LevelCoverAnimator.SetTrigger("Start");

        BeatManager.Pause();

        yield return new WaitForSeconds(.5f);
        // =============================================================================

        if (LevelManager.Grid.transform.childCount > 0)
        {
            Destroy(LevelManager.Grid.transform.GetChild(0).gameObject);
            yield return new WaitForEndOfFrame();
        }

        if (LevelManager.Grid.transform.childCount > 0)
        {
            Destroy(LevelManager.Grid.transform.GetChild(0).gameObject);
            yield return new WaitForEndOfFrame();
        }

        LevelManager.LoadLevel();

        yield return new WaitForEndOfFrame();

        LevelCoverAnimator.SetTrigger("End");
        BeatIndicatorManager.Instance.StartIndicators();

    }
}
