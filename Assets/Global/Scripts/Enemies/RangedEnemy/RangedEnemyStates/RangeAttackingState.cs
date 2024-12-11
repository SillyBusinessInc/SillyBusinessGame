using UnityEngine;

namespace EnemiesNS
{
    public class RangeAttackingState : BaseAttackingState
    {
        private RangedEnemy enemy;
        private float currentTime;
        public RangeAttackingState(RangedEnemy enemy) : base(enemy) { this.enemy = enemy; }

        public override void Enter()
        {
            // enemy.animator.SetInteger("Attack_var", 0);
            // enemy.animator.SetBool("Attack", true);
            base.Enter();
            currentTime = 0;
            attacksThisState = 1;
        }
        public override void Update()
        {
            base.Update();
            currentTime += Time.deltaTime;
            // Check if it's time to shoot
            if (currentTime > enemy.attackRecoveryTime && CheckingInRange() == true )
            {
                Attack();
            }
            if (enemy.distanceToPlayer >= 2) {
                FacePlayer();
            }
        }
        public bool CheckingInRange(){
            if (!IsWithinAttackRange() || attacksThisState >= enemy.attacksPerCooldown){
                attacksThisState +=1000;
                enemy.inAttackAnim = false;   
                enemy.toggleCanAttack(false);
                return false;             
            }
            return true;
        }


        public override void Exit()
        {
            // enemy.animator.SetBool("Attack", false);
            base.Exit();
        }

        protected override void Attack()
        {
            if (CheckingInRange())
            {
                GameObject bullet = Object.Instantiate(enemy.bulletPrefab, enemy.bulletSpawnPoint.position, Quaternion.identity);
                
                // Assign the forward direction of the enemy to the bullet
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.bulletDirection = (GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget.transform.position - enemy.bulletSpawnPoint.position).normalized;
                }
                
                // Reset the timer and increment the shot count
                currentTime = 0;
                // attacksThisState++;
                enemy.inAttackAnim = true;
                attacksThisState += 1;
                enemy.toggleIsRecovering(true);
            }
            // enemy.animator.SetTrigger("PlayAttack");
        }

    }
}