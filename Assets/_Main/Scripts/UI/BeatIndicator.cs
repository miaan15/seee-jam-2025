using System;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{
    public Action Callback;

    private RectTransform rectTransform;

    private float delta;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        delta = rectTransform.sizeDelta.x / (GameManager.Instance.BeatManager.Interval * 2f);
    }

    private void FixedUpdate()
    {
        rectTransform.sizeDelta = new(Mathf.MoveTowards(rectTransform.sizeDelta.x, -20f, delta * Time.deltaTime), rectTransform.sizeDelta.y);
        if (rectTransform.sizeDelta.x == -20f)
        {
            Callback?.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }
    }
}
