using UnityEngine;

public class FallingState : BaseState
{
    public FallingState(Player player) : base(player)
    {
    }
    
    public override void Update()
    {
        player.transform.Translate(0.5f* Vector3.forward * Time.deltaTime * player.speed * player.horizontalInput);
        player.transform.Translate(0.5f* Vector3.left * Time.deltaTime * player.speed * player.verticalInput);
        
        if(Input.GetKeyDown(KeyCode.Space) && player.doubleJumps > player.currentJumps)
        {
            player.SetState(new JumpingState(player));
            player.currentJumps += 1; 
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            player.SetState(new GlidingState(player));
        }
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.SetState(new IdleState(player));
            player.currentJumps = 0;
        }
    }
}
