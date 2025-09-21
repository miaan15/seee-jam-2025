using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Data/Enemy/Zombie")]
public class ZombieEnemyData : ScriptableObject
{
    public int Health;
    public int Damage;

    public int MovementSpeed;
    public int RecoverySpeed;
}