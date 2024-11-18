using UnityEngine;


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
            Player.rb.AddForce(Vector3.up * Player.playerStatistic.JumpForce.GetValue(), ForceMode.Impulse);
        }
        else if (Player.rb.linearVelocity.y < 0 && Player.isSlamming)
        {
            Player.rb.AddForce(Vector3.down * Player.playerStatistic.JumpForce.GetValue(), ForceMode.Impulse);
        }
        Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
    }
    void Slash()
    {
        if (!isReturning)
        {
            if (rotate < 180)
            {
                Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, turnLeft ? Player.playerStatistic.AttackSpeedMultiplier.GetValue() : -Player.playerStatistic.AttackSpeedMultiplier.GetValue());
                rotate += Player.playerStatistic.AttackSpeedMultiplier.GetValue();
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
                Player.TransformTail.transform.RotateAround(Player.rb.position, Vector3.up, turnLeft ? Player.playerStatistic.AttackSpeedMultiplier.GetValue() : -Player.playerStatistic.AttackSpeedMultiplier.GetValue());
                rotate += Player.playerStatistic.AttackSpeedMultiplier.GetValue();
            }
            else
            {
                Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
            }
        }
    }

    public override void Enter()
    {
        ++Player.attackCounter;
        if (Player.attackCounter == 1)
        {
            Player.tailCanDoDamage = true;
            Player.tailDoDamage = Player.firstTailDamage;
            turnLeft = false;
        }
        if (Player.attackCounter == 2)
        {
            Player.tailCanDoDamage = true;
            Player.tailDoDamage = Player.secondTailDamage;
            turnLeft = true;
        }
        if (Player.attackCounter == 3)
        {
            Player.slamCanDoDamage = true;
        }
        rotate = 0;
        isReturning = false;
    }

    public override void Exit()
    {
        Player.tailCanDoDamage = false;
        Player.slamCanDoDamage = false;
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
                Player.SetState(Player.movementInput.magnitude > 0 ? Player.states.Walking : Player.states.Idle);
            }
        }
    }
}