using UnityEngine;

public class JumpingState : StateBase
{
    public JumpingState(Player player) : base(player) {}

    public override void Enter()
    {
        Player.rb.linearVelocity = new Vector3(Player.rb.linearVelocity.x, Player.playerStatistic.JumpForce.GetValue(), Player.rb.linearVelocity.z);
        Player.SetState(Player.states.Falling);
    }
}
