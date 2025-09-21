using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerParameters parameters;
    private PlayerData data;
    public GameInput input;

    private TimeStamp lockInputTimeStamp = new();

    private Vector2Int desiredMoveToPos;

    public int Length = 8;

    public bool[] BombBeat;
    public bool[] DetoBeat;

    public int CurrentBeat = 0;


    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        parameters = manager.Parameters;
        data = manager.Data;
        input = manager.Input;

        BombBeat = new bool[Length];
        DetoBeat = new bool[Length];
        DetoBeat[7] = true;
    }

    private void Start()
    {
        desiredMoveToPos = GameManager.Instance.LevelManager.PlayerStartPos;
        MoveToDesiredPos();

        GameManager.Instance.AddOnBeatCallback(OnBeat);
    }

    private void Update()
    {
        parameters.MoveDirectionInput = input.Player.Move.ReadValue<Vector2>();
        if (!input.Player.Move.WasPerformedThisFrame()) parameters.MoveDirectionInput = Vector2.zero;
        if (parameters.MoveDirectionInput.magnitude > 0.1f)
        {
            Debug.Log(GameManager.Instance.BeatManager.GetDebugTime());
        }

        parameters.DesiredMoveDirection = Vector2Int.zero;
        if (Mathf.Abs(parameters.MoveDirectionInput.x) > 0)
        {
            parameters.DesiredMoveDirection.x = (int)Mathf.Sign(parameters.MoveDirectionInput.x);
        }
        else if (Mathf.Abs(parameters.MoveDirectionInput.y) > 0)
        {
            parameters.DesiredMoveDirection.y = (int)Mathf.Sign(parameters.MoveDirectionInput.y);
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!lockInputTimeStamp.Reached() && lockInputTimeStamp.Setted())
        {
            return;
        }
        if (!GameManager.Instance.AcceptInput && parameters.DesiredMoveDirection != Vector2Int.zero)
        {
            lockInputTimeStamp.Set(GameManager.Instance.BeatManager.Interval * .95f);
            GameManager.Instance.SoundManager.PlaySFX("error");
            NerrController.Miss();
            ScreenShake.Shake(0.03f, 0.03f);
            return;
        }

        if (!GameManager.Instance.AcceptInput || parameters.DesiredMoveDirection == Vector2Int.zero)
        {
            return;
        }

        Vector2Int currentPosition = parameters.GridPosition;
        Vector2Int desiredPosition = currentPosition + parameters.DesiredMoveDirection;

        desiredMoveToPos = desiredPosition;
        if (desiredMoveToPos.x < 0 || desiredMoveToPos.x >= GameManager.Instance.LevelLayout.Width
            || desiredMoveToPos.y < 0 || desiredMoveToPos.y >= GameManager.Instance.LevelLayout.Height
            || GameManager.Instance.LevelLayout.GetFlag(desiredMoveToPos) == LevelLayoutFlag.Wall
            || GameManager.Instance.LevelLayout.GetFlag(desiredMoveToPos) == LevelLayoutFlag.Breakable)
        {
            desiredMoveToPos = currentPosition;
        }

        if (desiredMoveToPos != currentPosition)
        {
            GameManager.Instance.SoundManager.PlaySFX("move");

        }
    }

    private void OnBeat()
    {
        if (BombBeat[CurrentBeat])
        {
            GameManager.Instance.BombManager.SpawnBomb(parameters.GridPosition, 0);
            GameManager.Instance.SoundManager.PlaySFX("kick", 1);
        }
        if (DetoBeat[CurrentBeat])
        {
            GameManager.Instance.BombManager.Detonate();
            // if (GameManager.Instance.BombManager.allBombs.Count > 0)
            GameManager.Instance.SoundManager.PlaySFX("snare", 2);
            ScreenShake.Shake(0.25f, 0.25f);
        }

        ++CurrentBeat;
        if (CurrentBeat >= Length) CurrentBeat = 0;

        MoveToDesiredPos();
    }

    private void MoveToDesiredPos()
    {
        if (parameters.GridPosition != desiredMoveToPos) manager.Animator.Play("Move");
        parameters.GridPosition = desiredMoveToPos;

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
            transform.position = Vector2.MoveTowards(transform.position, target, data.MoveSpeed * Time.fixedDeltaTime);
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = target;
        finishedMoveToPosAnimation = true;
    }
}
