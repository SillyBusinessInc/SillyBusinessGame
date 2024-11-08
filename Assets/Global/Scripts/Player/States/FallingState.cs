using UnityEngine;

public class FallingState : StateBase
{
    public FallingState(Player player) : base(player)
    {
    }
    
    public override void Update()
    {
        Player.rb.AddForce(Player.GetDirection() * (Player.speed * Player.airBornMovementFactor), ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Space) && Player.doubleJumps > Player.currentJumps)
        {
            Player.SetState(new JumpingState(Player));
            Player.currentJumps += 1; 
        }

        if(Input.GetKey(KeyCode.LeftShift) && Player.rb.linearVelocity.y < 0)
        {
            Player.SetState(new GlidingState(Player));
        }
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
