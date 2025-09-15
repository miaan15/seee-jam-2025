using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(WallManager))]
public class LevelManager : MonoBehaviour
{
    public enum LevelLayoutFlag
    {
        None = 0,
        Player,
        Enemy,
        Wall,
    }

    private WallManager wallManager;

    [Header("References")]
    public Grid Grid;

    [Header("Data")]
    public int Width;
    public int Height;

    private List<LevelLayoutFlag> flags = new();
    public LevelLayoutFlag GetFlag(int x, int y)
    {
        return flags[y * Width + x];
    }
    public void SetFlag(int x, int y, LevelLayoutFlag flag)
    {
        flags[y * Width + x] = flag;
    }

    private Tilemap wallInstance;

    private void Awake()
    {
        wallManager = GetComponent<WallManager>();
    }

    private void Start()
    {
        Make();
    }

    public void Make()
    {
        flags = new(Width * Height);
        flags.AddRange(Enumerable.Repeat(LevelLayoutFlag.None, Width * Height));
        flags.TrimExcess();

        HandleMakeWall();

        DebugLogFlags();
    }

    private void HandleMakeWall()
    {
        wallInstance = Instantiate(wallManager.Layout, Grid.transform);

        wallInstance.CompressBounds();
        BoundsInt bounds = wallInstance.cellBounds;
        Vector3Int origin = bounds.min;

        int maxX = Mathf.Min(Width, bounds.size.x);
        int maxY = Mathf.Min(Height, bounds.size.y);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var cellPos = new Vector3Int(origin.x + x, origin.y + y, 0);

                if (wallInstance.HasTile(cellPos))
                {
                    SetFlag(x, y, LevelLayoutFlag.Wall);
                }
            }
        }
    }

    private void DebugLogFlags()
    {
        if (flags == null || flags.Count != Width * Height)
        {
            Debug.LogWarning("Flags list is not properly initialized.");
            return;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                int index = y * Width + x;
                char c = flags[index] switch
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
