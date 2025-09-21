using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private static Health instance;
    public static Health Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Health>();

                if (instance == null)
                {
                    Debug.LogError("NO Health !? YET !?");
                }
            }

            return instance;
        }
    }

    public TextMeshProUGUI textMeshProUGUI;

    public int CurHealth;

    private void Update()
    {
        textMeshProUGUI.text = CurHealth.ToString();
    }
}
