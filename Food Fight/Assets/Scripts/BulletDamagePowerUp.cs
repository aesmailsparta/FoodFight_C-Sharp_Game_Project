using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamagePowerUp : PowerUp
{
    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        if (player)
        {
            var pc = player.GetComponent<PlayerController>();
            pc.SetBulletDamageBonus(2f);
        }
    }

    public override void DisablePowerUp()
    {
        base.DisablePowerUp();
        if (player)
        {
            var pc = player.GetComponent<PlayerController>();
            pc.SetBulletDamageBonus(1);
        }
    }

}
