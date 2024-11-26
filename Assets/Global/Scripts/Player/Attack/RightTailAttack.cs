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
        canDoDamage(0.5f);
    }
}
