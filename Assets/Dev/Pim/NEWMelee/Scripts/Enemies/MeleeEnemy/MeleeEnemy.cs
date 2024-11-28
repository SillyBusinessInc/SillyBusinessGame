using UnityEngine;

public class MeleeEnemy : EnemiesNS.EnemyBase
{
    [Header("Melee Character Specific settings")]
    [Tooltip("Reference to the weapon's hitbox")]
    public Collider weapon;
    private bool playerHit = false;


    //
    // called in animations as events
    //
    public void EnableWeaponHitBox()
    {
        weapon.enabled = true;
        // Debug.Log($"weapon.enabled: {weapon.enabled}");
    }

    public void DisableWeaponHitBox()
    {
        weapon.enabled = false;
        playerHit = false;
        // Debug.Log($"weapon.enabled: {weapon.enabled}");
    }

    public override void PlayerHit(PlayerObject playerObject, int damage)
    {
        if (playerHit) return;
        playerHit = true;
        base.PlayerHit(playerObject, damage);
    }
}
