using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerParameters parameters;
    private PlayerData data;
    public GameInput input;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        parameters = manager.Parameters;
        data = manager.Data;
        input = manager.Input;
    }

    private void Start()
    {
        MoveTo(GameManager.Instance.LevelManager.PlayerStartPos);
    }

    private void Update()
    {
        parameters.MoveDirectionInput = input.Player.Move.ReadValue<Vector2>();
        parameters.MoveDirection = Vector2Int.zero;
        if (Mathf.Abs(parameters.MoveDirectionInput.x) > 0)
        {
            parameters.MoveDirection.x = (int)Mathf.Sign(parameters.MoveDirectionInput.x);
        }
        else if (Mathf.Abs(parameters.MoveDirectionInput.y) > 0)
        {
            parameters.MoveDirection.y = (int)Mathf.Sign(parameters.MoveDirectionInput.y);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            MoveTo(parameters.GridPosition + parameters.MoveDirection);
        }
    }

    private void MoveTo(Vector2Int pos)
    {
        transform.position = GameManager.Instance.LayoutPosToPosition(pos);
        parameters.GridPosition = pos;
    }
}
