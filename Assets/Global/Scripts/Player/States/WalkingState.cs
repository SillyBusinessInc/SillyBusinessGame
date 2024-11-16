using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : StateBase
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        // add force to the player object for movement
        Player.rb.AddForce(Player.GetDirection() * Player.playerStatistic.speed, ForceMode.Acceleration);
        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
    }

}
