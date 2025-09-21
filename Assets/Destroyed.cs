using UnityEngine;

public class Destroyed : MonoBehaviour
{
    public float time;

    void Start()
    {  
        Destroy(gameObject, time); 
    }

}
