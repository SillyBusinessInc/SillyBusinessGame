using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleLogic : MonoBehaviour
{
    [SerializeField] private Image image;
    private bool listening = true;

    void Start() {
        image.gameObject.SetActive(false);
    }
    void Update()
    {
        if (listening && Input.anyKey) {
            listening = false;
            SetAlpha(0);
            image.gameObject.SetActive(true);
            StartCoroutine(Fade());
        }
    }

    private void OnTransition() {
        // image.gameObject.SetActive(false);
        // SetAlpha(0);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator Fade() {
        for (float i = 0; i <= 1.1f; i += Time.deltaTime*2)
        {
            SetAlpha(i);
            yield return null;
        }
        OnTransition();
    }

    private void SetAlpha(float alpha) {
        image.color = new Color(1, 1, 1, alpha);
    }
}
