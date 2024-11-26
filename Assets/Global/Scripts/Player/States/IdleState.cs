using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player) : base(player) {}

    public override void Update()
    {
        Player.targetVelocity = Vector3.zero;
        if (!Player.isGrounded) Player.SetState(Player.states.Falling);
    }
}
