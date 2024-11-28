using UnityEngine;
using UnityEngine.InputSystem;
public class AttackingState : StateBase
{
    public AttackingState(Player player)
        : base(player) { }

    public void IncreaseIndex()
    {
        Player.Tail.attackIndex =
            Player.Tail.attackIndex >= Player.Tail.currentTail.currentCombo.Count - 1
                ? 0
                : ++Player.Tail.attackIndex;
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
                Player.Tail.attackIndex = 0;
            }
            tail.currentCombo = tail.groundCombo;
        }
        else
        {
            if(tail.currentCombo == tail.groundCombo)
            {
                Player.Tail.attackIndex = 0;
            }
            tail.currentCombo = tail.airCombo;
        }
        if(tail.currentCombo.Count == 0)
        {
            Player.SetState(Player.states.Idle);
            return;
        }
        var currentCombo = tail.currentCombo[Player.Tail.attackIndex];
        currentCombo.Start();
        Player.StartCoroutine(currentCombo.SetStateIdle());
        IncreaseIndex();
    }

    public override void Exit()
    {
        Player.Tail.flipDoDamage = false;
        Player.Tail.tailCanDoDamage = false;
        Player.collidersEnemy.Clear();
    }

    public override void Jump(InputAction.CallbackContext ctx) { }

    public override void Glide(InputAction.CallbackContext ctx) { }

    public override void Dodge(InputAction.CallbackContext ctx) { }

    public override void Move(InputAction.CallbackContext ctx) { }

    public override void Sprint(InputAction.CallbackContext ctx) { }

    public override void Attack(InputAction.CallbackContext ctx) { }
}
