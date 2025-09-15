using UnityEngine;
using UnityEngine.Tilemaps;

public class WallManager : MonoBehaviour
{
    private LevelManager levelManager;

    public Tilemap Layout;

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }
}
