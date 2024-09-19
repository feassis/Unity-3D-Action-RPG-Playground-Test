using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float freeLookMovementSpeed = 6;
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter {  get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set;}

    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float TargetingSpeed {  get; private set; }
    [field: SerializeField] public AttackData[] Attacks {  get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }


    public Transform MainCameraTransform { get; private set; }

    public InputReader GetInputReader() { return inputReader; }

    public CharacterController GetCharacterController() {  return characterController; }

    public float GetFreeLookMovementSpeed() {  return freeLookMovementSpeed; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
}
