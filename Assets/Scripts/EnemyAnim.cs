using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class EnemyAnim : MonoBehaviour
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip run, hit, idle, death;

    public void CallIdleAnim()
    {
        ResetAnim();
        character.Play(idle, 0.1f);
    }
    public void CallHitAnim()
    {
        ResetAnim();
        character.Play(hit, 0.1f);
    }
    public void CallRunAnim()
    {
        ResetAnim();
        character.Play(run, 0.1f);
    }
    public void CallDeathAnim()
    {
        ResetAnim();
        character.Play(death, 0.1f);
    }

    private void ResetAnim()
    {
    }
}
