using UnityEngine;

public class JumpingState : StateBase
{
    public JumpingState(Player player) : base(player)
    {
    }

    public override void Enter()
    {        
        // Player.playerAnimationsHandler.SetBool("IsFallingDown", false);
        // Player.playerAnimationsHandler.SetBool("IsJumpingBool",true);
        // wait 1 second before the code continues no coroutines

        Player.rb.linearVelocity = new Vector3(Player.rb.linearVelocity.x, 0, Player.rb.linearVelocity.z);
        Player.rb.AddForce(Vector3.up * Player.playerStatistic.JumpForce.GetValue(), ForceMode.Impulse);
        Player.SetState(Player.states.Falling);
        
    }
}
