using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Ragdoll.ToggleRagdoll(true);

        foreach(var weapon in StateMachine.Weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        GameObject.Destroy(StateMachine.Target);
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        
    }
}
