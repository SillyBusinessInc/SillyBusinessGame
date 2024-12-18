using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class UpgradeOptions : Reference
{
    [HideInInspector]
    public List<UpgradeOption> options;
    public UpgradeOption option;
    [HideInInspector]
    public bool isShown = false;
    
    [ContextMenu("SHOW")]
    public void ShowOptions()
    {
        isShown = true;
        SetCursorState(true, CursorLockMode.None);
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Debug.Log(transform.childCount);
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     Transform child = transform.GetChild(i);
        //     child.gameObject.SetActive(true);
        //     SetOptions(options[i], i);
        // }
    }

    [ContextMenu("HIDE")]
    public void HideOptions()
    {
        Time.timeScale = 1;
        SetCursorState(false, CursorLockMode.Locked);
        gameObject.SetActive(false);
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     Transform child = transform.GetChild(i);
        //     child.gameObject.SetActive(false);
        // }
        isShown = false;
    }

    public void SetOptions(UpgradeOption upgrade, int index)
    {
        transform.GetChild(index).GetComponent<UpgradeOptionLogic>().data = upgrade;
    }

    public GameObject GetOption(int index)
    {
        transform.GetChild(index).gameObject.SetActive(true);
        return transform.GetChild(index).gameObject;;
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
            HideOptions();
        }
    }
}
