using UnityEngine;

public class NerrController : MonoBehaviour
{
    private static NerrController instance;
    public static NerrController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<NerrController>();

                if (instance == null)
                {
                    Debug.LogError("NO NerrController !? YET !?");
                }
            }

            return instance;
        }
    }

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static void Miss()
    {
        Instance.animator.SetTrigger("Miss");
    }
}
