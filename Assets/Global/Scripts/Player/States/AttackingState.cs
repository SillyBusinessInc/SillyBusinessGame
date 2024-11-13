using JetBrains.Annotations;
using NUnit.Framework;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackingState : StateBase
{
    public bool rotateLeft;

    public float rotate;

    private bool isReturning;

    private bool turnLeft;

    public AttackingState(Player player) : base(player)
    {
    }

    public override void FixedUpdate()
    {
        Attack(Player.attackCounter);
    }
    void Attack(int attackCounter)
    {
        switch (attackCounter)
        {
            case 1:
                Slash();
                break;
            case 2:
                Slash();
                break;
            case 3:
                GroundPound();
                break;
        }
    }

    void GroundPound()
    {
        if (Player.isGrounded)
        {
            Player.isSlamming = true;
            Player.rb.AddForce(Vector3.up * Player.jumpForce, ForceMode.Impulse);
        }
        else if (Player.rb.linearVelocity.y < 0 && Player.isSlamming)
        {
            Player.rb.AddForce(Vector3.down * Player.jumpForce, ForceMode.Impulse);
        }
    }
    void Slash()
    {
        if (!isReturning)
        {
            if (rotate < 180)
            {
                Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, turnLeft ? Player.TailTurnSpeed : -Player.TailTurnSpeed);
                rotate += Player.TailTurnSpeed;
            }
            else
            {
                rotate = 0;
                isReturning = !isReturning;
                turnLeft = !turnLeft;
            }
        }
        else
        {
            if (rotate < 180)
            {
                Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, turnLeft ? Player.TailTurnSpeed : -Player.TailTurnSpeed);
                rotate += Player.TailTurnSpeed;
            }
            else
            {
                Player.SetState(Player.states.Idle);
            }
        }
    }

    public override void Enter()
    {
        ++Player.attackCounter;
        if (Player.attackCounter == 1)
        {
            turnLeft = false;
        }
        if (Player.attackCounter == 2)
        {
            turnLeft = true;
        }
        rotate = 0;
        isReturning = false;
    }

    public override void Exit()
    {
        Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, 0);
        Player.attackCounter = Player.attackCounter == 3 ? 0 : Player.attackCounter;
    }

    public override void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Player.isSlamming)
            {
                Player.isSlamming = false;
                Player.SetState(Player.states.Idle);
            }
        }
    }
}