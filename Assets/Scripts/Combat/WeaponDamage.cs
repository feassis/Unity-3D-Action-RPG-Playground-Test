using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private int damage;
    private float knockback;

    public void SetAttackDamage(int dmg, float knockback = 0f)
    {
        damage = dmg;
        this.knockback = knockback;
    }

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == myCollider)
        {
            return;
        }

        if (alreadyCollidedWith.Contains(other))
        {
            return;
        }

        alreadyCollidedWith.Add(other);

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if(other.TryGetComponent<ForceReciever>(out ForceReciever receiver))
        {
            var direction = (other.transform.position - myCollider.transform.position);
            direction.y = 0f;
            direction = direction.normalized;

            receiver.AddForce(direction * knockback);
        }
    }
}
