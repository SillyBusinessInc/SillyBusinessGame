using UnityEngine;

public class GlidingState : StateBase
{
    private float oldDrag;
    
    public GlidingState(Player player) : base(player) {}

    public override void Update()
    {
        // smoothly rotate to desired angle and apply force
        float singleStep = Time.deltaTime;
        Player.targetVelocity = Vector3.RotateTowards(Player.targetVelocity, Player.GetDirection() + new Vector3(0, -0.5f, 0), singleStep, 0.0f);

        // ground check
        if (Player.isGrounded) Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
    }

    public override void Enter()
    {
        oldDrag = Player.rb.linearDamping;
        Player.rb.linearDamping = Player.glideDrag;
    }

    public override void Exit()
    {
        Player.rb.linearDamping = oldDrag;
    }
}