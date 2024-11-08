using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Player.SetState(new WalkingState(Player));
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.SetState(new JumpingState(Player));
        }
        if (!Player.isGrounded)
        {
            Player.SetState(new FallingState(Player));
        }
    }

}
