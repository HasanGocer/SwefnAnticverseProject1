using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private Animator character;
    private bool runBool = false, hitBool = false, idleBool = false, deathBool = false, hitWithRunBool = false;
    private string run = "Run", hit = "Hit", idle = "Idle", death = "Death", hitWithRun = "HitWithRun";


    public void CallIdleAnim()
    {
        BoolOff();
        idleBool = true;
        AnimOn(idle);
        AnimOff(death, hit, run);
    }
    public void CallHitAnim()
    {
        hitBool = true;
        if (PerformDualAction())
            HitWithRunAnim();
        else
        {
            AnimOn(hit);
            BoolSelectedOff();
            AnimOff(death, idle, run);
        }
    }
    public void CallRunAnim()
    {
        runBool = true;
        if (PerformDualAction())
            HitWithRunAnim();
        else
        {
            AnimOn(run);
            BoolSelectedOff();
            AnimOff(death, idle, hit);
        }
    }
    public void CallDeathAnim()
    {
        BoolOff();
        deathBool = true;
        character.SetBool(hitWithRun, false);
        AnimOn(death);
        AnimOff(run, idle, hit);
    }
    public bool GetHitAnimBool()
    {
        return runBool;
    }

    public void SetRunBool(bool tempBool)
    {
        runBool = tempBool;
        PerformDualActionFinish();
    }
    public void SetHitBool(bool tempBool)
    {
        hitBool = tempBool;
        PerformDualActionFinish();
    }

    private void PerformDualActionFinish()
    {
        if (!runBool)
        {
            character.SetBool(hitWithRun, false);
            AnimOn(hit);
            AnimOff(death, idle, run);
        }
        else if (!hitBool)
        {
            character.SetBool(hitWithRun, false);
            AnimOn(run);
            AnimOff(death, idle, hit);
        }
    }
    private bool PerformDualAction()
    {
        if (runBool && hitBool)
            return true;
        else return false;
    }
    private void HitWithRunAnim()
    {
        character.SetBool(hit, false);
        character.SetBool(run, false);
        character.SetBool(hitWithRun, true);
    }
    private void AnimOn(string anim)
    {
        character.SetBool(anim, true);
    }
    private void AnimOff(string anim1, string anim2, string anim3)
    {
        character.SetBool(anim1, false);
        character.SetBool(anim2, false);
        character.SetBool(anim3, false);
    }
    private void BoolSelectedOff()
    {
        idleBool = false;
        deathBool = false;
    }
    private void BoolOff()
    {
        deathBool = false;
        idleBool = false;
        runBool = false;
        hitBool = false;
    }
}
