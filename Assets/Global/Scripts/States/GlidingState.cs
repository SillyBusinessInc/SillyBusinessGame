using System;
using UnityEngine;

public class GlidingState : BaseState
{
    private float oldDrag;
    public GlidingState(Player player) : base(player)
    {
    }
    public override void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.SetState(new FallingState(player));
        }
        player.transform.Translate(0.5f * Vector3.forward * Time.deltaTime * player.speed * player.horizontalInput);
        player.transform.Translate(0.5f * Vector3.left * Time.deltaTime * player.speed * player.verticalInput);
       
        if(Input.GetKeyDown(KeyCode.Space) && player.doubleJumps > player.currentJumps)
        {
            player.SetState(new JumpingState(player));
            player.currentJumps += 1; 
        }
        if(Input.GetKeyDown(KeyCode.E) && player.canDodgeRoll)
        {
            player.SetState(new DodgeRollState(player));
        }

    }

    public override void Enter()
    {
        oldDrag = player.playerRb.linearDamping;
        player.playerRb.linearDamping = player.glideDrag;
    }

    public override void Exit()
    {
        player.playerRb.linearDamping = oldDrag;
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.SetState(new IdleState(player));
            player.currentJumps = 0;
        }
    }
}