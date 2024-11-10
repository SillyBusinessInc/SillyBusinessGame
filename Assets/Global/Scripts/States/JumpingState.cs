using JetBrains.Annotations;
using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState(Player player) : base(player)
    {
    }
    
    public override void Update()
    {
        player.playerRb.linearVelocity = new Vector3(player.playerRb.linearVelocity.x, 0, player.playerRb.linearVelocity.z);
        player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
        player.SetState(new FallingState(player));
    }
}
