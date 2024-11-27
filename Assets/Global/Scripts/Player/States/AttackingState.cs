using UnityEngine;
using UnityEngine.InputSystem;
public class AttackingState : StateBase
{
    public AttackingState(Player player)
        : base(player) { }

    public void IncreaseIndex()
    {
        Player.attackIndex =
            Player.attackIndex >= Player.Tail.currentTail.currentCombo.Count - 1
                ? 0
                : ++Player.attackIndex;
    }

    public override void Enter()
    {
        var tail = Player.Tail.currentTail;
        if(tail.currentCombo.Count == 0) 
        {
            if(Player.isGrounded)
            {
                tail.currentCombo = tail.groundCombo;
            }
            else
            {
                tail.currentCombo = tail.airCombo;
            }
        }
        else if(Player.isGrounded)
        {
            if(tail.currentCombo == tail.airCombo)
            {
                Player.attackIndex = 0;
            }
            tail.currentCombo = tail.groundCombo;
        }
        else
        {
            if(tail.currentCombo == tail.groundCombo)
            {
                Player.attackIndex = 0;
            }
            tail.currentCombo = tail.airCombo;
        }
        if(tail.currentCombo.Count == 0)
        {
            Player.SetState(Player.states.Idle);
            return;
        }
        Object.Instantiate(tail.currentCombo[Player.attackIndex]);
        IncreaseIndex();
    }



    public override void Jump(InputAction.CallbackContext ctx) { }

    public override void Glide(InputAction.CallbackContext ctx) { }

    public override void Dodge(InputAction.CallbackContext ctx) { }

    public override void Move(InputAction.CallbackContext ctx) { }

    public override void Sprint(InputAction.CallbackContext ctx) { }

    public override void Attack(InputAction.CallbackContext ctx) { }
}
