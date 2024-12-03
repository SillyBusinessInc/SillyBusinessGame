using System;
using TMPro;
using UnityEngine;

public class Confirmation : MonoBehaviour
{
    private TMP_Text title;
    private TMP_Text description;
    private Action confirmAction;
    private Action rejectAction;

    void Start() {
        // title = transform.GetChild(0).GetComponent<TMP_Text>();
        // description = transform.GetChild(1).GetComponent<TMP_Text>();

        // gameObject.SetActive(false);
    }

    public void RequestConfirmation(string title_, string description_, Action confirmAction_, Action rejectAction_ = null) {
        gameObject.SetActive(true);
        title = transform.GetChild(0).GetComponent<TMP_Text>();
        description = transform.GetChild(1).GetComponent<TMP_Text>();
        
        title.text = title_;
        description.text = description_;
        confirmAction = confirmAction_;
        rejectAction = rejectAction_;
    }

    public void OnConfirm() {
        if (confirmAction != null) confirmAction();
        gameObject.SetActive(false);
    }

    public void OnReject() {
        if (rejectAction != null) rejectAction();
        gameObject.SetActive(false);
    }
}
