public class Settings : SaveSystem
{
    protected override string Prefix => "settings";

    public override void Init() {
        Add("resolution_width", 2560);
        Add("resolution_height", 1440);
        Add("fullscreen", true);

        Add("master_volume", 0.5f);
        Add("effects_volume", 0.5f);
        Add("music_volume", 0.5f);

        Add("brightness", 0.5f);
    }
}