using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Power;
    public int Damage;

    public Vector2Int GridPosition;

    private bool lockUpPower = true;

    private void Start()
    {
        StartCoroutine(UnlockUpPowerCoroutine());
        GameManager.Instance.AddOnBeatCallback(() =>
        {
            if (!lockUpPower && Power < 4) Power++;
        });
    }

    private IEnumerator UnlockUpPowerCoroutine()
    {
        yield return new WaitForFixedUpdate();
        lockUpPower = false;
    }

    public void Explode()
    {
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, -1, 0, 1 };
        bool[] locked = { false, false, false, false };

        GameManager.Instance.DealDamage(GridPosition, Damage, DamageType.Everything);
        for (int k = 1; k <= Power; k++)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2Int target = GridPosition + new Vector2Int(dx[i], dy[i]) * k;
                if (GameManager.Instance.LevelLayout.GetFlag(target) == LevelLayoutFlag.Wall)
                    locked[i] = true;

                if (locked[i]) continue;

                GameManager.Instance.DealDamage(target, Damage, DamageType.Everything);

                // FIXME
                Destroy(Instantiate(
                    GameManager.Instance.TestSprite, GameManager.Instance.LayoutPosToPosition(target), Quaternion.identity),
                    .1f
                );
            }
        }

        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
