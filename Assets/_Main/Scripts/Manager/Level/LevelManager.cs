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
    public Camera MainCamera;

    [Header("Data")]
    public int Width;
    public int Height;
    public float CamSize;

    public LevelData LevelData;

    [Space(10)]
    public Vector2Int PlayerStartPos;

    private LevelLayout layout;
    public LevelLayout Layout => layout;

    public void LoadLevel()
    {
        layout = LevelData.GetLayout();

        MainCamera.orthographicSize = CamSize;

        var levelObj = Instantiate(LevelData, Grid.transform).GetComponent<LevelData>();

        levelObj.wallMap.gameObject.SetActive(false);
        levelObj.breakableMap.gameObject.SetActive(false);
        levelObj.enemyMap.gameObject.SetActive(false);

        var tilemap = new GameObject("Tilemap", typeof(Tilemap), typeof(TilemapRenderer)).GetComponent<Tilemap>();
        tilemap.transform.SetParent(Grid.transform);
        tilemap.color = new Color(130f / 255f, 130f / 255f, 0f, 1f);

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

        MainCamera.transform.position = new Vector3(
            (Layout.Width * Grid.cellSize.x) / 2f,
            (Layout.Height * Grid.cellSize.y) / 2f - CamSize * .16f,
            MainCamera.transform.position.z
        );
    }
}
