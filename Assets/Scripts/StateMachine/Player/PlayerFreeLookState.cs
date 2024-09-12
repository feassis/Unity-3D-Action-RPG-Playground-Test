using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedAnimParameter = Animator.StringToHash("FreeLookSpeed");
    private readonly int freeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");

    private const float animatorDampTime = 0.1f;
    private const float crossFadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        stateMachine = playerStateMachine;
    }

    public override void Enter()
    {
        stateMachine.GetInputReader().OnTargetEvent += OnTarget;
        stateMachine.GetAnimator().CrossFadeInFixedTime(freeLookBlendTree, crossFadeDuration);
        stateMachine.GetInputReader().OnPrimaryAttackButtomPressed += OnPrimaryAttackPressed;
    }

    

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.GetFreeLookMovementSpeed(), deltaTime);

        if (stateMachine.GetInputReader().MovementValue == Vector2.zero)
        {
            stateMachine.GetAnimator().SetFloat(freeLookSpeedAnimParameter, 0, animatorDampTime, deltaTime);
            return;
        }

        stateMachine.GetAnimator().SetFloat(freeLookSpeedAnimParameter, 1, animatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.GetInputReader().OnTargetEvent -= OnTarget;
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

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget())
        {
            return;
        }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, 
            Quaternion.LookRotation(movement), stateMachine.RotationDamping * deltaTime);
    }

    private Vector3 CalculateMovement()
    {
        var cameraForward = stateMachine.MainCameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        var cameraRight = stateMachine.MainCameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 movement = new Vector3();

        movement.x = stateMachine.GetInputReader().MovementValue.x;
        movement.y = 0;
        movement.z = stateMachine.GetInputReader().MovementValue.y;

        return cameraForward * stateMachine.GetInputReader().MovementValue.y
            + cameraRight * stateMachine.GetInputReader().MovementValue.x;
    }
}
