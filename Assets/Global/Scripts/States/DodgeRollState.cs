using UnityEngine;

public class DodgeRollState : BaseState
{
    
    private float timer;

    public DodgeRollState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Vector3 dodgeDirection = player.GetDirection();
        
        timer = player.dodgeRollDuration;
        player.canDodgeRoll = false;
        
        if (dodgeDirection == Vector3.zero)
        {
            dodgeDirection = player.playerRb.transform.forward.normalized;
        }
        
        player.playerRb.AddForce(dodgeDirection * player.dodgeRollSpeed, ForceMode.Impulse);
        // player.animator.SetTrigger("DodgeRoll");
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (player.isGrounded)
            {
                player.SetState(new IdleState(player));
            }
            else
            {
                player.SetState(new FallingState(player));
            }
        }
    }
}