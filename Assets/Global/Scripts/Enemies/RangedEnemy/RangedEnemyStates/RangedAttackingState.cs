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
        private bool isAttacking = false; // Tracks if currently in attack animation

        public override void Update()
        {
            base.Update();

            // If attacking, handle the animation progress
            if (isAttacking)
            {
                HandleAttackProgress();
                return; // Exit Update while handling attack animation
            }

            // Check if the enemy can attack
            if (canshoot && currentTime > enemy.attackRecoveryTime && CheckingInRange())
            {
                Debug.Log("Attack");
                StartAttack();
            }

            // Increment recovery time if not attacking
            if (canshoot)
            {
                currentTime += Time.deltaTime;
            }

            // Face the player if needed
            if (enemy.distanceToPlayer >= 2)
            {
                FacePlayer();
            }
        }

        private void StartAttack()
        {
            currentTime = 0;
            canshoot = false;
            isAttacking = true;

            // Trigger the attack animation
            enemy.animator.SetBool("AttackIdle", false);
            enemy.animator.SetTrigger("AttackStart");
        }

        private void HandleAttackProgress()
        {
            var stateInfo = enemy.animator.GetCurrentAnimatorStateInfo(0);

            // Wait for the animation to reach the bullet firing point
            if (stateInfo.normalizedTime >= 0.35f && stateInfo.normalizedTime < 1f && !enemy.inAttackAnim)
            {
                Attack();
                enemy.inAttackAnim = true; // Ensure bullet fires only once
            }

            // Wait for the animation to finish
            if (stateInfo.normalizedTime >= 1f)
            {
                EndAttack();
            }
        }

        private void EndAttack()
        {
            isAttacking = false; // Reset attack state
            enemy.inAttackAnim = false;

            if (currentAttack < enemy.attacksPerCooldown)
            {
                enemy.animator.SetBool("AttackIdle", true);
            }

            canshoot = true; // Allow for next attack
        }

        public bool CheckingInRange()
        {
            if (!IsWithinAttackRange() || currentAttack >= enemy.attacksPerCooldown)
            {
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
                canshoot = true;

            }
        }
        private IEnumerator HandleAttackAnimation()
        {
            if (!CheckingInRange())
            {
                yield return null;
            }
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