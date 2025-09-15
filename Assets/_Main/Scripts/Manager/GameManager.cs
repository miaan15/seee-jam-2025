using System;
using UnityEngine;

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

    public LevelManager LevelManager;

    public BeatManager BeatManager;

    public LevelLayout LevelLayout => LevelManager.Layout;
    public Vector2 LayoutPosToPosition(Vector2Int pos)
    {
        Vector2 cellSize = LevelManager.Grid.cellSize;
        return new Vector2(pos.x * cellSize.x, pos.y * cellSize.y) + cellSize * 0.5f;
    }

    public bool AcceptInput => BeatManager.AcceptInput;
    public void AddOnBeatCallback(Action callback)
    {
        BeatManager.AddOnBeatCallback(callback);
    }

    [Space(10)]
    public PlayerManager Player;

    private void Start()
    {
        BeatManager.Play();
    }
}
