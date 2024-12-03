using System.Collections.Generic;
using UnityEngine;

public abstract class ActionScriptableObjectBase : ScriptableObject
{
    // The method to be implemented by each action
    public abstract void InvokeAction(List<string> parameters);
}

// For backwards compatibility
public abstract class ActionScriptableObject : OneParamAction { }

public abstract class NoParamAction : ActionScriptableObjectBase
{
    public abstract void InvokeAction();

    public override void InvokeAction(List<string> parameters)
    {
        InvokeAction();
    }
}

public abstract class OneParamAction : ActionScriptableObjectBase
{
    // The method to be implemented by each action
    public abstract void InvokeAction(string parameter);

    public override void InvokeAction(List<string> parameters)
    {
        // validate the number of parameters
        if (parameters.Count < 1)
        {
            Debug.LogWarning("Not enough parameters for action.");
            return;
        }

        InvokeAction(parameters[0]);
    }
}

public abstract class TwoParamAction : ActionScriptableObjectBase
{
    public abstract void InvokeAction(string parameter1, string parameter2);

    public override void InvokeAction(List<string> parameters)
    {
        // validate the number of parameters
        if (parameters.Count < 2)
        {
            Debug.LogWarning("Not enough parameters for action.");
            return;
        }

        InvokeAction(parameters[0], parameters[1]);
    }

}

public abstract class ThreeParamAction : ActionScriptableObjectBase
{
    public abstract void InvokeAction(string parameter1, string parameter2, string parameter3);

    public override void InvokeAction(List<string> parameters)
    {
        // validate the number of parameters
        if (parameters.Count < 3)
        {
            Debug.LogWarning("Not enough parameters for action.");
            return;
        }

        InvokeAction(parameters[0], parameters[1], parameters[2]);
    }
}


[System.Serializable]
public class ActionParamPair
{
    public ActionScriptableObjectBase action;
    public List<string> parameters;

    public void InvokeAction()
    {
        if (action == null || parameters == null)
        {
            Debug.LogWarning("Action or parameters are not assigned.");
            return;
        }

        action.InvokeAction(parameters);
    }
}