using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int speedParameter = Animator.StringToHash("Speed");
    private readonly int locomotionBlendTree = Animator.StringToHash("Locomotion");

    private const float crossFadeDuration = 0.1f;
    private const float animatorDampTime = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Animator.CrossFadeInFixedTime(locomotionBlendTree, crossFadeDuration);
        
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (IsInChaseRange())
        {
            StateMachine.SwitchState(new EnemyChasingState(StateMachine));
            return;
        }

        FacePlayer();

        StateMachine.Animator.SetFloat(speedParameter, 0, animatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }
}
