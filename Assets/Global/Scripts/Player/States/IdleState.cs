using UnityEngine;

public class IdleState : StateBase
{
    private float currentTime;

    public IdleState(Player player) : base(player) {}

    public override void Enter()
    {
        currentTime = Random.Range(Player.minIdleTime, Player.maxIdleTime);
        Player.playerAnimationsHandler.resetStates();
    }

    public override void Update()
    {
        // add gravity to y velocity
        float linearY = ApplyGravity(Player.rb.linearVelocity.y);
        Player.targetVelocity = new Vector3(0, linearY, 0);

        if (!Player.isGrounded) Player.SetStateAfter(Player.states.Falling, Player.coyoteTime);

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            Player.playerAnimationsHandler.SetInt("IdleSpecialType", Random.Range(1, 3));
            Player.playerAnimationsHandler.animator.SetTrigger("IdleSpecial");
            currentTime = Random.Range(Player.minIdleTime, Player.maxIdleTime);
        }
    }
    
}
