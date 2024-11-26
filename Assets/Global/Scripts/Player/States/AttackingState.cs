using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingState : StateBase
{
    public AttackingState(Player player)
        : base(player) { }

    public void IncreaseIndex()
    {
        Player.attackIndex =
            Player.attackIndex >= Player.Tail.GetComponent<Tail>().currentCombo.Count - 1
                ? 0
                : ++Player.attackIndex;
    }

    public override void Enter()
    {
        if(Player.Tail.GetComponent<Tail>().currentCombo != Player.Tail.GetComponent<Tail>().groundCombo && Player.Tail.GetComponent<Tail>().currentCombo != Player.Tail.GetComponent<Tail>().groundCombo)
        {
            if(Player.isGrounded)
            {
                Player.Tail.GetComponent<Tail>().currentCombo = Player.Tail.GetComponent<Tail>().groundCombo;
            }
            else
            {
                Player.Tail.GetComponent<Tail>().currentCombo = Player.Tail.GetComponent<Tail>().airCombo;
            }
        }
        if(Player.isGrounded)
        {
            if(Player.Tail.GetComponent<Tail>().currentCombo == Player.Tail.GetComponent<Tail>().airCombo)
            {
                Player.attackIndex = 0;
            }
            Player.Tail.GetComponent<Tail>().currentCombo = Player.Tail.GetComponent<Tail>().groundCombo;
        }
        else
        {
            if(Player.Tail.GetComponent<Tail>().currentCombo == Player.Tail.GetComponent<Tail>().groundCombo)
            {
                Player.attackIndex = 0;
            }
            Player.Tail.GetComponent<Tail>().currentCombo = Player.Tail.GetComponent<Tail>().airCombo;
        }
        if(Player.Tail.GetComponent<Tail>().currentCombo.Count == 0)
        {
            Player.SetState(Player.states.Idle);
            return;
        }
        Object.Instantiate(Player.Tail.GetComponent<Tail>().currentCombo[Player.attackIndex]);
        IncreaseIndex();
    }



    public override void Jump(InputAction.CallbackContext ctx) { }

    public override void Glide(InputAction.CallbackContext ctx) { }

    public override void Dodge(InputAction.CallbackContext ctx) { }

    public override void Move(InputAction.CallbackContext ctx) { }

    public override void Sprint(InputAction.CallbackContext ctx) { }

    public override void Attack(InputAction.CallbackContext ctx) { }
}
