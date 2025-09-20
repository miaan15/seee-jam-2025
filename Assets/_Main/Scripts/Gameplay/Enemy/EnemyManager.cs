using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public abstract class EnemyManager : MonoBehaviour
{
    [Header("References")]
    public Animator Animator;

    [HideInInspector]
    public Transform SpriteTransform;

    [Header("Parameters")]
    [SerializeField]
    public EnemyParameters Parameters;

    public EnemyStats Stats { get; private set; }

    protected Vector2Int desiredMoveToPos;

    private void Awake()
    {
        Parameters = new EnemyParameters();

        Stats = GetComponent<EnemyStats>();

        SpriteTransform = transform.GetChild(0);

        OnAwake();
    }

    private void Start()
    {
        GameManager.Instance.AddOnBeatCallback(MoveToDesiredPos);
        GameManager.Instance.AddOnBeatCallback(OnBeat);

        OnStart();

        desiredMoveToPos = Parameters.GridPosition;
    }

    private void MoveToDesiredPos()
    {
        Parameters.GridPosition = desiredMoveToPos;

        if (!finishedMoveToPosAnimation)
        {
            StopCoroutine(moveToPosAnimationCoroutine);
        }
        moveToPosAnimationCoroutine = MoveToPosAnimationCoroutine(GameManager.Instance.LayoutPosToPosition(desiredMoveToPos));
        StartCoroutine(moveToPosAnimationCoroutine);
    }

    private bool finishedMoveToPosAnimation = true;
    private IEnumerator moveToPosAnimationCoroutine;
    private const float maxTime = 0.5f;
    private IEnumerator MoveToPosAnimationCoroutine(Vector2 target)
    {
        float time = 0f;
        while (Vector2.Distance((Vector2)transform.position, target) > 0.01f && time < maxTime)
        {
            finishedMoveToPosAnimation = false;
            transform.position = Vector2.MoveTowards(transform.position, target, GameManager.Instance.Player.Data.MoveSpeed * Time.fixedDeltaTime);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = target;
        finishedMoveToPosAnimation = true;
    }

    protected abstract void OnAwake();
    protected abstract void OnStart();

    protected abstract void OnBeat();
}
