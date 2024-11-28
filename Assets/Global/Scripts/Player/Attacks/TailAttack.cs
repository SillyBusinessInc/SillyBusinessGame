using System.Collections;
using UnityEngine;

public class TailAttack : Attack
{
    [HideInInspector]
    public Player player;
    public float duration;
    protected float idleTime;

    public override void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();
    }

    public override IEnumerator SetStateIdle()
    {
        Debug.Log("SetStateIdle");
        yield return new WaitForSeconds(idleTime);
        Debug.Log("SetStateIdle");
        player.SetState(player.states.Idle);
    }
}
