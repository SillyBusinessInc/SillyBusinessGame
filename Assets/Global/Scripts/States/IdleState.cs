using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            player.SetState(new WalkingState(player));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            player.SetState(new JumpingState(player));
        }
    }

}