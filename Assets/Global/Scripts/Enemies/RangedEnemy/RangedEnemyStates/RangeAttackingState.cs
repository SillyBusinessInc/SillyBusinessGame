using UnityEngine;

namespace EnemiesNS
{
    public class RangeAttackingState : BaseAttackingState
    {
        private RangedEnemy enemy;
        private float currentTime;
        private float stillNeedToShoot = 0;
        public RangeAttackingState(RangedEnemy enemy) : base(enemy) { this.enemy = enemy; }

        public override void Enter()
        {
            // enemy.animator.SetInteger("Attack_var", 0);
            // enemy.animator.SetBool("Attack", true);
            base.Enter();
            currentTime = 0;
            stillNeedToShoot = 0;
            //spawn a prefab of the projectile
            
        }
        public override void Update()
        {
            base.Update();
            currentTime += Time.deltaTime;
            // Check if it's time to shoot
            if (currentTime > 1f && stillNeedToShoot < enemy.BulletsNeedtoShoot)
            {
                Debug.Log("Shooting");
                // Instantiate the bullet
                GameObject bullet = Object.Instantiate(enemy.bulletPrefab, enemy.bulletSpawnPoint.position, Quaternion.identity);
                
                // Assign the forward direction of the enemy to the bullet
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.bulletDirection = enemy.bulletSpawnPoint.forward;
                }

                // Reset the timer and increment the shot count
                currentTime = 0;
                stillNeedToShoot++;
            }

        }


        public override void Exit()
        {
            // enemy.animator.SetBool("Attack", false);
            base.Exit();
        }

        protected override void Attack()
        {
            // enemy.animator.SetTrigger("PlayAttack");
            base.Attack();
        }

    }
}