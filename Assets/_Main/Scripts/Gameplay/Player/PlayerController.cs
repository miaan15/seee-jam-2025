using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerParameters parameters;
    private PlayerData data;
    public GameInput input;

    private Vector2Int desiredMoveToPos;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        parameters = manager.Parameters;
        data = manager.Data;
        input = manager.Input;
    }

    private void Start()
    {
        desiredMoveToPos = GameManager.Instance.LevelManager.PlayerStartPos;
        MoveToDesiredPos();

        GameManager.Instance.AddOnBeatCallback(MoveToDesiredPos);
    }

    private void Update()
    {
        parameters.MoveDirectionInput = input.Player.Move.ReadValue<Vector2>();
        parameters.DesiredMoveDirection = Vector2Int.zero;
        if (Mathf.Abs(parameters.MoveDirectionInput.x) > 0)
        {
            parameters.DesiredMoveDirection.x = (int)Mathf.Sign(parameters.MoveDirectionInput.x);
        }
        else if (Mathf.Abs(parameters.MoveDirectionInput.y) > 0)
        {
            parameters.DesiredMoveDirection.y = (int)Mathf.Sign(parameters.MoveDirectionInput.y);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameManager.Instance.BombManager.SpawnBomb(parameters.GridPosition, 4);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.Instance.BombManager.Detonate();
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.AcceptInput && parameters.DesiredMoveDirection != Vector2Int.zero)
        {
            desiredMoveToPos = parameters.GridPosition + parameters.DesiredMoveDirection;
            if (desiredMoveToPos.x < 0 || desiredMoveToPos.x >= GameManager.Instance.LevelLayout.Width
                    || desiredMoveToPos.y < 0 || desiredMoveToPos.y >= GameManager.Instance.LevelLayout.Height
                    || GameManager.Instance.LevelLayout.GetFlag(desiredMoveToPos) == LevelLayoutFlag.Wall
                    || GameManager.Instance.LevelLayout.GetFlag(desiredMoveToPos) == LevelLayoutFlag.Enemy)
            {
                desiredMoveToPos = parameters.GridPosition;
            }
        }
    }

    private void MoveToDesiredPos()
    {
        transform.position = GameManager.Instance.LayoutPosToPosition(desiredMoveToPos);
        parameters.GridPosition = desiredMoveToPos;
    }
}
