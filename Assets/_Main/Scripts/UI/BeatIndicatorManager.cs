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
    public int skip = 2;

    public void StartIndicators()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        started = false;
        skip = 2;
    }

    private void Start()
    {
        GameManager.Instance.BeatManager.AddOnPrePlayedBeatCallback(() =>
        {
            if (skip > 0)
            {
                skip--;
                return;
            }

            if (!started)
            {
                BeatIndicator p = Instantiate(Prototype, transform).GetComponent<BeatIndicator>();
                p.Callback = HereTheStoryStarted;
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
