using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class TailAttack : Attack
{
    [HideInInspector]
    public Player player;

    public void Awake()
    {
        player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();
    }

    public IEnumerator canDoDamageCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        player.SetState(player.states.Idle);
        Destroy(gameObject);
    }

    public void canDoDamage(float time)
    {
        StartCoroutine(canDoDamageCoroutine(time));
    }
}
