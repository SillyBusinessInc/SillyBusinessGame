using UnityEngine;

public class ArmPivot : MonoBehaviour
{
    [SerializeField] private FollowEnemy fe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        //set the scale of the arm
        Vector3 newScale = this.transform.localScale;
        newScale.z = fe.attackRange;
        this.transform.localScale = newScale;
    }
}
