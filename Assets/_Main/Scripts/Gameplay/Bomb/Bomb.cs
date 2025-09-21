using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Power;
    public int Damage;
    public Color color;

    public Vector2Int GridPosition;

    private bool lockUpPower = true;

    private void Start()
    {
        StartCoroutine(UnlockUpPowerCoroutine());
        GameManager.Instance.AddOnBeatCallback(() =>
        {
            if (!lockUpPower && Power < 3)
            {
                Power++;
            }
        });
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
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
        Instantiate(
            GameManager.Instance.TestSprite, GameManager.Instance.LayoutPosToPosition(GridPosition), Quaternion.identity
        ).GetComponent<SpriteRenderer>().color = color;
        int truePower = Power;
        if (Power > 1) truePower--;
        for (int k = 1; k <= truePower; k++)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2Int target = GridPosition + new Vector2Int(dx[i], dy[i]) * k;
                if (GameManager.Instance.LevelLayout.GetFlag(target) == LevelLayoutFlag.Wall)
                    locked[i] = true;

                if (locked[i]) continue;

                GameManager.Instance.DealDamage(target, Damage, DamageType.Everything);

                Instantiate(
                    GameManager.Instance.TestSprite, GameManager.Instance.LayoutPosToPosition(target), Quaternion.identity
                ).GetComponent<SpriteRenderer>().color = color;
            }
        }

        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
