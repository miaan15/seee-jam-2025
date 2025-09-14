using UnityEngine;

public class TimeStamp
{
    public float time;

    public TimeStamp(float time = -1f)
    {
        this.time = time;
    }

    public void Set(float time)
    {
        this.time = Time.time + time;
    }

    public void Reset()
    {
        time = -1f;
    }

    public bool Check()
    {
        if (time < 0f)
            return false;
        return Time.time >= time;
    }

    public bool Reached()
    {
        if (Check())
        {
            Reset();
            return true;
        }
        return false;
    }
}