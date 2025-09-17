using UnityEngine;

public class PlayerBeats : MonoBehaviour
{
    private PlayerManager manager;
    private PlayerParameters parameters;


    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
        parameters = manager.Parameters;
    }

    private void Start()
    {
    }

}
