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
        [Tooltip("For how long the enemy will idle before roaming to new position")]
        [SerializeField]
        [Range(0f, 300f)]
        protected float idleTime = 0.5f;
        [Tooltip("Idle variance allows to fine tune the randomisation of the idle time, 0.5 means idle variance will be between 50% and 150% of the given idle time.")]
        [SerializeField]
        [Range(0f, 1f)]
        protected float idleVariance = 0.5f;

        [Header("Base roam settings | ignored on moldcores")]
        [Tooltip("How far this enemy will travel from it's original spawn while roaming | ignored on moldcores")]
        [SerializeField]
        [Range(0f, 250f)]
        protected float roamRange = 15f;
        [HideInInspector]
        protected Vector3 spawnPos;
        [HideInInspector]
        protected Vector3 roamDestination;

        [Header("Base chase settings | ignored on moldcores")]
        [Tooltip("How close the target needs to get before triggering the chasing behavior")]
        [SerializeField]
        [Range(0f, 250f)]
        protected float chaseRange = 25f;

        [Header("Base attack settings | ignored on moldcores")]
        [Tooltip("The range of the attack")]
        [SerializeField]
        [Range(5f, 250f)]
        protected float attackRange = 2f;
        [Tooltip("The time between attacking states")]
        [SerializeField]
        [Range(5f, 300f)]
        protected float attackCooldown = 2f;
        [Tooltip("The base damage of the attack")]
        [SerializeField]
        [Range(0, 10)]
        protected int attackDamage = 1;

        [Header("References")]
        [Tooltip("OPTIONAL: Reference to the target's Transform. Default: Player")]
        [SerializeField]
        protected Transform target;
        [Tooltip("OPTIONAL: Reference to the Animator of this enemy. Has Default")]
        [SerializeField]
        protected Animator animator;
        [Tooltip("OPTIONAL: Reference to the NavMeshAgent of this enemy. Has Default")]
        [SerializeField]
        protected NavMeshAgent agent;

        [Header("States")]
        [HideInInspector]
        protected BaseStates states;
        [HideInInspector]
        protected StateBase currentState;

        [Header("DEBUGGING")]
        [Tooltip("DO NOT SET | shows the current state's name")]
        [SerializeField]
        protected string currentStateName;


        protected void Start()
        {
            spawnPos = this.transform.position;
            setReferences();
            SetupStateMachine();
            GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);

            if (target) Debug.Log("target", target); //TODO: Delete me
        }

        protected void Update() => currentState?.Update();
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
                    target = GlobalReference.GetReference<PlayerReference>().GetComponent<Transform>();
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

        //
        // When creating your own enemy, override this to use your enemy specific BaseStates class. And set the set to your desired default state.
        //
        protected virtual void SetupStateMachine()
        {
            states = new BaseStates(this);
            ChangeState(states.Idle);
        }
    }
}
