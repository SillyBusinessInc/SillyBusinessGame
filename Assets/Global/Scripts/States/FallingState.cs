using UnityEngine;

public class FallingState : BaseState
{
    public FallingState(Player player) : base(player)
    {
    }


    public override void Update()
    {

    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.SetState(new IdleState(player));
            player.isOnGround = true;
        }
    }
}
