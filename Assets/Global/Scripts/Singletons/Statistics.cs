using UnityEngine;

public class Statistics
{
    private readonly string pre = "statistics_";
    
    public float DistanceWalked {
        get => PlayerPrefs.GetFloat($"{pre}distanceWalked");
        set => PlayerPrefs.SetFloat($"{pre}distanceWalked", value);
    }

    public int TimesJumped {
        get => PlayerPrefs.GetInt($"{pre}timesJumped");
        set => PlayerPrefs.SetInt($"{pre}timesJumped", value);
    }
}