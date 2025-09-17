using UnityEngine;

public class BeatIndicatorManager : MonoBehaviour
{
    public BeatIndicator Prototype;

    public bool started = false;

    private void Start()
    {
        GameManager.Instance.BeatManager.AddOnPrePlayedBeatCallback(() =>
        {
            if (!started)
            {
                BeatIndicator p = Instantiate(Prototype, transform).GetComponent<BeatIndicator>();
                p.Callback = HereTheStoryStarted;
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
