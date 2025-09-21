using UnityEngine;

public class hurtAni : MonoBehaviour
{
    public Animator animator;

    public static void Hurt()
    {
        FindFirstObjectByType<hurtAni>().animator.SetTrigger("hurt");
    }
}
