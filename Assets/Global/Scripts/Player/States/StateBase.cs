using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StateBase
{
    protected readonly Player Player;
    protected Vector2 movementInput;

    protected StateBase(Player player)
    {
        this.Player = player;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void OnCollision(Collision collision)
    {

    }

    // Input handling
    public virtual void Move(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        if (ctx.performed && Player.currentState != Player.states.Walking)
        {
            Player.SetState(Player.states.Walking);
        }
    }

    public virtual void Sprint(InputAction.CallbackContext ctx)
    {

    }

    public virtual void Dodge(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (Player.canDodgeRoll)
            {
                Player.SetState(Player.states.DodgeRoll);
            }
        }
    }

    public virtual void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Player.SetState(Player.states.Jumping);
        }

    }

    public virtual void Glide(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Player.SetState(Player.states.Falling);
        }
    }

    public virtual void Crouch(InputAction.CallbackContext ctx)
    {

    }

    public virtual void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!Player.isGrounded)
            {
                Player.attackCounter = 2;
                Player.isSlamming = true;
            }
            Player.SetState(Player.states.Attacking);
        }
    }
}
