using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int targetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    private readonly int targetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int targetingRightHash = Animator.StringToHash("TargetingRight");


    private const float crossFadeDuration = 0.1f;
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.GetAnimator().CrossFadeInFixedTime(targetingBlendTree, crossFadeDuration);
        stateMachine.GetInputReader().OnCancelTargetEvent += OnCancelTarget;
        stateMachine.GetInputReader().OnPrimaryAttackButtomPressed += OnPrimaryAttackPressed;
    }

    

    public override void Tick(float deltaTime)
    {

        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargetingSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }


    public override void Exit()
    {
        stateMachine.GetInputReader().OnCancelTargetEvent -= OnCancelTarget;
        stateMachine.GetInputReader().OnPrimaryAttackButtomPressed -= OnPrimaryAttackPressed;
    }

    private void OnPrimaryAttackPressed(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        stateMachine.SwitchState(new PlayerPrimaryAttackState(stateMachine, 0));
    }

    private void OnCancelTarget()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private void UpdateAnimator(float deltaTime)
    {
        var movementValue = stateMachine.GetInputReader().MovementValue;

        if (movementValue.y == 0)
        {
            stateMachine.GetAnimator().SetFloat(targetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = movementValue.y > 0 ? 1f : -1f;
            stateMachine.GetAnimator().SetFloat(targetingForwardHash, value, 0.1f, deltaTime);
        }

        if (movementValue.x == 0)
        {
            stateMachine.GetAnimator().SetFloat(targetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = movementValue.x > 0 ? 1f : -1f;
            stateMachine.GetAnimator().SetFloat(targetingRightHash, value, 0.1f, deltaTime);
        }

    }
}
