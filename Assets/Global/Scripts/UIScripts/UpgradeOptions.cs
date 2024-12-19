using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeOptions : Reference
{
    [HideInInspector] public UpgradeOption option;
    private UpgradeOptionLogic UpgradeOptionLogic;
    [HideInInspector] public bool isShown = false;
    
    [ContextMenu("SHOW")]
    public void ShowOption()
    {
        isShown = true;
        SetCursorState(true, CursorLockMode.None);
        Time.timeScale = 0;
        gameObject.SetActive(true);

        if (option != null) {
            UpgradeOptionLogic.data = option;
        }
    }

    [ContextMenu("HIDE")]
    public void HideOption()
    {
        Time.timeScale = 1;
        SetCursorState(false, CursorLockMode.Locked);
        gameObject.SetActive(false);
        isShown = false;
    }
    
    void SetCursorState(bool cursorVisible, CursorLockMode lockMode) {
        Cursor.visible = cursorVisible;
        Cursor.lockState = lockMode;
    }

    public void Confirm(InputAction.CallbackContext ctx) {
        if (!isShown) return;

        if (ctx.started) {
            foreach(ActionParamPair action in option.interactionActions) {
                action.InvokeAction();
            }
            HideOption();
        }
    }
}
