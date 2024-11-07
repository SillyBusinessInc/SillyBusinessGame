using UnityEngine;

public class Walking : BaseMovement
{
    void Update()
    {
    }

    public Walking(Player player) : base(player)
    {

    }

    public override void OnAttack()
    {
        player.SetState(new Attacking(player));
    }

    public override void OnWalk()
    {
        player.Move();
    }

    public override void OnJump()
    {
        player.SetState(new Jumping(player));
    }

    public override void Still()
    {
        player.SetState(new Idle(player));
    }
}
