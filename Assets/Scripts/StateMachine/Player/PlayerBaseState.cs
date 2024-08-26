using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine) 
    { 
        stateMachine = playerStateMachine;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
       
    }

    public override void Tick(float deltaTime)
    {
       
    }
}
