using UnityEngine;

public class Walking : BaseMovement
{
    public Walking(Player player) : base(player)
    {
        
    }

    public override void OnAttack()
    {
        player.SetState(new Attacking(player));
    }

    public override void OnWalk()
    {
        //player stop walking go to idle
    }
}
