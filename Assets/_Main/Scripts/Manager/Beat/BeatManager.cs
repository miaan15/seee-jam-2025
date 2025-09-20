using System;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float Interval;
    public float EarlyAcceptInputOffset;
    public float LateAcceptInputOffset;

    private TimeStamp nextBeatTimeStamp = new();

    public bool AcceptInput = false;
    private TimeStamp acceptInputTimeStamp = new();
    private TimeStamp denyInputTimeStamp = new();

    public List<Action> OnBeatCallbacks = new();
    public List<Action> OnPrePlayedBeatCallbacks = new();

    private bool played = false;

    private void Start()
    {
        nextBeatTimeStamp.Set(Interval);

        acceptInputTimeStamp.Set(Interval - EarlyAcceptInputOffset);
        denyInputTimeStamp.Set(Interval + LateAcceptInputOffset);
    }

    public void Pause()
    {
        played = false;
    }

    public void Play()
    {
        played = true;
    }

    public void AddOnBeatCallback(Action callback)
    {
        OnBeatCallbacks.Add(callback);
    }

    public void RemoveOnBeatCallback(Action callback)
    {
        OnBeatCallbacks.Remove(callback);
    }

    public void AddOnPrePlayedBeatCallback(Action callback)
    {
        OnPrePlayedBeatCallbacks.Add(callback);
    }

    public void RemoveOnPrePlayedBeatCallback(Action callback)
    {
        OnPrePlayedBeatCallbacks.Remove(callback);
    }

    private void FixedUpdate()
    {
        if (acceptInputTimeStamp.Reached())
        {
            AcceptInput = true;

            acceptInputTimeStamp.Set(Interval);
        }
        if (denyInputTimeStamp.Reached())
        {
            AcceptInput = false;

            denyInputTimeStamp.Set(Interval);
        }

        if (nextBeatTimeStamp.Reached())
        {
            nextBeatTimeStamp.Set(Interval);

            if (played)
            {
                for (int i = OnBeatCallbacks.Count - 1; i >= 0; i--)
                {
                    var cb = OnBeatCallbacks[i];
                    if (cb == null)
                    {
                        OnBeatCallbacks.RemoveAt(i);
                    }
                    else
                    {
                        cb.Invoke();
                    }
                }
            }

            for (int i = OnPrePlayedBeatCallbacks.Count - 1; i >= 0; i--)
            {
                var cb = OnPrePlayedBeatCallbacks[i];
                if (cb == null)
                {
                    OnPrePlayedBeatCallbacks.RemoveAt(i);
                }
                else
                {
                    cb.Invoke();
                }
            }
        }
    }
}
