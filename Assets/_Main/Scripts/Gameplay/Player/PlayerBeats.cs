using UnityEngine;

public class PlayerBeats : MonoBehaviour
{
    public int Length = 8;

    public bool[] BombBeat;
    public bool[] DetoBeat;
    public bool[] InvulBeat;

    public int CurrentBeat = 0;

    private void Awake()
    {
        BombBeat = new bool[Length];
        DetoBeat = new bool[Length];
        InvulBeat = new bool[Length];
    }

    private void Start()
    {
        GameManager.Instance.AddOnBeatCallback(OnBeat);
    }

    private void OnBeat()
    {
        if (BombBeat[CurrentBeat])
        {
            Debug.Log("Set bomb");
        }
        if (DetoBeat[CurrentBeat])
        {
            Debug.Log("Boom bomb");
        }
        if (InvulBeat[CurrentBeat])
        {
            Debug.Log("i am title");
        }

        ++CurrentBeat;
        if (CurrentBeat >= Length) CurrentBeat = 0;
    }
}
