using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int impactAnim = Animator.StringToHash("Impact");
    private const float crossFadeDuration = 0.1f;

    private float duration = 1f;

    public PlayerImpactState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(impactAnim, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {

    }
}
