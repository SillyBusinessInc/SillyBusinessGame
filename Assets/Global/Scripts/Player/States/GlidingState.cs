using UnityEngine;
using UnityEngine.InputSystem;

public class GlidingState : StateBase
{
    private float oldDrag;
    public GlidingState(Player player) : base(player)
    {
    }
    public override void Update()
    {
        if (Player.inputActions.actions["Glide"].ReadValue<float>() == 0)
        {
            Player.SetState(Player.states.Falling);  // When movement ends (e.g., released)
        }

        
        Player.rb.AddForce(Player.GetDirection() * Player.playerStatistic.Speed.GetValue(), ForceMode.Force);

        
        if(Player.inputActions.actions["Jump"].triggered && Player.playerStatistic.DoubleJumpsCount.GetValueInt() > Player.currentJumps && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.Jumping);
            Player.currentJumps += 1;
        }

        if(Player.inputActions.actions["Dodge"].triggered && Player.canDodgeRoll)
        {
            Player.SetState(Player.states.DodgeRoll);
        }
        if (Input.GetMouseButtonDown(0)) // TODO: replace this with the new event system thing
        {
            Player.attackCounter = 2;
            Player.isSlamming = true;
            Player.SetState(Player.states.Attacking);
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