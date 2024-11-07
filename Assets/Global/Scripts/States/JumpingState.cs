using JetBrains.Annotations;
using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        if (player.jumps > 0)
        {
            player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
            player.jumps--;
        }
        player.SetState(new FallingState(player));
    }
    public void OnJump()
    {
        
    }
}
