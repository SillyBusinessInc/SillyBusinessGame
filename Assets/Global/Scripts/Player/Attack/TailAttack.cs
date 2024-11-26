using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class TailAttack : Attack
{
    public Player player;

    public void Awake()
    {
        player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();
    }

    public IEnumerator canDoDamageCoroutine(float time)
    {
        // move this up after testing
        player.SetState(player.states.Idle);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        //player.SetState(player.states.Idle);
    }

    public void canDoDamage(float time)
    {
        StartCoroutine(canDoDamageCoroutine(time));
    }
}
