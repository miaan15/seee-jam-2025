using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    public Animator Animator;

    [HideInInspector]
    public Transform SpriteTransform;

    [Header("Data")]
    public EnemyData Data;

    [Header("Parameters")]
    [SerializeField]
    public EnemyParameters Parameters;

    public EnemyStats EnemyStats { get; private set; }

    private void Awake()
    {
        Parameters = new EnemyParameters();

        EnemyStats = GetComponent<EnemyStats>();

        SpriteTransform = transform.GetChild(0);
    }

    Vector2Int[] path;
    int pp;
    private void Start()
    {
        Parameters.GridPosition = GameManager.Instance.PositionToLayoutPos(transform.position);

        path = GameManager.Instance.PathFinding.GetPathToMove(Parameters.GridPosition, GameManager.Instance.LevelManager.PlayerStartPos);
        pp = 0;

        GameManager.Instance.AddOnBeatCallback(Movee);
    }
    private void Movee()
    {
        if (pp >= path.Length) return;

        var desiredMoveToPos = Parameters.GridPosition + path[pp];
        pp++;

        transform.position = GameManager.Instance.LayoutPosToPosition(desiredMoveToPos);
        Parameters.GridPosition = desiredMoveToPos;
    }
}
