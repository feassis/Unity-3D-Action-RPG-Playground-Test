using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int impactAnim = Animator.StringToHash("Impact");
    private const float crossFadeDuration = 0.1f;

    private float duration = 1f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        StateMachine.Animator.CrossFadeInFixedTime(impactAnim, crossFadeDuration);    
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
        }
    }

    public override void Exit()
    {
        
    }

    
}
