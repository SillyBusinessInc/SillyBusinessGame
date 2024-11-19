using UnityEngine;

[CreateAssetMenu(menuName = "Actions/LogAction")]
public class LogAction : ActionScriptableObject
{
    [SerializeField] private string message;

    public override void InvokeAction()
    {
        Debug.Log($"Interactable msg - {message}");
    }
}
