using UnityEngine;

public class JumpingState : StateBase
{
    public JumpingState(Player player) : base(player)
    {
    }

    public override void Update()
    {
        Player.rb.linearVelocity = new Vector3(Player.rb.linearVelocity.x, 0, Player.rb.linearVelocity.z);
        Player.rb.AddForce(Vector3.up * Player.jumpForce, ForceMode.Impulse);

        Player.SetState(Player.states.Falling);
    }
}
