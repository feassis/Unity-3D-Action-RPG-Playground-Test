using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int speedParameter = Animator.StringToHash("Speed");
    private readonly int locomotionBlendTree = Animator.StringToHash("Locomotion");

    private const float crossFadeDuration = 0.1f;
    private const float animatorDampTime = 0.1f;

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Animator.CrossFadeInFixedTime(locomotionBlendTree, crossFadeDuration);
        
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
            return;
        }
        else if(IsInAttackRange())
        {
            StateMachine.SwitchState(new EnemyAttackState(StateMachine));
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer();

        StateMachine.Animator.SetFloat(speedParameter, 1f, animatorDampTime, deltaTime);
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (StateMachine.NavMeshAgent.isOnNavMesh)
        {
            StateMachine.NavMeshAgent.destination = StateMachine.Player.transform.position;

            Move(this.StateMachine.NavMeshAgent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);

            
        }
        StateMachine.NavMeshAgent.velocity = StateMachine.Controller.velocity;

    }

    public override void Exit()
    {
        StateMachine.NavMeshAgent.ResetPath();
        StateMachine.NavMeshAgent.velocity = Vector3.zero;
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= StateMachine.AttackRange * StateMachine.AttackRange;
    }

}
