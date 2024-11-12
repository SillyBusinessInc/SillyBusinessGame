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
            Player.SetState(Player.states.Falling);
        }
        Player.rb.AddForce(Player.GetDirection() * Player.speed, ForceMode.Force);
        
        if(Input.GetKeyDown(KeyCode.Space) && Player.doubleJumps > Player.currentJumps && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.Jumping);
            Player.currentJumps += 1; 
        }
        if(Input.GetKeyDown(KeyCode.E) && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }

    }

    public override void Enter()
    {
        oldDrag = Player.rb.linearDamping;
        Player.rb.linearDamping = Player.glideDrag;
    }

    public override void Exit()
    {
        Player.rb.linearDamping = oldDrag;
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player.SetState(Player.states.Idle);
            Player.currentJumps = 0;
        }
    }
}