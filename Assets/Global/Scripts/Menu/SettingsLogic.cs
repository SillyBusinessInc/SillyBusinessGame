using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLogic : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    [Header("Imports")]
    [SerializeField] private TMP_Dropdown resolution;
    [SerializeField] private Toggle fullscreen;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider effectsVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider brightness;

    [SerializeField] private Button cancel;
    [SerializeField] private Button confirm;
    [SerializeField] private Button back;

    void Start() => LoadFromSave();
    void Update() => UpdateButtonState();

    public static List<Vector2> Resolutions => new() {
        new(2560, 1440),
        new(1920, 1080),
        new(1366, 768),
        new(1280, 720),
        new(1920, 1200),
        new(1680, 1050),
        new(1440, 900),
        new(1280, 800),
        new(1024, 768),
        new(800, 600),
        new(640, 480)
    };

    private void LoadFromSave() {
        GlobalReference.Settings.IsLocked = true;
        int resX = GlobalReference.Settings.Get<int>("resolution_width");
        int resY = GlobalReference.Settings.Get<int>("resolution_height");
        resolution.value = Resolutions.IndexOf(new(resX, resY));
        fullscreen.isOn = GlobalReference.Settings.Get<bool>("fullscreen");
        masterVolume.value = GlobalReference.Settings.Get<float>("master_volume");
        effectsVolume.value = GlobalReference.Settings.Get<float>("effects_volume");
        musicVolume.value = GlobalReference.Settings.Get<float>("music_volume");
        brightness.value = GlobalReference.Settings.Get<float>("brightness");
        GlobalReference.Settings.IsLocked = false;
    }

    private void UpdateButtonState() {
        cancel.interactable = GlobalReference.Settings.IsDirty;
        confirm.interactable = GlobalReference.Settings.IsDirty;
        back.interactable = !GlobalReference.Settings.IsDirty;
    }
    
    #region button event methods
    public void OnResolutionChange(int value) {
        int resX = (int)Resolutions[value].x;
        int resY = (int)Resolutions[value].y;
        GlobalReference.Settings.Set("resolution_width", resX);
        GlobalReference.Settings.Set("resolution_height", resY);
    }
    public void OnFullscreenChange(bool value) => GlobalReference.Settings.Set("fullscreen", value);
    public void OnMasterVolumeChange(float value) => GlobalReference.Settings.Set("master_volume", value);
    public void OnEffectsVolumeChange(float value) => GlobalReference.Settings.Set("effects_volume", value);
    public void OnMusicVolumeChange(float value) => GlobalReference.Settings.Set("music_volume", value);
    public void OnBrightnessChange(float value) => GlobalReference.Settings.Set("brightness", value);
    public void OnBack() {
        GlobalReference.Settings.SaveAll();
        UILogic.FadeToScene("Menu", fadeImage, this);
    }
    public void OnSave() {
        GlobalReference.Settings.SaveAll();
    }
    public void OnCancel() {
        GlobalReference.Settings.LoadAll();
        LoadFromSave();
    }
    #endregion
}
