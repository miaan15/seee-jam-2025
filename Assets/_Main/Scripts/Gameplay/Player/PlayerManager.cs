using UnityEngine;

[RequireComponent(typeof(PlayerController))]
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

    private void Awake()
    {
        Input = new GameInput();
        Input.Player.Enable();

        Parameters = new PlayerParameters();

        PlayerController = GetComponent<PlayerController>();

        SpriteTransform = transform.GetChild(0);
    }
}
