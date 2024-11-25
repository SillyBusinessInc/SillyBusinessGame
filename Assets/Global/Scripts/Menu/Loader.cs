using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public Image overlay;
    void Start() {
        overlay = transform.GetChild(0).GetComponent<Image>();
    }

    void Update() {
        
    }
}
