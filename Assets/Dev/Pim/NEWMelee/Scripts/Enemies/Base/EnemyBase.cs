using UnityEngine;
using UnityEngine.AI;

namespace EnemiesNS
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Base Enemy Fields")]
        [Tooltip("HP for this enemy: integer")]
        [SerializeField]
        [Range(0, 1000)]
        protected int health = 100;

        [Header("Base idle settings | ignored on moldcores ")]
        [Tooltip("For how long the enemy will idle before roaming to new position. NOTE: this is the base value, there will be randomisation applied to make it the idling seem more natural")]
        [SerializeField]
        [Range(0f, 300f)]
        public float idleTime = 0.5f;
        [Tooltip("Idle variance allows to finetune the randomisation of the idle time, 0.5 means idle variance will be between 50% and 150% of the given idle time.")]
        [SerializeField]
        [Range(0f, 1f)]
        public float idleVariance = 0.5f;
        [HideInInspector]
        public float idleWaitElapsed;
        [HideInInspector]
        public float idleWaitTime;
        //[HideInInspector]
        public bool isIdling = false;

        [Header("Base roam settings | ignored on moldcores")]
        [Tooltip("How far this enemy will travel from it's original spawn while roaming | ignored on moldcores")]
        [SerializeField]
        [Range(0f, 250f)]
        public float roamRange = 15f;
        [HideInInspector]
        public Vector3 spawnPos;
        [HideInInspector]
        public Vector3 roamDestination;

        [Header("Base chase settings | ignored on moldcores")]
        [Tooltip("How close the target needs to get before triggering the chasing behavior")]
        [SerializeField]
        [Range(0f, 250f)]
        public float chaseRange = 25f;
        [Tooltip("Time for the chasing enemy to hold position once it gets into attackingrange but still on attack cooldown. Used to keep the enemy from hugging the player.")]
        [SerializeField]
        [Range(0f, 5f)]
        public float chaseWaitTime = 1f;
        //[HideInInspector]
        public bool isChasing = false;
        [HideInInspector]
        public bool isWaiting = false;

        [Header("Base attack settings | ignored on moldcores")]
        [Tooltip("The range of the attack")]
        [SerializeField]
        [Range(5f, 250f)]
        public float attackRange = 2f;
        [Tooltip("The time between attacking states")]
        [SerializeField]
        [Range(5f, 300f)]
        public float attackCooldown = 2f;
        [Tooltip("The base damage of the attack")]
        [SerializeField]
        [Range(0, 10)]
        public int attackDamage = 1;
        //[HideInInspector]
        public bool canAttack = true;
        [HideInInspector]
        public float attackCooldownElapsed = 0;

        [Header("References")]
        [Tooltip("OPTIONAL: Reference to the target's Transform. Default: Player")]
        [SerializeField]
        public Transform target;
        [Tooltip("OPTIONAL: Reference to the Animator of this enemy. Has Default")]
        [SerializeField]
        public Animator animator;
        [Tooltip("OPTIONAL: Reference to the NavMeshAgent of this enemy. Has Default")]
        [SerializeField]
        public NavMeshAgent agent;

        [Header("States")]
        [HideInInspector]
        public BaseStates states;
        [HideInInspector]
        public StateBase currentState;

        [Header("DEBUGGING")]
        [Tooltip("DO NOT SET | shows the current state's name")]
        [SerializeField]
        protected string currentStateName;
        [Tooltip("DO NOT SET | shows the current state's name")]
        [SerializeField]
        protected bool agentIsStopped = false;



        protected void Start()
        {
            spawnPos = this.transform.position;
            setReferences();
            SetupStateMachine();
            GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);

            if (target) Debug.Log("target", target); //TODO: Delete me
        }

        protected void Update()
        {
            agentIsStopped = agent.isStopped;
            UpdateTimers();
            currentState?.Update();
        }
        protected void FxedUpdate() => currentState?.FixedUpdate();

        public void OnHit(int damage)
        {
            health -= damage;
            Debug.Log($"Enemy health: {health}", this);
            if (health <= 0)
            {
                OnDeath();
            }
        }

        protected void OnDeath()
        {
            ChangeState(states.Dead);
            GlobalReference.AttemptInvoke(Events.ENEMY_KILLED);
        }

        public void ChangeState(StateBase state)
        {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
            currentStateName = state.GetType().Name;
        }

        protected void setReferences()
        {
            if (!target)
            {
                try
                {
                    target = GlobalReference.GetReference<PlayerReference>().PlayerObj.transform;
                }
                catch { }
            }
            if (!agent)
            {
                agent = this.GetComponent<NavMeshAgent>();
            }
            if (!animator)
            {
                animator = this.GetComponent<Animator>();
            }
        }

        public void toggleCanAttack(bool v)
        {
            canAttack = v;
            if (canAttack) attackCooldownElapsed = 0f;
        }

        public void toggleIsIdling(bool v)
        {
            isIdling = v;
            if (!isIdling) idleWaitElapsed = 0f;
        }

        public void FreezeMovement(bool v)
        {
            agent.isStopped = v;
        }

        public void UpdateTimers()
        {
            // increment timers
            if (!canAttack) attackCooldownElapsed += Time.deltaTime;
            if (isIdling) idleWaitElapsed += Time.deltaTime;

            // check flags
            if (attackCooldownElapsed >= attackCooldown) toggleCanAttack(true);
            if (idleWaitElapsed >= idleWaitTime) toggleIsIdling(false);
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

            // Draw the Attack Range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            // Draw the Roam Range around the spawn position
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(spawnPos, roamRange);
        }
    }
}
