using UnityEngine;

public class GlidingState : StateBase
{
    private float oldDrag;
    public GlidingState(Player player) : base(player)
    {
    }
    public override void Update()
    {
        Player.rb.AddForce(Player.GetDirection() * Player.playerStatistic.Speed.GetValue(), ForceMode.Acceleration);
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

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
        }
    }
}