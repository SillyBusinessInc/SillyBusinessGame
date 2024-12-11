using UnityEngine;

public enum Events
{
    #region general
    PICKUP_COLLECTED,
    ROOM_FINISHED,
    DROP_TREASURE_EVENT,
    #endregion
    
    #region enemy
    ENEMY_KILLED,
    ENEMY_SPAWNED,
    #endregion
    
    #region waves
    ALL_ENEMIES_DEAD,
    NORMAL_WAVE_DONE,
    NORMAL_WAVE_START,
    SPAWN_WAVE,
    ALL_WAVES_DONE,
    MOLD_CORE_SPAWNED,
    MOLD_CORE_KILLED,
    NEXT_SPAWNER,
    ALL_NEXT_SPAWNERS_DONE,
    #endregion

    #region stats
    CRUMBS_CHANGED,
    HEALTH_CHANGED,
    STATISTIC_CHANGED,
    #endregion

    #region player attack
    PLAYER_ATTACK_STARTED,
    PLAYER_ATTACK_ENDED,
    #endregion
}
