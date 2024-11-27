using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class FlipAttack : TailAttack
{
    public void Start()
    {
        player.tailDoDamage = player.Tail.GetComponent<Tail>().flipDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.GetComponent<Tail>()
            .animator;
        animatorTailAttack.SetTrigger("FlipAttack");
        canDoDamage(0.5f);
    }
}
