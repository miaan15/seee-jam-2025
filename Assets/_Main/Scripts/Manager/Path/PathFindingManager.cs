using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    private Vector2Int updatedPlayerPos = new(-1, -1);

    private List<Vector2Int> moveToPlayerPolicy = new();

    private const int maxbfs = 100;

    public Vector2Int GetMoveToPlayerPolicy(Vector2Int from)
    {
        var layout = GameManager.Instance.LevelLayout;

        if (from.x < 0 || from.x >= layout.Width
            || from.y < 0 || from.y >= layout.Height)
            return Vector2Int.zero;

        if (updatedPlayerPos != GameManager.Instance.Player.Parameters.GridPosition)
        {
            updatedPlayerPos = GameManager.Instance.Player.Parameters.GridPosition;

            moveToPlayerPolicy = new(layout.Width * layout.Height);
            moveToPlayerPolicy.AddRange(Enumerable.Repeat(Vector2Int.zero, layout.Width * layout.Height));
            moveToPlayerPolicy.TrimExcess();

            Queue<Vector2Int> queue = new();
            HashSet<Vector2Int> visited = new();

            queue.Enqueue(GameManager.Instance.Player.Parameters.GridPosition);

            int bfscnt = 0;
            while (queue.Count > 0 && bfscnt < maxbfs)
            {
                Vector2Int cur = queue.Peek();
                queue.Dequeue();

                visited.Add(cur);

                int[] dx = { -1, 0, 1, 0 };
                int[] dy = { 0, -1, 0, 1 };

                for (int i = 0; i < 4; i++)
                {
                    Vector2Int next = cur + new Vector2Int(dx[i], dy[i]);
                    if (next.x < 0 || next.x >= layout.Width || next.y < 0 || next.y >= layout.Height) continue;
                    if (visited.Contains(next)) continue;
                    if (layout.GetFlag(next) == LevelLayoutFlag.Wall) continue;

                    var newPolicy = new Vector2Int(-dx[i], -dy[i]);
                    if (moveToPlayerPolicy[next.y * layout.Width + next.x] != newPolicy)
                    {
                        queue.Enqueue(next);
                        moveToPlayerPolicy[next.y * layout.Width + next.x] = newPolicy;
                    }
                }

                bfscnt++;
            }
        }

        return moveToPlayerPolicy[from.y * layout.Width + from.x];
    }
}
