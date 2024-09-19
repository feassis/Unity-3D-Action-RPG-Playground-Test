using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int[] AttackAnimations = { 
                                                Animator.StringToHash("Mutant Punch"), 
                                                Animator.StringToHash("Mutant Swiping") 
                                              };

    private const float attackTransition = 0.1f;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        foreach( var weaponDamage in StateMachine.Weapons)
        {
            weaponDamage.SetAttackDamage(StateMachine.AttackDamage, StateMachine.AttackKnockback);
        }

        StateMachine.Animator.CrossFadeInFixedTime(GetRandomAnimHash(), attackTransition);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(StateMachine.Animator) >= 1)
        {
            StateMachine.SwitchState(new EnemyIdleState(StateMachine));
        }

        Move(deltaTime);
    }

    public override void Exit()
    {
     
    }

    

    private int GetRandomAnimHash()
    {
        var sortedNum = Random.Range(0, AttackAnimations.Length);

        return AttackAnimations[sortedNum];
    }
}
