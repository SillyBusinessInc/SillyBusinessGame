using UnityEngine;

public class UpgradeOptions : MonoBehaviour
{
    [ContextMenu("SHOW")]
    public void ShowOptions() {
        Time.timeScale = 0;
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }

    [ContextMenu("HIDE")]
    public void HideOptions() {
        Time.timeScale = 1;
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    public void SetOptions() {
        // for later
    }

    // TEMP HARDCODE:
    public void GlorbTactics() {
        HideOptions();
        GlobalReference.GetReference<PlayerReference>().Player.IncreaseMaxHealth(25);
    }
    public void NoonSupport() {
        HideOptions();
        GlobalReference.GetReference<PlayerReference>().Player.Heal(50);
    }
    public void WalnutCurse() {
        HideOptions();
        GlobalReference.GetReference<PlayerReference>().Player.MultiplyMaxHealth(0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            GlorbTactics();
        }
        else if (Input.GetKeyDown("2"))
        {
            NoonSupport();
        }
        else if (Input.GetKeyDown("3"))
        {
            WalnutCurse();
        }
    }
}
