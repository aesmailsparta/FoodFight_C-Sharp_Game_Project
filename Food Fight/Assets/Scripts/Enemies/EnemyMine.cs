using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : Bullet
{
    public bool AutoDestroy = false;

    public void Start()
    {
        this.SetUp();

        if (AutoDestroy)
        {
            Destroy(this, 2.5f);
        }
    }

    public new void FixedUpdate()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        BulletDamageMax = 45f;
        BulletDamageMin = 60f;
        //Debug.Log(BulletDamageMin * BulletDamageBonus);
        if (other.gameObject.tag == "Player")
        {
            bool CriticalHit = false;
            Health health = other.gameObject.GetComponent<Health>();
            if (Random.value <= 0.2f)
            {
                CriticalHit = true;
                health.TakeDamage(Random.Range(BulletDamageMax, BulletDamageMax * 1.1f), CriticalHit);
            }
            else
            {
                health.TakeDamage(Random.Range(BulletDamageMin, BulletDamageMax), CriticalHit);
            }
        }

        if (!other.CompareTag("EventTrigger") && !other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
