using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class LeftTailAttack : TailAttack
{
    public void Start()
    {
        player.tailDoDamage = player.Tail.GetComponent<Tail>().leftTailDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.GetComponent<Tail>()
            .animator;
        animatorTailAttack.SetTrigger("LeftAttack");
        canDoDamage(0.5f);
    }
}
