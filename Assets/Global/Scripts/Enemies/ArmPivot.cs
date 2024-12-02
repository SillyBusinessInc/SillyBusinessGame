using UnityEngine;

public class ArmPivot : MonoBehaviour
{
    [SerializeField] private EnemiesNS.EnemyBase enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //set the scale of the arm
        Vector3 newScale = this.transform.localScale;
        newScale.z = enemy.attackRange;
        this.transform.localScale = newScale;
    }
}
