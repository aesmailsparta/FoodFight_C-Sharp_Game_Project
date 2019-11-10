using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectHealth : Health
{
    private float HealTime = 1f;
    private float DefaultHealTime = 2f;
    //public bool HoldingKey = false;

    public GameObject brokenObject;
    // Start is called before the first frame update
    void Awake()
    {
        this.SetUp();
    }

    public override void TakeDamage(float damage, bool isCriticalHit)
    {
        base.TakeDamage(damage, isCriticalHit);
        this.HealTime = DefaultHealTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealTime <= 0 && currentHealth != this.MaxHealth)
        {
            Heal(10);
            HealTime = DefaultHealTime;
        }
        else
        {
            HealTime -= Time.deltaTime;
        }
    }

    public override void Die()
    {
        base.Die();
        SpawnDieFX();
        if (brokenObject) { Instantiate(brokenObject, this.transform.position, Quaternion.identity); }
        Destroy(this.gameObject);
    }

}
