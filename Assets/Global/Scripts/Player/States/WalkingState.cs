using UnityEngine;

public class WalkingState : StateBase
{
    public WalkingState(Player player) : base(player) {}

    public override void Update()
    {
        // add force to the player object for movement
        // Player.rb.AddForce(Player.GetDirection() * Player.playerStatistic.Speed.GetValue(), ForceMode.Acceleration);
        Player.targetVelocity = Player.GetDirection() * Player.playerStatistic.Speed.GetValue();
        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
    }

}
