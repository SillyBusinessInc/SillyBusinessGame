using System.Collections;
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
            enemy.animator.SetBool("AttackIdle", true);
        }
        public override void Update()
        {
            base.Update();
            // Check if it's time to shoot
            if (currentTime > enemy.attackRecoveryTime && CheckingInRange())
            {
                currentTime = 0;
                canshoot = false;

                // Transition to attack state
                enemy.animator.SetBool("AttackIdle", false);
                enemy.animator.SetTrigger("AttackStart");

                // Wait for a specific point in the animation to fire the bullet
                enemy.StartCoroutine(HandleAttackAnimation());
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
                enemy.animator.SetBool("AttackIdle", false);
                currentAttack = 5000;
                enemy.inAttackAnim = false;
                CheckState();   
                return false;             
            }
            return true;
        }


        public override void Exit()
        {
            enemy.animator.SetBool("AttackIdle", false);
            base.Exit();
        }

        protected override void Attack()
        {
            if (CheckingInRange())
            {
                currentAttack += 1;

                // enemy.animator.SetTrigger("AttackStart");
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
        }
        private IEnumerator HandleAttackAnimation()
        {
            // Wait for the attack animation to reach a specific point
            while (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f)
            {
                yield return null;
            }

            // Fire the bullet
            Attack();

            // Wait for the end of the attack animation
            while (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }

            // Reset back to idle
            if (currentAttack < enemy.attacksPerCooldown)
            {
                enemy.animator.SetBool("AttackIdle", true);
            }
            canshoot = true;
        }


    }
}