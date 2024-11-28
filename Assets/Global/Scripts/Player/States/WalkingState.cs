using UnityEngine;

public class WalkingState : StateBase
{
    public WalkingState(Player player) : base(player) {}

    public override void Update()
    {
        if (!Player.isGrounded) {
            Player.StartCoroutine(Player.SetStateAfter(Player.states.Falling, Player.coyoteTime));
            return;
        }

        if (Player.GetDirection() != Vector3.zero) Player.currentWalkingPenalty += Player.acceleration * Time.deltaTime;
        else Player.currentWalkingPenalty -= Player.acceleration * Time.deltaTime;
        Player.currentWalkingPenalty = Mathf.Clamp(Player.currentWalkingPenalty, Player.maxWalkingPenalty, 1);

        Vector3 newTargetVelocity = Player.currentWalkingPenalty * Player.playerStatistic.Speed.GetValue() * new Vector3(Player.GetDirection().x, 0, Player.GetDirection().z);

        Vector3 referenceVector = Player.rb.linearVelocity.normalized * newTargetVelocity.magnitude;

        Player.targetVelocity = new Vector3(newTargetVelocity.x, referenceVector.y, newTargetVelocity.z);

        // if (!Player.isGrounded && Time.time > Player.timeLeftGrounded + Player.coyoteTime) {
        //     Player.SetState(Player.states.Falling);  
        // }
        // else if (Player.rb.linearVelocity == Vector3.zero) Player.SetState(Player.states.Idle);
    }

}
