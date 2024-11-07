using System;
using UnityEngine;

public class GlidingState : BaseState
{
    public GlidingState(Player player) : base(player)
    {
    }
    public override void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.SetState(new FallingState(player));
        }
        player.transform.Translate(0.5f * Vector3.forward * Time.deltaTime * player.speed * player.horizontalInput);
        player.transform.Translate(0.5f * Vector3.left * Time.deltaTime * player.speed * player.verticalInput);
    }

    public override void Enter()
    {
        player.playerRb.linearDamping = player.Drag;
    }

    public override void Exit()
    {
        player.playerRb.linearDamping = 0f;
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