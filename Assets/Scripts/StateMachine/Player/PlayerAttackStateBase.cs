using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackStateBase : PlayerBaseState
{
    [SerializeField] protected AttackData attackData;
    [SerializeField] protected int attackId;

    protected float previousFrameTime;

    protected bool hasPressedAttackButtom = false;
    protected bool alreadyApplyedForce = false;


    public PlayerAttackStateBase(PlayerStateMachine playerStateMachine, int attackId) : base(playerStateMachine)
    {
        this.attackId = attackId;

        attackData = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attackData.AnimationName, attackData.TransitionDuration);
        stateMachine.GetInputReader().OnPrimaryAttackButtomPressed += OnPrimaryAttackPressed;
        stateMachine.WeaponDamage.SetAttackDamage(attackData.AttackDamage, attackData.AttackKnockback);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();


        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime >= previousFrameTime && normalizedTime < 1)
        {
            if (normalizedTime > attackData.ForceTime)
            {
                TryApplyForce();
            }

            if (hasPressedAttackButtom)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
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

        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {
        stateMachine.GetInputReader().OnPrimaryAttackButtomPressed -= OnPrimaryAttackPressed;
    }

    protected virtual void TryComboAttack(float normalizedTime)
    {
        
        if (attackData.ComboStateIndex < 0)
        {
            return;
        }

        if (normalizedTime < attackData.ComboAttackTime)
        {
            return;
        }

        stateMachine.SwitchState(new PlayerAttackStateBase(stateMachine, attackData.ComboStateIndex));
    }

    private void OnPrimaryAttackPressed(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        hasPressedAttackButtom = true;
    }

    protected void TryApplyForce()
    {
        if (alreadyApplyedForce)
        {
            return;
        }
        
        stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * attackData.Force);
        alreadyApplyedForce = true;
    }
}