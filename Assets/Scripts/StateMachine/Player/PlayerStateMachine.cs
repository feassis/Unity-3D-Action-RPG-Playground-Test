using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float freeLookMovementSpeed = 6;
    [SerializeField] private Animator animator;

    [field: SerializeField] public float RotationDamping { get; private set; }

    public Transform MainCameraTransform { get; private set; }

    public InputReader GetInputReader() { return inputReader; }

    public CharacterController GetCharacterController() {  return characterController; }

    public float GetFreeLookMovementSpeed() {  return freeLookMovementSpeed; }

    public Animator GetAnimator() {  return animator; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
}
