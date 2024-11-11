using JetBrains.Annotations;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackingState : BaseState
{
    public float rotation;


    public AttackingState(Player player) : base(player)
    {
    }

    public override void Update()
    {
        if (rotation >= 360.0f)
        {
            player.SetState(new IdleState(player));
        }
        Turn();
    }

    void Turn()
    {
        for (int i = 0; i < player.turnSpeed; i++) //Forloop to make the rotation speed more smooth
        {
            if (rotation >= 360.0f)
            {
                return;
            }
            player.rotateLeft = rotation == 180.0f? !player.rotateLeft : player.rotateLeft;
            player.TransformTail.transform.RotateAround(player.TransformPlayer.position, Vector3.up, player.rotateLeft ? 1 : -1);
            rotation += 1;
        }
    }
}
