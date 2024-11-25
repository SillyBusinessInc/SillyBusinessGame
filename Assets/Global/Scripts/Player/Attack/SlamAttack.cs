using System.Collections;
using UnityEngine;

public class SlamAttack : Attack
{
    protected Player player;
    private bool slamStarted;

    protected void Start()
    {
        slamStarted = true;
        player = GlobalReference.GetReference<PlayerReference>().GetComponent<Player>();
        StartCoroutine(Slam());
    }

    private IEnumerator Slam()
    {
        Debug.Log(player.isGrounded);
        // while (true)
        //{
            player.rb.AddForce(Vector3.up * player.slamForce * 20, ForceMode.Impulse);
            yield return null;
        // }
    }

    void Exit()
    {
        Debug.Log("Slam Exit");
        player.isSlamming = false;
        player.SetState(player.states.Idle);
        Destroy(gameObject);
    }
}
