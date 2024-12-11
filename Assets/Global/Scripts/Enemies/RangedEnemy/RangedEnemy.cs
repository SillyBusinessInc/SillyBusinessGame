
using UnityEngine;
namespace EnemiesNS
{
    public class RangedEnemy : EnemyBase
    {
        // public float BulletsNeedtoShoot = 3;
        public Transform bulletSpawnPoint; 
        public GameObject bulletPrefab;
        public float shotInterval = 1f;
        protected override void SetupStateMachine()
        {
            states = new RangedStated(this);
            ChangeState(states.Idle);
        }

    }
}