using UnityEngine;

public class GlidingState : StateBase
{
    private float oldDrag;
    public GlidingState(Player player) : base(player)
    {
    }
    public override void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            Player.SetState(new FallingState(Player));
        }
        Player.playerRb.AddForce(Player.GetDirection() * Player.speed, ForceMode.Force);
        
        if(Input.GetKeyDown(KeyCode.Space) && Player.doubleJumps > Player.currentJumps)
        {
            Player.SetState(new JumpingState(Player));
            Player.currentJumps += 1; 
        }

    }

    public override void Enter()
    {
        oldDrag = Player.playerRb.linearDamping;
        Player.playerRb.linearDamping = Player.glideDrag;
    }

    public override void Exit()
    {
        Player.playerRb.linearDamping = oldDrag;
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player.SetState(new IdleState(Player));
            Player.currentJumps = 0;
        }
    }
}