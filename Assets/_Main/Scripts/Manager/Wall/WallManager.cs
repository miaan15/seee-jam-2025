using UnityEngine;
using UnityEngine.Tilemaps;

public class WallManager : MonoBehaviour
{
    private LevelManager levelManager;

    public WallContext Context;
    public Tilemap Layout;

    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
    }
}
