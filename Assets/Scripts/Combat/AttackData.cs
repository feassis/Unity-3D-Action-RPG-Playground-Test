using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackData
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;

    [field: SerializeField] public float ComboAttackTime { get; private set; } = 0.9f;
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float ForceTime { get; private set; } = 0.9f;
    [field: SerializeField] public int AttackDamage { get; private set; } = 5;
    [field: SerializeField] public float AttackKnockback { get; private set; } = 1;
}
