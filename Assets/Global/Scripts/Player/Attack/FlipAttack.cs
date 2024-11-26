using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class FlipAttack : TailAttack
{
    public void Start()
    {
        //Debug.Log("In rightTail attack class");
        player.tailDoDamage = player.rightTailDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.GetComponent<Tail>()
            .animatorTailAttack;
        animatorTailAttack.SetTrigger("FlipAttack");
        canDoDamage(0.5f);
    }
}
