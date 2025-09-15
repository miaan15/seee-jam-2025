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

    public LevelLayout LevelLayout => LevelManager.Layout;
    public Vector2 LayoutPosToPosition(Vector2Int pos)
    {
        Vector2 cellSize = LevelManager.Grid.cellSize;
        return new Vector2(pos.x * cellSize.x, pos.y * cellSize.y) + cellSize * 0.5f;
    }

    [Space(10)]
    public PlayerManager Player;
}
