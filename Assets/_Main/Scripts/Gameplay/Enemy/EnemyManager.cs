using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public abstract class EnemyManager : MonoBehaviour
{
    [Header("References")]
    public Animator Animator;

    [Header("Parameters")]
    public EnemyParameters Parameters;
    public EnemyStats Stats { get; private set; }

    public Transform SpriteTransform { get; private set; }
    public Vector2Int DesiredPosition { get; private set; }

    private void Awake()
    {
        Parameters = new EnemyParameters();
        Stats = GetComponent<EnemyStats>();
        SpriteTransform = transform.GetChild(0);
        OnAwake();
    }

    private void Start()
    {
        GameManager.Instance.AddOnBeatCallback(OnBeat);
        OnStart();
        SetDesiredPosition(Parameters.GridPosition);
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnBeat()
    {
        MoveToDesiredPosition();
    }

    public void ForceStopAllActivities()
    {
        gameObject.SetActive(false);
        GameManager.Instance.RemoveOnBeatCallback(OnBeat);
        StopAllCoroutines();
    }

    protected void SetDesiredPosition(Vector2Int desiredPosition)
    {
        DesiredPosition = desiredPosition;
    }

    private void MoveToDesiredPosition()
    {
        foreach (var enemy in GameManager.Instance.EnemyWaveManager.Enemies)
        {
            if (enemy == this) continue;
            if (enemy.Parameters.GridPosition == DesiredPosition)
            {
                SetDesiredPosition(Parameters.GridPosition);
            }
        }
        Parameters.GridPosition = DesiredPosition;
        moveToPosAnimationCoroutine ??= StartCoroutine(MoveToPosAnimationCoroutine(GameManager.Instance.LayoutPosToPosition(DesiredPosition)));
    }

    private const float MAX_TIME = 0.5f;
    private Coroutine moveToPosAnimationCoroutine;
    private IEnumerator MoveToPosAnimationCoroutine(Vector2 target)
    {
        float time = 0f;
        while (Vector2.Distance((Vector2)transform.position, target) > 0.01f && time < MAX_TIME)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, GameManager.Instance.Player.Data.MoveSpeed * Time.fixedDeltaTime);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = target;
        moveToPosAnimationCoroutine = null;
    }
}