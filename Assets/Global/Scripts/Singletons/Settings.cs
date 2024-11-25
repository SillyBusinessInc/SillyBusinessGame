using System.Collections.Generic;
using UnityEngine;

public class Settings : SaveSystem
{
    protected override string Prefix => "settings";

    public override void Init() {
        // volume settings
        Add("master_volume", 100);
        Add("music_volume", 100);
        Add("sfx_volume", 100);

        // screen settings
        Add("fullscreen", true);

        // keybinds
    }
}