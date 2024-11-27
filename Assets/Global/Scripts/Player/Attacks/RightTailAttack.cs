using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class RightTailAttack : TailAttack
{
    public void Start()
    {
        player.tailDoDamage = player.Tail.GetComponent<Tail>().rightTailDamage;
        player.tailCanDoDamage = true;
        Animator animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.GetComponent<Tail>()
            .animator;
        animatorTailAttack.SetTrigger("RightAttack");
        canDoDamage(0.5f);
    }
}
