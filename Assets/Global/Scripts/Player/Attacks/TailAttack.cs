using System.Collections;
using UnityEngine;

public class TailAttack : Attack
{
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
        player.SetState(player.states.Idle);
        player.Tail.cooldownTime = cooldown;
    }
}
