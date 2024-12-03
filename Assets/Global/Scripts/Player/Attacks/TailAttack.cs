using System.Collections;
using UnityEngine;

public abstract class TailAttack : Attack
{
    public TailAttack(string Name,float damage, float cooldown) : base(Name, damage, cooldown)
    {
    } 
    protected Player player;
    public float duration;
    public override void Start()
    {
        player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();
    }

    public override IEnumerator SetStateIdle()
    {
        // TODO: duration is  the base speed of the attack, and so it should work with the duration of the animation and stuff.
        // And also with attack speed
        yield return new WaitForSeconds(duration);
        if (player.isGrounded) player.SetState(player.movementInput.magnitude > 0 ? player.states.Walking : player.states.Idle);
        else player.SetState(player.states.Falling);
    }

}
