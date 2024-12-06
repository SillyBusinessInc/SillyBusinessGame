using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeOptions : Reference
{
    [HideInInspector]
    public List<UpgradeOption> options;
    
    [ContextMenu("SHOW")]
    public void ShowOptions()
    {
        Debug.Log(options.Count);
        Time.timeScale = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
            SetOptions(options[i], i);
        }
    }

    [ContextMenu("HIDE")]
    public void HideOptions()
    {
        Time.timeScale = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
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


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            GetOption(0).GetComponent<UpgradeOptionLogic>().data.interactionActions[0].InvokeAction();
            HideOptions();
        }
        else if (Input.GetKeyDown("2"))
        {
            Debug.Log("2");
            foreach(ActionParamPair action in options[1].interactionActions)
            {
                action.InvokeAction();
            }
            HideOptions();
        }
        else if (Input.GetKeyDown("3"))
        {
            Debug.Log("3");
            foreach(ActionParamPair action in options[2].interactionActions)
            {
                action.InvokeAction();
            }
            HideOptions();
        }

    }
}
