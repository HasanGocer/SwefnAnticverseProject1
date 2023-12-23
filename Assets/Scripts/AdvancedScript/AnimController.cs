using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimController : MonoBehaviour
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip run, hit, idle, death;

    public void CallIdleAnim()
    {
        character.Play(idle, 0.2f);
    }
    public void CallHitAnim()
    {
        character.Play(hit, 0.2f);
    }
    public void CallRunAnim()
    {
        character.Play(run, 0.2f);
    }
    public void CallDeathAnim()
    {
        character.Play(death, 0.2f);
    }
}
