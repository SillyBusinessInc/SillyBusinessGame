using UnityEngine;

[CreateAssetMenu(menuName = "Actions/LogAction")]
public class LogAction : ActionScriptableObject
{
    public override void InvokeAction(string param)
    {
        Debug.Log($"Interactable msg - {param}");
    }
    
}

