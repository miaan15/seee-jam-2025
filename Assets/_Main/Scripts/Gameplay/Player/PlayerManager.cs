using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region References
    [Header("References")]
    public Rigidbody2D Body;
    public Animator Animator;

    [HideInInspector]
    public GameInput Input;

    [HideInInspector]
    public Transform SpriteTransform;
    #endregion

    [Header("Data")]
    public PlayerData Data;

    public PlayerController PlayerController { get; private set; }

    [Header("Parameters")]
    [SerializeField]
    public PlayerParameters Parameters;

    private void Awake()
    {
        Input = new GameInput();
        Input.Player.Enable();

        Parameters = new PlayerParameters();

        PlayerController = GetComponent<PlayerController>();

        SpriteTransform = transform.GetChild(0);
    }
}
