using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TailAttack : Attack
{
    protected Transform tailTransform;
    protected Rigidbody playerRb;

    protected Player player;

    protected void Enter()
    {
        player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();
        playerRb = GlobalReference
            .GetReference<PlayerReference>()
            .PlayerObj.GetComponent<Rigidbody>();
        tailTransform = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.transform;
        player.tailCanDoDamage = true;
    }

    public void Turn(float degrees)
    {
        tailTransform.RotateAround(playerRb.position, Vector3.up, degrees);
    }
    public void Exit()
    {
        player.SetState(
            player.movementInput.magnitude > 0 ? player.states.Walking : player.states.Idle
        );
        player.tailCanDoDamage = false;
        Destroy(gameObject);
    }
}
