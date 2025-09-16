using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerBeats))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D Body;
    public Animator Animator;

    [HideInInspector]
    public GameInput Input;

    [HideInInspector]
    public Transform SpriteTransform;

    [Header("Data")]
    public PlayerData Data;

    [Header("Parameters")]
    [SerializeField]
    public PlayerParameters Parameters;

    public PlayerController PlayerController { get; private set; }
    public PlayerBeats PlayerBeats { get; private set; }
    public PlayerStats PlayerStats { get; private set; }

    private void Awake()
    {
        Input = new GameInput();
        Input.Player.Enable();

        Parameters = new PlayerParameters();

        PlayerController = GetComponent<PlayerController>();
        PlayerBeats = GetComponent<PlayerBeats>();
        PlayerStats = GetComponent<PlayerStats>();

        SpriteTransform = transform.GetChild(0);
    }

    private void Start()
    {
    }
}
