using System;
using UnityEngine;
using System.Collections.Generic;
public class Tail : MonoBehaviour
{
    public Player player;

    private List<Collider> colliders;
    public void Start()
    {
        colliders = new List<Collider>();
    }
    public void Update()
    {
        colliders = player.tailCanDoDamage? colliders : new List<Collider>();
    }
    public void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.CompareTag("Enemy"))
        {
            if(!colliders.Contains(Collider))
            {
                colliders.Add(Collider);
            }
            else
            {
                return;
            }
            if(player.tailCanDoDamage)
            {
                float actualDamage = player.tailDoDamage * player.playerStatistic.AttackDamageMultiplier.GetValue();
                Collider.GetComponent<EnemyBase>().OnHit((int)MathF.Round(actualDamage, 0));
            }
        }
    }
}