using UnityEngine;
using UnityEngine.Tilemaps;

public enum LevelLayoutFlag
{
    None = 0,
    Wall,
    Breakable
}

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    public Grid Grid;

    [Header("Data")]
    public int Width;
    public int Height;

    public LevelData LevelData;

    [Space(10)]
    public Vector2Int PlayerStartPos;

    private LevelLayout layout;
    public LevelLayout Layout => layout;

    private void Awake()
    {
        layout = LevelData.GetLayout();
    }

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        var levelObj = Instantiate(LevelData, Grid.transform).GetComponent<LevelData>();

        levelObj.wallMap.gameObject.SetActive(false);
        levelObj.breakableMap.gameObject.SetActive(false);
        levelObj.enemyMap.gameObject.SetActive(false);

        var tilemap = new GameObject("Tilemap", typeof(Tilemap), typeof(TilemapRenderer)).GetComponent<Tilemap>();
        tilemap.transform.SetParent(Grid.transform);

        for (int y = -LevelData.WallBoundThickness; y < Layout.Height + LevelData.WallBoundThickness; y++)
        {
            for (int x = -LevelData.WallBoundThickness; x < Layout.Width + LevelData.WallBoundThickness; x++)
            {
                if (y < 0 || y >= Layout.Height || x < 0 || x >= Layout.Width || Layout.GetFlag(x, y) == LevelLayoutFlag.Wall)
                {
                    Vector3Int pos = new(x, y, 0);
                    tilemap.SetTile(pos, LevelData.wallTile);
                    continue;
                }

                if (Layout.GetFlag(x, y) == LevelLayoutFlag.Breakable)
                {
                    var obj = Instantiate(LevelData.breakableObject, tilemap.transform).GetComponent<Breakable>();
                    obj.transform.position = GameManager.Instance.LayoutPosToPosition(new Vector2Int(x, y));
                    obj.GridPosition = new Vector2Int(x, y);
                }

                if (levelObj.enemyIDs[y * Layout.Width + x] != -1)
                {
                    int id = levelObj.enemyIDs[y * Layout.Width + x];
                    GameManager.Instance.EnemyWaveManager.SpawnEnemy(new Vector2Int(x, y), id);
                }
            }
        }
    }
}
