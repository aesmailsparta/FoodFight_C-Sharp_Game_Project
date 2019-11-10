using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : CollectableObject
{

    public int HealthLevel = 1;
    public float HealMinimum = 25f;
    private GameObject player;

    //what to do when object is collected
    public override void DoOnCollect()
    {
        //Add Health To Player
        if (player)
        {
            Health health = player.GetComponent<Health>();
            health.Heal(HealMinimum * HealthLevel);
        }
        React();
    }

    //how the collectable should react on screen
    public override void React()
    {
        if (this.FX && player) { Instantiate(FX, player.transform); }
        Destroy(this.gameObject);
    }

    public new void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            DoOnCollect();
        }
    }
}
