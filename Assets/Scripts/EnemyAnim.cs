using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class EnemyAnim : MonoBehaviour
{
    [SerializeField] private Animator character;
    private string run = "Run", hit = "Hit", idle = "Idle", death = "Death";

    public void CallIdleAnim()
    {
        character.SetBool(run, false);
        character.SetBool(death, false);
        character.SetBool(hit, false);
        character.SetBool(idle, true);
    }
    public void CallHitAnim()
    {
        character.SetBool(run, false);
        character.SetBool(death, false);
        character.SetBool(idle, false);
        character.SetBool(hit, true);
    }
    public void CallRunAnim()
    {
        character.SetBool(idle, false);
        character.SetBool(death, false);
        character.SetBool(hit, false);
        character.SetBool(run, true);
    }
    public void CallDeathAnim()
    {
        character.SetBool(run, false);
        character.SetBool(idle, false);
        character.SetBool(hit, false);
        character.SetBool(death, true);
    }
}
