using System.Collections;
using UnityEngine;

namespace EnemiesNS
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Base Enemy Fields")]
        [Tooltip("HP for this enemy: integer")]
        [SerializeField]
        [Range(0, 1000)]
        public int health = 100;
        [HideInInspector]
        public int maxHealth;
        public GameObject HealthBarPrefab;
        [HideInInspector]
        public bool HealthBarDestroy = false;

        [HideInInspector] public Vector3 spawnPos;

        [Header("Death Particle Effect settings")]
        [Tooltip("Reference to the Enemies Particle Death Prefab")]
        [SerializeField]
        protected ParticleSystem PrefabDeathParticles;
        [Tooltip("Origin point for the death particle effect")]
        [SerializeField]
        protected Transform DeathParticleOrigin;

        protected ParticleSystem particleSystemDeath;

        [Header("References")]
        [Tooltip("Reference to the player.")]
        [HideInInspector]
        public Transform target;


        protected virtual void Start()
        {
            GlobalReference.AttemptInvoke(Events.ENEMY_SPAWNED);
            maxHealth = health;
            spawnPos = this.transform.position;
            try
            {
                target = GlobalReference.GetReference<PlayerReference>().PlayerObj.transform;
            }
            catch { }
            if (!DeathParticleOrigin)
            {
                Debug.LogWarning("NULLREFERENCE: Death Paricle Origin not set. This will result in malfunctioning OnDeath() behavior.", this);
            }
        }

        protected virtual void Update()
        {
            UpdateTimers();
        }

        protected virtual void FixedUpdate() { }

        public virtual void OnHit(int damage)
        {
            if (HealthBarPrefab)
            {
                HealthBarPrefab.SetActive(true);
            }

            health -= damage;

            // allows room to add behavior / logic between the damage calculation and the death check
            OnHitSlot();

            // death check
            if (health <= 0)
            {
                OnDeath();
                return;
            }

        }

        protected virtual void OnHitSlot() { }

        protected virtual void OnDeath()
        {
            HealthBarDestroy = true;
            PlayDeathParticleEffect();
        }

        protected virtual void OnDestroy()
        {
            GlobalReference.AttemptInvoke(Events.ENEMY_KILLED);
        }

        public virtual void FreezeMovement(bool v) { }
        public virtual void UpdateTimers() { }

        protected void PlayDeathParticleEffect()
        {
            // Instantiate and play the death particle effect
            if (!DeathParticleOrigin) return;
            particleSystemDeath = Instantiate(PrefabDeathParticles, DeathParticleOrigin);
            particleSystemDeath.Play();

            // Start a coroutine to destroy the particle system and the enemy once the particles finish playing
            StartCoroutine(DestroyAfterParticles(particleSystemDeath));
        }
        protected IEnumerator DestroyAfterParticles(ParticleSystem particles)
        {
            if (particles != null)
            {
                // Wait until the particle system finishes playing
                yield return new WaitWhile(() => particles.isPlaying);

                // Destroy the particle system
                Destroy(particles.gameObject);
            }

            // Destroy the enemy object
            Destroy(this.gameObject);
        }

        //
        // Used as events in animations
        //
        public virtual void EnableWeaponHitBox() { }
        public virtual void DisableWeaponHitBox() { }
        public virtual void AttackAnimStarted() { }
        public virtual void AttackAnimEnded() { }
        public virtual void DeathAnimEnded() { }
    }
}
