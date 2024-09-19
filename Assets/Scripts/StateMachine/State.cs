using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class State
{


    public abstract void Enter();

    public abstract void Tick(float deltaTime);

    public abstract void Exit();

    protected float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currInfo.IsTag("Attack"))
        {
            return currInfo.normalizedTime;
        }

        return 0f;
    }

}
