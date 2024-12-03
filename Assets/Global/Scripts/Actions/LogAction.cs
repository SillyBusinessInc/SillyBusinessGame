using UnityEngine;

[CreateAssetMenu(menuName = "Actions/LogAction")]
public class LogAction : ActionScriptableObject
{
    [SerializeField] private string actionName = "Log Action";
    public override void InvokeAction(string param)
    {
        Debug.Log($"{actionName} - {param}");
    }

}

