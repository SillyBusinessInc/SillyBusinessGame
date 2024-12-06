using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class TailStatistic
{
    public Statistic slamObjectSize = new(1.0f);
    public Statistic increaseTailSpeed = new(1.0f);

    public Statistic increaseAttackSpeed = new(1.0f);
    public Statistic comboResetTime = new(2.0f);
    public Statistic leftTailDamage = new(10.0f);
    public Statistic rightTailDamage = new(15.0f);
    public Statistic flipTailDamage = new(20.0f);

    public Statistic leftTailCooldown = new(0.0f);
    public Statistic rightTailCooldown = new(0.0f);
    public Statistic flipTailCooldown = new(0.0f);

    public Statistic increaseDamage = new(1.0f);
}
