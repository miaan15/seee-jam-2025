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
        desiredMoveToPos = Parameters.GridPosition;
        GameManager.Instance.AddOnBeatCallback(MoveToDesiredPos);
        GameManager.Instance.AddOnBeatCallback(OnBeat);

        OnStart();
    }

    private void MoveToDesiredPos()
    {
        transform.position = GameManager.Instance.LayoutPosToPosition(desiredMoveToPos);
        Parameters.GridPosition = desiredMoveToPos;
    }

    protected abstract void OnAwake();
    protected abstract void OnStart();

    protected abstract void OnBeat();
}
