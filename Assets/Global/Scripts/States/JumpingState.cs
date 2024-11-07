using JetBrains.Annotations;
using UnityEngine;

public class JumpingState : BaseState
{
    public JumpingState(Player player) : base(player)
    {
    }

    public override void Update()
    {
        player.playerRb.AddForce(Vector3.up * player.jumpforce, ForceMode.Impulse);
        
        player.SetState(new FallingState(player));
    }
    public void OnJump()
    {
        
    }
}
