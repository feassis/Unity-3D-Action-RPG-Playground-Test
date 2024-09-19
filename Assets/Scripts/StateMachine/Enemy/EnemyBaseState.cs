using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    [field: SerializeField] protected EnemyStateMachine StateMachine { get; private set; }

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        var toPlayer = StateMachine.Player.transform.position - StateMachine.transform.position;

        return toPlayer.sqrMagnitude <= StateMachine.PlayerChasingRange * StateMachine.PlayerChasingRange;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 finalMotion = motion + StateMachine.ForceReciever.Movement;
        finalMotion *= deltaTime;

        StateMachine.Controller.Move(finalMotion);
    }

    protected void FacePlayer()
    {
        if (StateMachine.Player == null)
        {
            return;
        }

        Vector3 facinngDir = StateMachine.Player.transform.position - StateMachine.transform.position;

        facinngDir.y = 0;

        StateMachine.transform.rotation = Quaternion.LookRotation(facinngDir);
    }
}
