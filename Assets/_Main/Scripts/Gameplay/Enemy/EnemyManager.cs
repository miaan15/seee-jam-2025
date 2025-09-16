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
}
