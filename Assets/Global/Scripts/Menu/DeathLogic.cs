using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathLogic : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private void Start() {
        StartCoroutine(GameOver());
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator GameOver() {
        yield return StartCoroutine(WaitForFewSecond());
        MoveToMenu();
    }

    private IEnumerator WaitForFewSecond() {
        yield return new WaitForSeconds(4f);
    }
        
    private void MoveToMenu() => UILogic.FadeToScene("Menu", fadeImage, this);
}
