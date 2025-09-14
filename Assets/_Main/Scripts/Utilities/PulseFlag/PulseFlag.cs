public class PulseFlag
{
    private bool active;
    private bool wasActive;

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        if (wasActive)
        {
            wasActive = false;
        }
        if (active)
        {
            active = false;
            wasActive = true;
        }
    }

    public bool IsActive() => active;
    public bool WasActiveLastTime() => wasActive;
}
