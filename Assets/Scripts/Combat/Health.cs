using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int dmg)
    {
        if(health == 0)
        {
            return;
        }

        health = Mathf.Clamp(health - dmg, 0, maxHealth);
    }

    public void Heal(int healingAmount)
    {
        if(health == 0)
        {
            return;
        }

        health = Mathf.Clamp(health + healingAmount, 0, maxHealth);
    }
}
