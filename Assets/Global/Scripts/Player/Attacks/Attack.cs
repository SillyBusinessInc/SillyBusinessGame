using System.Collections;
using UnityEngine;
using System;
public abstract class Attack : ScriptableObject
{
    public Attack(string Name, float damage, float cooldown)
    {
        this.Name = Name;
        this.damage = damage;
        this.cooldown = cooldown;
    }

    public string Name;
    public float cooldown;
    public float damage;
    public abstract void Start();

    public abstract IEnumerator SetStateIdle();
    public void ClipDuration(Animator animator, float targetDuration, string clip)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(var animationClip in clips)
        {
            Debug.Log(animationClip.name);    
        }
        AnimationClip clipToChange = Array.Find(clips, x => x.name == clip);
        animator.speed *= clipToChange.length / targetDuration;
    }

    
}
