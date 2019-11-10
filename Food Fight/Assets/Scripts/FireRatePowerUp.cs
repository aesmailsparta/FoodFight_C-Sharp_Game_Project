using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUp
{

    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        if (player)
        {
            var pc = player.GetComponent<PlayerController>();
            pc.fireRate = 0.1f;
        }
    }

    public override void DisablePowerUp()
    {
        base.DisablePowerUp();
        if (player)
        {
            var pc = player.GetComponent<PlayerController>();
            pc.resetFireRate();
        }
    }
}
