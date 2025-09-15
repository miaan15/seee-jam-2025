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
        return flags[y * Width + x];
    }
    public LevelLayoutFlag GetFlag(Vector2Int pos)
    {
        return flags[pos.y * Width + pos.x];
    }
    public void SetFlag(int x, int y, LevelLayoutFlag flag)
    {
        flags[y * Width + x] = flag;
    }
    public void SetFlag(Vector2Int pos, LevelLayoutFlag flag)
    {
        flags[pos.y * Width + pos.x] = flag;
    }
}
