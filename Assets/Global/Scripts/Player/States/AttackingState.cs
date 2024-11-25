using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingState : StateBase
{
    public bool isAttacking;

    public AttackingState(Player player)
        : base(player) { }

    public void IncreaseIndex()
    {
        Player.attackIndex =
            Player.attackIndex >= Player.Tail.GetComponent<Tail>().combo.Count - 1
                ? 0
                : ++Player.attackIndex;
    }

    public override void Update()
    {
        if (isAttacking)
        {
            Object.Instantiate(Player.Tail.GetComponent<Tail>().combo[Player.attackIndex]);
            isAttacking = false;
            IncreaseIndex();
        }
    }

    public override void Enter()
    {
        isAttacking = true;
    }

    public override void Exit()
    {
        isAttacking = false;
    }

    public override void Jump(InputAction.CallbackContext ctx) { }

    public override void Glide(InputAction.CallbackContext ctx) { }

    public override void Dodge(InputAction.CallbackContext ctx) { }

    public override void Move(InputAction.CallbackContext ctx) { }

    public override void Sprint(InputAction.CallbackContext ctx) { }

    public override void Attack(InputAction.CallbackContext ctx) { }
}
