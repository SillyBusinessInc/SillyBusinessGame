using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(Player player) : base(player) {}

    public override void Update()
    {
        if (!Player.isGrounded) Player.activeCoroutine = Player.StartCoroutine(Player.SetStateAfter(Player.states.Falling, Player.coyoteTime));
    }
}
