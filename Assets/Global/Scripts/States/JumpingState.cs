using JetBrains.Annotations;
using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState(Player player) : base(player)
    {
    }

    [System.Obsolete]
    public override void Update()
    {
        player.playerRb.velocity = new Vector3(player.playerRb.velocity.x, 0, player.playerRb.velocity.z);
        player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
        player.SetState(new FallingState(player));
    }
    public void OnJump()
    {
        
    }
}
