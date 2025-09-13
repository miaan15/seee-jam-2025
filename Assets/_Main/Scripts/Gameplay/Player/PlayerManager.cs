using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerManager : MonoBehaviour
{
    #region References
    [Header("References")]
    public Rigidbody2D Body;

    [HideInInspector]
    public GameInput Input;
    #endregion

    [Header("Data")]
    public PlayerData Data;
    public PlayerParameters Parameters { get; private set; }

    public PlayerController PlayerController { get; private set; }

    private void Awake()
    {
        Input = new GameInput();
        Input.Player.Enable();

        Parameters = new PlayerParameters();
        
        PlayerController = GetComponent<PlayerController>();
    }
}
