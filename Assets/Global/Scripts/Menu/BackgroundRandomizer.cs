using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgrounds;
    private Image background;
    private Image overlay;

    private float lastTimeSwitched = 0;
    private int lastIndex;
    [SerializeField] private float interval = 3;
    // [SerializeField] private float minLoadTime = 5;

    void Start() => Switch();
    void Update() => Switch();

    public void Switch() {
        Debug.Log(lastTimeSwitched);
        if (lastTimeSwitched == -1 || lastTimeSwitched + interval > Time.unscaledTime) return;
        if (background == null) background = transform.GetChild(0).GetComponent<Image>();
        if (overlay == null) overlay = transform.GetChild(1).GetComponent<Image>();

        lastTimeSwitched = -1;

        int index = UnityEngine.Random.Range(0, backgrounds.Count-1);
        if (index == lastIndex) index = backgrounds.Count-1;
        lastIndex = index;

        Sprite sprite = backgrounds[index];
        overlay.sprite = sprite;
        SetAlpha(0);
        StartCoroutine(Fade());
    }

    private IEnumerator Fade() {
        for (float i = 0; i <= 1.1f; i += Time.unscaledDeltaTime)
        {
            SetAlpha(i);
            yield return null;
        }
        PostFade();
    }

    private void PostFade() {
        lastTimeSwitched = Time.unscaledTime;
        background.sprite = overlay.sprite;
        SetAlpha(0);
    }

    private void SetAlpha(float alpha) {
        overlay.color = new Color(1, 1, 1, alpha);
    }
}
