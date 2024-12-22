using UnityEngine;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] private Confirmation confirmation;
    [SerializeField] private Image fadeImage;
    
    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void OnNewRun() => UILogic.FadeToScene("Loading", fadeImage, this);
    public void OnUpgrades() => UILogic.FadeToScene("Loading", fadeImage, this);
    public void OnArchive() => UILogic.FadeToScene("Loading", fadeImage, this);
    public void OnSettings() => UILogic.FadeToScene("Settings", fadeImage, this);
    public void OnQuit() => confirmation.RequestConfirmation("Are you sure?", "Unsaved progress will be lost if you quit now", () => Application.Quit());
}
