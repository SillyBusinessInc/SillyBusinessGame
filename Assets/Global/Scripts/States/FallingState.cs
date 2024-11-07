using UnityEngine;

public class FallingState : BaseState
{
    public FallingState(Player player) : base(player)
    {
    }


    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new JumpingState(player));
        }
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.SetState(new IdleState(player));
            player.jumps = 2;
        }
    }
}
