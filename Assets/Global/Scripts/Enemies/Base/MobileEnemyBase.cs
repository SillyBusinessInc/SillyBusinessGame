using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace EnemiesNS
{
    public class MobileEnemyBase : EnemyBase
    {
        [Header("Base idle settings")]
        [Tooltip("For how long the enemy will idle before roaming to new position. NOTE: this is the base value, there will be randomisation applied to make it the idling seem more natural")]
        [SerializeField]
        [Range(0f, 300f)]
        public float idleTime = 0.5f;

        [Tooltip("Idle variance allows to fine tune the randomisation of the idle time, 0.5 means idle variance will be between 50% and 150% of the given idle time.")]
        [SerializeField]
        [Range(0f, 1f)]
        public float idleVariance = 0.5f;

        [HideInInspector] public float idleWaitElapsed;
        [HideInInspector] public float idleWaitTime;
        [HideInInspector] public bool isIdling = false;

        [Header("Base roam settings")]
        [Tooltip("How far this enemy will travel from it's original spawn while roaming | ignored on moldcores")]
        [SerializeField]
        [Range(0f, 250f)]
        public float roamRange = 15f;

        [Tooltip("Movement speed while roaming")]
        [SerializeField]
        [Range(0f, 250f)]
        public float roamingSpeed = 3.5f;

        [Tooltip("Acceleration while roaming")]
        [SerializeField]
        [Range(0f, 250f)]
        public float roamingAcceleration = 8f;


        [HideInInspector] public Vector3 roamDestination;

        [Header("Base chase settings")]
        [Tooltip("How close the target needs to get before triggering the chasing behavior")]
        [SerializeField]
        [Range(0f, 250f)]
        public float chaseRange = 25f;

        [Tooltip("Movement speed while chasing")]
        [SerializeField]
        [Range(0f, 250f)]
        public float chaseSpeed = 3.5f;

        [Tooltip("Acceleration while chasing")]
        [SerializeField]
        [Range(0f, 250f)]
        public float chaseAcceleration = 8f;

        [Tooltip("the minimum distance to keep from the player.")]
        [SerializeField]
        [Range(0f, 5f)]
        public float minDistanceToPlayer = 2f;

        [Tooltip("Time for the chasing enemy to hold position once it gets into attackingrange but still on attack cooldown. Used to keep the enemy from hugging the player.")]
        [SerializeField]
        [Range(0f, 5f)]
        public float chaseWaitTime = 1f;

        [HideInInspector] public float chaseWaitElapsed = 0;
        [HideInInspector] public bool isChasing = false;
        [HideInInspector] public bool isWaiting = false;
        [HideInInspector] public float distanceToPlayer;

        [Header("Base attack settings")]
        [Tooltip("The range of the attack")]
        [SerializeField]
        [Range(0f, 250f)]
        public float attackRange = 2f;

        [Tooltip("The time between attacking states")]
        [SerializeField]
        [Range(0f, 300f)]
        public float attackCooldown = 2f;

        [Tooltip("The amount of time this character will have to recover from attacking, and be standing still before able to attack again. NOTE: if this is less than the attack clip length, there will be no additional waiting time applied.")]
        [SerializeField]
        [Range(0f, 10f)]
        public float attackRecoveryTime = 0.3f;

        [Tooltip("number of attacks before triggering cooldown.")]
        [SerializeField]
        [Range(1, 10)]
        public int attacksPerCooldown = 1;

        [Tooltip("The base damage of the attack")]
        [SerializeField]
        [Range(0, 10)]
        public int attackDamage = 1;

        [Tooltip("The angle the enemy can be off while trying to face the player")]
        [SerializeField]
        [Range(0f, 180f)]
        public float facingPlayerVarianceAngle = 5f;

        [Tooltip("The amount of knockback this enemy's attacks will apply")]
        [SerializeField]
        [Range(0f, 100f)]
        public float attackKnockback;

        [Tooltip("The time the hit object will be stunned due to knockback")]
        [SerializeField]
        [Range(0f, 5f)]
        public float knockbackStunTime = 0.5f;

        [HideInInspector]
        public bool canAttack = true;
        [HideInInspector]
        public bool isRecovering = false;
        [HideInInspector]
        public float attackCooldownElapsed = 0;
        [HideInInspector]
        public float attackRecoveryElapsed = 0;
        [HideInInspector]
        public bool inAttackAnim = false;

        [Header("References")]
        [Tooltip("Reference to the Animator of this enemy.")]
        [SerializeField]
        public Animator animator;
        [HideInInspector]
        public int VFXLayer;

        [Tooltip("Reference to the NavMeshAgent of this enemy.")]
        [SerializeField]
        public NavMeshAgent agent;

        [Tooltip("Reference to this Enemy's walking particle system")]
        public ParticleSystem particleSystemWalk;

        [Header("States")]
        [HideInInspector] public BaseStates states;
        [HideInInspector] public StateBase currentState;

        [Header("DEBUGGING")]
        [Tooltip("DO NOT SET | shows the current state's name")]
        [SerializeField]
        protected string currentStateName;

        [Tooltip("DO NOT SET | shows the current state's name")]
        [SerializeField]
        protected bool agentIsStopped = false;

        protected override void Start()
        {
            base.Start();
            if (animator)
            {
                VFXLayer = animator.GetLayerIndex("VFX");
            }
            SetupStateMachine();
        }

        protected override void Update()
        {
            base.Update();
            currentState?.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            currentState?.FixedUpdate();
        }


        public override void OnHit(int damage)
        {
            base.OnHit(damage);
            if (!animator) return;
            if (!inAttackAnim) animator.SetTrigger("PlayDamage");
        }
        protected override void OnHitSlot()
        {
            base.OnHitSlot();
            if (animator) animator.SetTrigger("PlayDamageFlash");
        }
        protected override void OnDeath()
        {
            HealthBarDestroy = true;
            ChangeState(states.Dead);
        }
        public override void UpdateTimers()
        {
            // increment timers
            float elapsedTime = Time.deltaTime;
            if (isIdling) idleWaitElapsed += elapsedTime;
            if (isWaiting) chaseWaitElapsed += elapsedTime;
            if (!canAttack) attackCooldownElapsed += elapsedTime;
            if (isRecovering) attackRecoveryElapsed += elapsedTime;

            // check flags
            if (idleWaitElapsed >= idleWaitTime) toggleIsIdling(false);
            if (chaseWaitElapsed >= chaseWaitTime) toggleIsWaiting(false);
            if (attackCooldownElapsed >= attackCooldown) toggleCanAttack(true);
            if (attackRecoveryElapsed >= attackRecoveryTime) toggleIsRecovering(false);
        }

        public void ChangeState(StateBase state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
            currentStateName = state.GetType().Name;
        }

        public virtual void PlayerHit(PlayerObject playerObject, int damage)
        {
            Player player = playerObject.GetComponentInParent<Player>();
            if (!player) return;
            player.OnHit(damage);
            player.ApplyKnockback(CalculatedKnockback(playerObject), knockbackStunTime);
        }
        public virtual Vector3 CalculatedKnockback(PlayerObject playerObject)
        {
            Vector3 directionToPlayer = playerObject.transform.position - transform.position;
            directionToPlayer.y = 0;
            directionToPlayer.Normalize();

            return directionToPlayer * attackKnockback;
        }

        // AnimEvents
        public override void AttackAnimStarted()
        {
            inAttackAnim = true;
        }
        public override void AttackAnimEnded()
        {
            toggleIsRecovering(true);
            inAttackAnim = false;
        }

        public override void DeathAnimEnded()
        {
            // animator is on the Model's GameObject, so we can reach that GameObject through this.
            if (animator)
            {
                animator.gameObject.SetActive(false);
            }

            PlayDeathParticleEffect();
        }


        // toggle methods
        public void toggleCanAttack(bool v)
        {
            canAttack = v;
            if (canAttack) attackCooldownElapsed = 0f;
        }

        public void toggleIsRecovering(bool v)
        {
            isRecovering = v;
            if (!isRecovering) attackRecoveryElapsed = 0f;
        }

        public void toggleIsIdling(bool v)
        {
            isIdling = v;
            if (!isIdling) idleWaitElapsed = 0f;
        }

        public void toggleIsWaiting(bool v)
        {
            isWaiting = v;
            if (!isWaiting) chaseWaitElapsed = 0f;
        }

        //
        // When creating your own enemy, override this to use your enemy specific BaseStates class. And set the set to your desired default state.
        //
        protected virtual void SetupStateMachine()
        {
            states = new BaseStates(this);
            ChangeState(states.Idle);
        }

        void OnDrawGizmos()
        {
            // Draw the Chase Range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            // Draw the Minimum Distance from Player
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, minDistanceToPlayer);

            // Draw the Attack Range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            // Draw the Roam Range around the spawn position
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(spawnPos, roamRange);
        }

    }
}
