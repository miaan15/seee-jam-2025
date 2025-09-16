using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    public Vector2Int[] GetPathToMove(Vector2Int from, Vector2Int to, bool ignoreEntity = true)
    {
        var layout = GameManager.Instance.LevelLayout;

        Queue<Vector2Int> queue = new();
        HashSet<Vector2Int> visited = new();
        Dictionary<Vector2Int, Vector2Int> pre = new();
        bool able = false;

        queue.Enqueue(from);
        while (queue.Count > 0)
        {
            Vector2Int cur = queue.Peek();
            queue.Dequeue();

            if (cur == to)
            {
                able = true;
                break;
            }

            visited.Add(cur);

            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, -1, 0, 1 };

            for (int i = 0; i < 4; i++)
            {
                Vector2Int next = cur + new Vector2Int(dx[i], dy[i]);
                if (next.x < 0 || next.x >= layout.Width || next.y < 0 || next.y >= layout.Height) continue;
                if (visited.Contains(next)) continue;
                if (layout.GetFlag(next) == LevelLayoutFlag.Wall) continue;
                if (!ignoreEntity)
                {
                    if (layout.GetFlag(next) == LevelLayoutFlag.Enemy || layout.GetFlag(next) == LevelLayoutFlag.Player)
                        continue;
                }

                queue.Enqueue(next);
                pre.TryAdd(next, cur);
            }
        }

        if (!able)
        {
            Debug.Log("cant get to " + to);
            return new Vector2Int[] { };
        }

        List<Vector2Int> res = new();
        Vector2Int p = to;
        while (p != from)
        {
            var k = pre[p];
            res.Add(p - k);
            p = k;
        }
        res.Reverse();
        return res.ToArray();
    }
}
