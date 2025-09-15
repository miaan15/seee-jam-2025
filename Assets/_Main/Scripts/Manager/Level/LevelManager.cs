using UnityEngine;
using UnityEngine.Tilemaps;

public enum LevelLayoutFlag
{
    None = 0,
    Player,
    Enemy,
    Wall,
}

[RequireComponent(typeof(WallManager))]
public class LevelManager : MonoBehaviour
{
    private WallManager wallManager;

    [Header("References")]
    public Grid Grid;

    [Header("Data")]
    public int Width;
    public int Height;

    [Space(10)]
    public Vector2Int PlayerStartPos;

    private LevelLayout layout;
    public LevelLayout Layout => layout;

    private Tilemap wallInstance;

    private void Awake()
    {
        wallManager = GetComponent<WallManager>();
    }

    private void Start()
    {
        layout = new(Width, Height);
        Load();
    }

    public void Load()
    {
        layout.Reset(Width, Height);

        HandleMakeWall();

        DebugLogFlags();
    }

    private void HandleMakeWall()
    {
        wallInstance = Instantiate(wallManager.Layout, Grid.transform);

        wallInstance.CompressBounds();
        BoundsInt bounds = wallInstance.cellBounds;
        Vector3Int origin = bounds.min;

        wallInstance.transform.position = new Vector3(Grid.cellSize.x * -bounds.min.x, Grid.cellSize.y * -bounds.min.y);

        int maxX = Mathf.Min(Width, bounds.size.x);
        int maxY = Mathf.Min(Height, bounds.size.y);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var cellPos = new Vector3Int(origin.x + x, origin.y + y, 0);

                if (wallInstance.HasTile(cellPos))
                {
                    layout.SetFlag(x, y, LevelLayoutFlag.Wall);
                }
            }
        }
    }

    private void DebugLogFlags()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                char c = layout.GetFlag(x, y) switch
                {
                    LevelLayoutFlag.None => 'o',
                    LevelLayoutFlag.Player => 'P',
                    LevelLayoutFlag.Enemy => 'E',
                    LevelLayoutFlag.Wall => 'X',
                    _ => '?'
                };
                sb.Append(c);
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        Debug.Log(sb.ToString());
    }
}
