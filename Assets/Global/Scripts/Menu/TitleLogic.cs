using UnityEngine;
using UnityEngine.UI;

public class TitleLogic : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    
    void Start() {
        fadeImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey) UILogic.FadeToScene("Menu", fadeImage, this);
    }
}
