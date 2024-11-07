using UnityEngine;

public class WalkingState : BaseState
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        player.transform.Translate(Vector3.forward * Time.deltaTime * player.speed * player.horizontalInput);
        player.transform.Translate(Vector3.left * Time.deltaTime * player.speed * player.verticalInput);
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            player.SetState(new IdleState(player));
        }

    }
}
