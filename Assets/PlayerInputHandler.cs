using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerInteraction interactor;

    // Handling the Input for the player.
    public void OnMove(InputAction.CallbackContext ctx)
    {
        player.currentState.Move(ctx);

        
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        player.currentState.Attack(ctx);
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        player.currentState.Crouch(ctx);
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        player.currentState.Sprint(ctx);
    }

    public void OnGlide(InputAction.CallbackContext ctx)
    {
        player.currentState.Glide(ctx);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        player.currentState.Jump(ctx);
    }

    public void OnDodge(InputAction.CallbackContext ctx)
    {
        player.currentState.Dodge(ctx);
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        interactor.Interact(ctx);
    }

    public void OnConfirm(InputAction.CallbackContext ctx) {

    }
}
