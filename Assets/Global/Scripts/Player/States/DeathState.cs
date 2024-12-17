using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathState : StateBase
{
    private bool isNotDeath = true;
     
    public DeathState(Player player) : base(player) { }
    private AnimatorStateInfo stateInfo;
    
    public override void Enter()
    {
        stateInfo = Player.playerAnimationsHandler.animator.GetCurrentAnimatorStateInfo(0);
        Player.playerAnimationsHandler.animator.SetTrigger("IsDeath");
    }
    public override void Update()
    {
        Debug.Log(stateInfo.normalizedTime);
        if (stateInfo.IsName("Breadaplus|Bradley_death 0") && stateInfo.normalizedTime >= 0.8f)
        {
            if (isNotDeath)
            {
                isNotDeath = false;
                Player.StartCoroutine(Player.DeathScreen());
            }
        }
    }
    public override void Move(InputAction.CallbackContext ctx)
    {
    }

    public override void Sprint(InputAction.CallbackContext ctx) { }

    public override void Dodge(InputAction.CallbackContext ctx)
    {
    }

    public override void Jump(InputAction.CallbackContext ctx)
    {
    }

    public override void Glide(InputAction.CallbackContext ctx)
    {
    }

    public override void Crouch(InputAction.CallbackContext ctx) { }

    public override void Attack(InputAction.CallbackContext ctx)
    {
    }

    public override void Death()
    {
    }
}