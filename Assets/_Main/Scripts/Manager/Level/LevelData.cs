using NUnit.Framework;
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

    [HideInInspector]
    public LevelLayout Layout = null;

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

        Layout = new(maxBound.max.x + ExtraSpace.x, maxBound.max.y + ExtraSpace.y);

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
                else if (enemyMap.HasTile(pos))
                {
                    Layout.SetFlag(x, y, LevelLayoutFlag.Enemy);
                }
            }
        }

        return Layout;
    }
}