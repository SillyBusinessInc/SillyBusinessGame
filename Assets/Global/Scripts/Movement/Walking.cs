using UnityEngine;

public class WalkingState : BaseState
{
    void Update()
    {
    }

    public WalkingState(Player player) : base(player)
    {

    }

    public override void OnAttack()
    {
        player.SetState(new AttackingState(player));
    }

    public override void OnWalk()
    {
        player.transform.Translate(Vector3.forward * Time.deltaTime * player.speed * player.horizontalInput);
        player.transform.Translate(Vector3.left * Time.deltaTime * player.speed * player.verticalInput);
    }

    public override void OnJump()
    {
        player.SetState(new JumpingState(player));
    }

    public override void Still()
    {
        player.SetState(new IdleState(player));
    }
}
