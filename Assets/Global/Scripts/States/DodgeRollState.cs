using UnityEngine;

public class DodgeRollState : BaseState
{
    
    private float dodgeRollDuration = 0.5f;
    private float timer;

    public DodgeRollState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Vector3 dodgeDirection = player.GetDirection();
        
        timer = dodgeRollDuration;
        player.playerRb.linearVelocity = player.transform.forward * player.dodgeRollSpeed;
        player.canDodgeRoll = false;
        
        if (dodgeDirection == Vector3.zero)
        {
            dodgeDirection = player.GetComponentInChildren<PlayerObjScript>().transform.forward.normalized;
        }
        Debug.Log(dodgeDirection);
        player.playerRb.linearVelocity = dodgeDirection * player.dodgeRollSpeed;
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

    public override void Exit()
    {
        player.playerRb.linearVelocity = Vector3.zero;
    }

    // public override void HandleInput()
    // {
    //     // Disable input during dodge roll
    // }
}