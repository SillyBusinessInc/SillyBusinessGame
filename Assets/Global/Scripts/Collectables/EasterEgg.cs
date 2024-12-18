using UnityEngine;

public class EasterEgg : Collectable
{
    public string secretId; // Unique identifier for this secret

    public override void OnCollect()
    {
        // Notify the game manager that a secret has been found
        // GameManager.Instance.CollectSecret(secretId);
        GlobalReference.GetReference<PlayerReference>().Player.playerStatistic.SecretCrumbs.Add(secretId);

        foreach (var secret in GlobalReference.GetReference<PlayerReference>().Player.playerStatistic.SecretCrumbs)
        {
            Debug.Log(secret);
        }
        // Debug.Log(GlobalReference.GetReference<PlayerReference>().Player.playerStatistic.SecretCrumbs);
    }
}
