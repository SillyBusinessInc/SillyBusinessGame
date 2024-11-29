using UnityEngine;


public abstract class ActionScriptableObject : ScriptableObject
{
    // The method to be implemented by each action
    public abstract void InvokeAction(string param);
}


[System.Serializable]
public class ActionParamPair
{
    public ActionScriptableObject action;
    public string param;
    
    public void InvokeAction()
    {
        action.InvokeAction(param);
    }
}