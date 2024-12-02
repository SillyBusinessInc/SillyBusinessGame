using UnityEngine;
using UnityEngine.UI;

public class SettingsLogic : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    
    public void OnBack() => UILogic.FadeToScene("Menu", fadeImage, this);
}
