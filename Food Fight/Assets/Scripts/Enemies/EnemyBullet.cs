using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{


    public void FixedUpdate()
    {
        UpdateBulletPosition();
    }

    void OnTriggerEnter(Collider other)
    {
        BulletDamageMax = 18f;
        BulletDamageMin = 13f;
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

        if (!other.CompareTag("EventTrigger") && !other.CompareTag("Enemy") && !other.CompareTag("EnemyBullet"))
        {
            Debug.Log(other.tag);
            Destroy(this.gameObject);
        }
    }
}
