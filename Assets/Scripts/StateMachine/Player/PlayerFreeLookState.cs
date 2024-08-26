using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int freeLookSpeedAnimParameter = Animator.StringToHash("FreeLookSpeed");

    private const float animatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        stateMachine = playerStateMachine;
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.GetCharacterController().Move(movement * deltaTime * stateMachine.GetFreeLookMovementSpeed());

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
