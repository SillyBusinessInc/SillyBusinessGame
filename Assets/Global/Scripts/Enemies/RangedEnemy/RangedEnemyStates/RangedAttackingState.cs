using UnityEngine;

namespace EnemiesNS
{
    public class RangedAttackingState : BaseAttackingState
    {
        private new RangedEnemy enemy;
        private float currentTime;
        private bool canshoot = true;
        private int currentAttack = 0;
        public RangedAttackingState(RangedEnemy enemy) : base(enemy) { this.enemy = enemy; }

        public override void Enter()
        {
            // enemy.animator.SetInteger("Attack_var", 0);
            // enemy.animator.SetBool("Attack", true);
            base.Enter();
            currentTime = 0;
            currentAttack = 0;
        }
        public override void Update()
        {
            base.Update();
            // Debug.Log(currentAttack);
            // Check if it's time to shoot
            if (currentTime > enemy.attackRecoveryTime && CheckingInRange() == true)
            {
                // Debug.Log(currentTime);
                // Debug.Log(enemy.attackRecoveryTime);
                // Debug.Log(enemy.attacksPerCooldown);
                currentTime = 0;
                canshoot = false;
                Attack();
                canshoot = true;
            }
            if (canshoot){
                currentTime += Time.deltaTime;
            }
            if (enemy.distanceToPlayer >= 2) {
                FacePlayer();
            }
        }
        public bool CheckingInRange(){
            if (!IsWithinAttackRange() || currentAttack >= enemy.attacksPerCooldown ){
                // Debug.Log("Not in range");
                currentAttack = 5000;
                enemy.inAttackAnim = false;
                CheckState();   
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
                currentAttack += 1;
                // Debug.Log(currentAttack);
                // Debug.Log(enemy.attacksPerCooldown);

                GameObject bullet = Object.Instantiate(enemy.bulletPrefab, enemy.bulletSpawnPoint.position, Quaternion.identity);
                
                // Assign the forward direction of the enemy to the bullet
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.bulletDirection = (GlobalReference.GetReference<PlayerReference>().SmoothCamaraTarget.transform.position - enemy.bulletSpawnPoint.position).normalized;
                }
                
                // Reset the timer and increment the shot count
                enemy.inAttackAnim = true;
                enemy.toggleIsRecovering(true);
                
            }
            // enemy.animator.SetTrigger("PlayAttack");
        }

    }
}