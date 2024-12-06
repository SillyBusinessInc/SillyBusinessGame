using UnityEngine.InputSystem;

public class AttackingState : StateBase
{
    public AttackingState(Player player)
        : base(player) { }

    public override void Update() { }

    public void IncreaseIndex()
    {
        Player.Tail.attackIndex =
            Player.Tail.attackIndex >= Player.Tail.currentTail.currentCombo.Count - 1
                ? 0
                : ++Player.Tail.attackIndex;
    }

    public override void Enter()
    {
        Player.targetVelocity *= 0;
        Player.rb.linearVelocity *= 0;
        
        if (Player.Tail.activeCooldownTime >= Player.Tail.cooldownTime)
        {
            Player.Tail.activeCooldownTime = 0.0f;
        }
        else
        {
            Player.SetState(Player.states.Idle);
            return;
        }
        var tail = Player.Tail.currentTail;
        if (tail.currentCombo.Count == 0)
        {
            Player.SetState(Player.states.Idle);
            return;
        }
        if (Player.isGrounded)
        {
            if (Player.Tail.currentTail.currentCombo != Player.Tail.currentTail.groundCombo)
            {
                Player.Tail.attackIndex = 0;
            }
            Player.Tail.currentTail.currentCombo = Player.Tail.currentTail.groundCombo;
        }
        else
        {
            if (Player.Tail.currentTail.currentCombo != Player.Tail.currentTail.airCombo)
            {
                Player.Tail.attackIndex = 0;
            }
            Player.Tail.currentTail.currentCombo = Player.Tail.currentTail.airCombo;
        }
        var currentCombo = tail.currentCombo[Player.Tail.attackIndex];
        currentCombo.Start();
        Player.StartCoroutine(currentCombo.SetStateIdle());
        IncreaseIndex();
    }

    public override void Exit()
    {
        float animatorTailAttack = GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator.speed = 1.0f;
        Player.Tail.flipCanDoDamage = false;
        Player.Tail.tailCanDoDamage = false;
        Player.collidersEnemy.Clear();
        GlobalReference
            .GetReference<PlayerReference>()
            .GetComponent<Player>()
            .Tail.WaffleAnimator.speed = 1.0f;
    }

    public override void Attack(InputAction.CallbackContext ctx) { }
}
