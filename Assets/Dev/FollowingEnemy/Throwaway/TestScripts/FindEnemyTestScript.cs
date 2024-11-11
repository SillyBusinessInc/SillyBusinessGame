using System.Linq;
using UnityEngine;

public class TestFindEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Check for Enemies")]
    public void CheckEnemy()
    {
        EnemyBase[] enemy = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);

        if (enemy.Length < 1) return;
        Debug.Log(enemy[0].name, enemy[0]);
    }

    [ContextMenu("List all Enemies")]
    public void ListEnemies()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);

        if (enemies.Length < 1)
        {
            Debug.LogWarning("No enemies found!");
            return;
        }

        foreach (EnemyBase enemy in enemies)
        {
            Debug.Log(enemy.name, enemy);
        }
    }



}
