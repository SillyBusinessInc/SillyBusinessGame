using UnityEngine;

public class JumpingState : StateBase
{
    public JumpingState(Player player) : base(player)
    {
    }
    
    public override void Update()
    {
        Player.playerRb.linearVelocity = new Vector3(Player.playerRb.linearVelocity.x, 0, Player.playerRb.linearVelocity.z);
        Player.playerRb.AddForce(Vector3.up * Player.jumpForce, ForceMode.Impulse);
        Player.SetState(new FallingState(Player));
    }
}
