using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelData : MonoBehaviour
{
    public Tilemap wallMap;
    public Tilemap breakableMap;
    public Tilemap enemyMap;

    public RuleTile wallTile;
    public Breakable breakableObject;

    [Space(10)]
    public int WallBoundThickness;
    public Vector2Int ExtraSpace;

    [Space(10)]
    public Vector2Int PlayerStartPos;

    [HideInInspector]
    public LevelLayout Layout = null;
    public List<int> enemyIDs;

    public LevelLayout GetLayout()
    {
        if (Layout != null) return Layout;

        wallMap.CompressBounds();
        breakableMap.CompressBounds();
        enemyMap.CompressBounds();
        BoundsInt wallMapBound = wallMap.cellBounds
            , breakableMapBound = breakableMap.cellBounds
            , enemyMapBound = enemyMap.cellBounds;
        BoundsInt maxBound = wallMapBound;
        maxBound.SetMinMax(
            Vector3Int.Min(maxBound.min, Vector3Int.Min(breakableMapBound.min, enemyMapBound.min)),
            Vector3Int.Max(maxBound.max, Vector3Int.Max(breakableMapBound.max, enemyMapBound.max))
        );

        int w = maxBound.max.x + ExtraSpace.x;
        int h = maxBound.max.y + ExtraSpace.y;
        Layout = new(w, h);
        enemyIDs = new(w * h);
        enemyIDs.AddRange(Enumerable.Repeat(-1, w * h));
        enemyIDs.TrimExcess();

        for (int y = 0; y < maxBound.max.y; y++)
        {
            for (int x = 0; x < maxBound.max.x; x++)
            {
                Vector3Int pos = new(x, y);
                if (wallMap.HasTile(pos))
                {
                    Layout.SetFlag(x, y, LevelLayoutFlag.Wall);
                }
                else if (breakableMap.HasTile(pos))
                {
                    Layout.SetFlag(x, y, LevelLayoutFlag.Breakable);
                }

                if (enemyMap.HasTile(pos))
                {
                    var enemyTiles = GameManager.Instance.EnemyWaveManager.EnemySpawnTiles;
                    var tileBase = enemyMap.GetTile(pos);
                    var enemyTilesList = enemyTiles.ToList();
                    Tile tile = tileBase as Tile;
                    if (tile != null && enemyTilesList.Contains(tile))
                    {
                        int id = enemyTilesList.IndexOf(tile);
                        enemyIDs[y * w + x] = id;
                    }
                }
            }
        }

        return Layout;
    }
}