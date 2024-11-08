using UnityEngine;

public class WalkingState : BaseState
{
    public WalkingState(Player player) : base(player)
    {

    }

    public override void Update()
    {
        player.playerRb.AddForce(player.getDirection() * player.speed, ForceMode.Force);
        
        // player.transform.Translate(Vector3.forward * Time.deltaTime * player.speed * player.verticalInput);
        // player.transform.Translate(Vector3.right * Time.deltaTime * player.speed * player.horizontalInput);
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            player.SetState(new IdleState(player));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            player.SetState(new JumpingState(player));
        }
    }
}
