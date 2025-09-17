using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout
{
    public int Width;
    public int Height;

    public LevelLayout(int width, int height)
    {
        Width = width;
        Height = height;

        flags = new(Width * Height);
        flags.AddRange(Enumerable.Repeat(LevelLayoutFlag.None, Width * Height));
        flags.TrimExcess();
    }

    public void Reset(int width, int height)
    {
        Width = width;
        Height = height;

        flags = new(Width * Height);
        flags.AddRange(Enumerable.Repeat(LevelLayoutFlag.None, Width * Height));
        flags.TrimExcess();
    }

    private List<LevelLayoutFlag> flags = new();
    public LevelLayoutFlag GetFlag(int x, int y)
    {
        if (x < 0 || y < 0) return LevelLayoutFlag.Wall;
        if (x >= Width || y >= Height) return LevelLayoutFlag.Wall;
        return flags[y * Width + x];
    }
    public LevelLayoutFlag GetFlag(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0) return LevelLayoutFlag.Wall;
        if (pos.x >= Width || pos.y >= Height) return LevelLayoutFlag.Wall;
        return flags[pos.y * Width + pos.x];
    }
    public void SetFlag(int x, int y, LevelLayoutFlag flag)
    {
        if (x < 0 || y < 0) return;
        if (x >= Width || y >= Height) return;
        flags[y * Width + x] = flag;
    }
    public void SetFlag(Vector2Int pos, LevelLayoutFlag flag)
    {
        if (pos.x < 0 || pos.y < 0) return;
        if (pos.x >= Width || pos.y >= Height) return;
        flags[pos.y * Width + pos.x] = flag;
    }
}
