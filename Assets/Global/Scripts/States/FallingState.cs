using UnityEngine;

public class FallingState : BaseState
{
    public FallingState(Player player) : base(player)
    {
    }
    
    public override void Update()
    {

        player.playerRb.AddForce(player.GetDirection() * player.speed * 0.5f, ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Space) && player.doubleJumps > player.currentJumps && player.canDodgeRoll)
        {
            player.SetState(new JumpingState(player));
            player.currentJumps += 1; 
        }

        if(Input.GetKey(KeyCode.LeftShift) && player.playerRb.linearVelocity.y < 0 && player.canDodgeRoll)
        {
            player.SetState(new GlidingState(player));
        }
        if(Input.GetKeyDown(KeyCode.E) && player.canDodgeRoll)
        {
            player.SetState(new DodgeRollState(player));
        }
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
