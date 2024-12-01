using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player) : base(player) {}

    public override void Update()
    {
        // add gravity to y velocity
        float linearY = ApplyGravity(Player.rb.linearVelocity.y);
        Player.targetVelocity = new Vector3(0, linearY, 0);

        if (!Player.isGrounded) Player.activeCoroutine = Player.StartCoroutine(Player.SetStateAfter(Player.states.Falling, Player.coyoteTime));
    }
}
