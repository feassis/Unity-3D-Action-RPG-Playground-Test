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


    protected PlayerAttackStateBase(PlayerStateMachine playerStateMachine, int attackId) : base(playerStateMachine)
    {
        this.attackId = attackId;

        attackData = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.GetAnimator().CrossFadeInFixedTime(attackData.AnimationName, attackData.TransitionDuration);
        stateMachine.GetInputReader().OnPrimaryAttackButtomPressed += OnPrimaryAttackPressed;
        stateMachine.WeaponDamage.SetAttackDamage(attackData.AttackDamage);
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        Move(deltaTime);
        FaceTarget();


        float normalizedTime = GetNormalizedTime();

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
        base.Exit();

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

    protected float GetNormalizedTime()
    {
        AnimatorStateInfo currInfo = stateMachine.GetAnimator().GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.GetAnimator().GetNextAnimatorStateInfo(0);

        if(stateMachine.GetAnimator().IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if(!stateMachine.GetAnimator().IsInTransition(0) && currInfo.IsTag("Attack"))
        {
            return currInfo.normalizedTime;
        }

        return 0f;
    }
}