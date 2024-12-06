using UnityEngine;

namespace EnemiesNS
{
    public class RangeAttackingState : BaseAttackingState
    {
        private RangedEnemy enemy;
        private float currentTime;
        private float stillNeedToShoot;
        public RangeAttackingState(RangedEnemy enemy) : base(enemy) { this.enemy = enemy; }

        public override void Enter()
        {
            enemy.animator.SetInteger("Attack_var", 0);
            enemy.animator.SetBool("Attack", true);
            base.Enter();
            stillNeedToShoot = enemy.BulletsNeedtoShoot;

            //spawn a prefab of the projectile
            
        }
        public override void Update()
        {
            base.Update();
            currentTime += Time.deltaTime;
            //dowhile loop to shoot the bullets
            while (stillNeedToShoot != enemy.BulletsNeedtoShoot)
            {
                if (currentTime > 1){
                    GameObject.Instantiate(enemy.bulletPrefab, enemy.bulletSpawnPoint.position, Quaternion.identity);
                    currentTime = 0;
                    stillNeedToShoot++;
                }
            }

                    

            // for (int i = 0; i < enemy.BulletsNeedtoShoot; i++){
            //     //wait 1 second before shooting
            //     if (currentTime > 1){
            //         GameObject.Instantiate(enemy.bulletPrefab, enemy.bulletSpawnPoint.position, Quaternion.identity);
            //         currentTime = 0;

            //     }
                
            // }
        }

        public override void Exit()
        {
            enemy.animator.SetBool("Attack", false);
            base.Exit();
        }

        protected override void Attack()
        {
            enemy.animator.SetTrigger("PlayAttack");
            base.Attack();
        }

    }
}