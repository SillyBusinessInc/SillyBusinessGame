using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            player.SetState(new WalkingState(player));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new JumpingState(player));
        }
        if (!player.isGrounded)
        {
            player.SetState(new FallingState(player));
        }
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new AttackingState(player));
        }
    }

}
