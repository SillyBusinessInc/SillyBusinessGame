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
            Player.SetState(Player.states.Walking);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.SetState(Player.states.Jumping);
        }
        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
    }

}
