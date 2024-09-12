using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action OnJumped;
    public event Action OnDodged;
    public event Action OnTargetEvent;
    public event Action OnCancelTargetEvent;
    public event Action<InputAction.CallbackContext> OnPrimaryAttackButtomPressed;
    public event Action<InputAction.CallbackContext> OnSecondaryAttackButtomPressed;

    public Vector2 MovementValue { get; private set; }


    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }



    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        OnJumped?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        OnDodged?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        OnTargetEvent?.Invoke();
    }

    public void OnCancelTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        OnCancelTargetEvent?.Invoke();
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        OnPrimaryAttackButtomPressed?.Invoke(context);
    }

    public void OnSecondaryAttack(InputAction.CallbackContext context)
    {
        OnSecondaryAttackButtomPressed?.Invoke(context);
    }
}
