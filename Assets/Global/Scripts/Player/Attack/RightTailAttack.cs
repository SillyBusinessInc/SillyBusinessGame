using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class RightTailAttack : TailAttack
{
    public void Start()
    {
        //Debug.Log("In rightTail attack class");
        player.tailDoDamage = player.rightTailDamage;
        player.tailCanDoDamage = true;
        Animator animatorLeftAttack = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>().Tail.GetComponent<Tail>().animatorRightAttack;
        animatorLeftAttack.SetTrigger("RightAttack");
        canDoDamage(0.5f);
    }
}
