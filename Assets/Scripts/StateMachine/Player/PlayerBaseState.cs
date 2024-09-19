using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine) 
    { 
        stateMachine = playerStateMachine;
    }


    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 finalMotion = motion + stateMachine.ForceReciever.Movement;
        finalMotion *= deltaTime;

        stateMachine.GetCharacterController().Move(finalMotion);
    }

    protected void FaceTarget()
    {
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            return;
        }

        Vector3 facinngDir = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;

        facinngDir.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(facinngDir);
    }

    protected void ReturnToLocomotion()
    {
        if(stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.GetInputReader().MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.GetInputReader().MovementValue.y;


        return movement;
    }
}
