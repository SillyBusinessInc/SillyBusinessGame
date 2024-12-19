using Unity.VisualScripting;
using UnityEngine;

public class PlatformSync : MonoBehaviour
{
    private Player player;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("!!!!!!");
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("??????");
        player = GlobalReference.GetReference<PlayerReference>().Player;
        other.transform.SetParent(player.transform);
    }
}
