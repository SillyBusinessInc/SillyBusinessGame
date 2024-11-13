
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatistic", menuName = "PlayerStatistic")]
public class PlayerStatistic : ScriptableObject
{
    public float speed = 5f;
    public Statistic maxHealth;
    public float health;
}