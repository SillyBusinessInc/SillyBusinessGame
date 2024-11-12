using UnityEngine;

public class DodgeRollState : StateBase
{
    
    private float timer;

    public DodgeRollState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        Vector3 dodgeDirection = Player.GetDirection();
        
        timer = Player.dodgeRollDuration;
        Player.canDodgeRoll = false;
        
        if (dodgeDirection == Vector3.zero)
        {
            dodgeDirection = Player.rb.transform.forward.normalized;
        }
        
        Player.rb.AddForce(dodgeDirection * Player.dodgeRollSpeed, ForceMode.Impulse);
        // player.animator.SetTrigger("DodgeRoll");
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (Player.isGrounded)
            {
                Player.SetState(new IdleState(Player));
            }
            else
            {
                Player.SetState(new FallingState(Player));
            }
        }
    }
}