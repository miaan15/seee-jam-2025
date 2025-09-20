using UnityEngine;

public class BeatIndicatorManager : MonoBehaviour
{
    private static BeatIndicatorManager instance;
    public static BeatIndicatorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<BeatIndicatorManager>();

                if (instance == null)
                {
                    Debug.LogError("NO BeatIndicatorManager !? YET !?");
                }
            }

            return instance;
        }
    }

    public BeatIndicator Prototype;

    public bool started = false;
    public int skip = 1;

    private bool bgmNext = true;

    public void StartIndicators()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        started = false;
        skip = 2;
        bgmNext = true;
    }

    private void Start()
    {
        GameManager.Instance.BeatManager.AddOnPrePlayedBeatCallback(() =>
        {
            if (bgmNext)
            {
                GameManager.Instance.SoundManager.PlayBGM("bgm");
                bgmNext = false;
            }

            if (skip > 0)
            {
                skip--;
                return;
            }

            if (!started)
            {
                BeatIndicator p = Instantiate(Prototype, transform).GetComponent<BeatIndicator>();
                p.Callback = HereTheStoryStarted;
                p.GetComponent<RectTransform>().sizeDelta = new Vector2(p.GetComponent<RectTransform>().sizeDelta.x, 0f);
                started = true;
            }
            else
            {
                Instantiate(Prototype, transform);
            }
        });
    }

    private void HereTheStoryStarted()
    {
        GameManager.Instance.BeatManager.Play();
    }
}
