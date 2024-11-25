using UnityEngine;

public class MenuLogic : MonoBehaviour
{
    public Confirmation confirmation;

    public void OnNewRun() {

    }

    public void OnUpgrades() {

    }

    public void OnArchive() {

    }

    public void OnSettings() {

    }

    public void OnQuit() {
        confirmation.RequestConfirmation("Are you sure?", "Unsaved progress will be lost if you quit now", () => Application.Quit());
    }
}
