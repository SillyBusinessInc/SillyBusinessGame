using UnityEngine;
using UnityEngine.InputSystem;

public class HurtState: StateBase
{

    public HurtState(Player player) : base(player) { }
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }
    public override void Hurt(Vector3 direction){}
    public override void Move(InputAction.CallbackContext ctx){}

    public override void Sprint(InputAction.CallbackContext ctx) { }

    public override void Dodge(InputAction.CallbackContext ctx){}

    public override void Jump(InputAction.CallbackContext ctx){}

    public override void Glide(InputAction.CallbackContext ctx){}

    public override void Crouch(InputAction.CallbackContext ctx) {}

    public override void Attack(InputAction.CallbackContext ctx){}
}