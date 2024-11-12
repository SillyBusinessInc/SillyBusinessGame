using UnityEngine;

public class AttackingState : StateBase
{
    private bool rotateLeft;

    private float rotate;

    private bool isReturning;

    public AttackingState(Player player) : base(player)
    {
    }
    
    public override void Enter()
    {
        rotate = 0;
        isReturning = false;
    }

    public override void FixedUpdate()
    {
        Turn();
    }

    void Turn()
    {
        if (!isReturning)
        {
            if (rotate < 180)
            {
                Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, Player.TurnSpeed);
                rotate += Player.TurnSpeed;
            }
            else
            {
                rotate = 0;
                isReturning = true;
            }
        }
        else
        {
            if (rotate < 180)
            {
                Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, -Player.TurnSpeed);
                rotate += Player.TurnSpeed;
            }
            else
            {
                Player.SetState(Player.states.Idle);
            }
        }
    }

    public override void Exit()
    {
        Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, 0);
    }
}