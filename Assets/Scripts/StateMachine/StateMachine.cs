using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State desiredState)
    {
        currentState?.Exit();

        currentState = desiredState;

        currentState?.Enter();
    }
}
