using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerPrimaryAttackState : PlayerAttackStateBase
{
    public PlayerPrimaryAttackState(PlayerStateMachine playerStateMachine, int attackId) : base(playerStateMachine, attackId)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
