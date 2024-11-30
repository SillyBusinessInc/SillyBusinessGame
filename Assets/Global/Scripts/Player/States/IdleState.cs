using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : StateBase
{
    public float time = 20f;
    private float currentTime;
    public IdleState(Player player) : base(player)
    {
        currentTime = time;
        Debug.Log("IdleState");
        Player.playerAnimationsHandler.resetStates();
    }

    public override void Update()
    {
        Player.playerAnimationsHandler.SetBool("IsRunning", false);
        Player.playerAnimationsHandler.resetStates();

        if (!Player.isGrounded)
        {
            Player.SetState(Player.states.Falling);
        }
        //every minute i want a 
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {

            Player.playerAnimationsHandler.SetInt("IdleSpecialType", Random.Range(1, 3));
            Player.playerAnimationsHandler.animator.SetTrigger("IdleSpecial");
            currentTime = time;
        }
    }

    public override void Jump(InputAction.CallbackContext ctx)
    {
        Player.playerAnimationsHandler.SetBool("IsFallingDown", false);
        Player.playerAnimationsHandler.SetBool("IsJumpingBool",true);

        base.Jump(ctx);
    }

}
