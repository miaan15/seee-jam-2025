using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap Tilemap;
    public MapContext Context;

    [Space(10)]
    [SerializeField]
    private MapData currentMapData;

    private List<int> data = new();

    private void Start()
    {
        LoadData(currentMapData);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) StartCoroutine(Make());
    }

    public void LoadData(MapData map)
    {
        currentMapData = map;

        data = new List<int>(map.Width * map.Height);
        data.AddRange(Enumerable.Repeat(-1, map.Width * map.Height));
        data.TrimExcess();

        data[4] = 0;
        data[3] = 0;
        data[1] = 0;
    }

    public IEnumerator Make()
    {
        int width = currentMapData.Width;
        int height = currentMapData.Height;

        Tilemap.ClearAllTiles();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int idx = y * width + x;
                int val = (idx < data.Count) ? data[idx] : 0;

                Vector3Int cellPos = new(x, y, 0);

                if (val >= 0)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = Context.Sprites[val];

                    Tilemap.SetTile(cellPos, tile);

                    Debug.Log(cellPos);
                }
                else
                {
                    Tilemap.SetTile(cellPos, null);
                }

                // Reduce lags
                if (idx % 64 == 0)
                {
                    yield return null;
                }
            }
        }
    }
}
