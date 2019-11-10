using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    List<PowerUp> activeKeys = new List<PowerUp>();

    Dictionary<PowerUp, float> activePowerUps = new Dictionary<PowerUp, float>();

    public void UpdateCurrentPowerUps()
    {
        if (activePowerUps.Count > 0)
        {
            foreach(PowerUp p in activeKeys)
            {
                if (activePowerUps[p] > 0) {
                    activePowerUps[p] -= Time.deltaTime;
                }
                else
                {
                    //Remove
                    activePowerUps.Remove(p);
                    activeKeys = new List<PowerUp>(activePowerUps.Keys);
                    p.DisablePowerUp();
                }

            }
        }
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (!activePowerUps.ContainsKey(powerUp))
        {
            activePowerUps.Add(powerUp, powerUp.ModifyTimer);
            powerUp.ActivatePowerUp();
        }
        else
        {
            activePowerUps[powerUp] = powerUp.ModifyTimer;
        }

        activeKeys = new List<PowerUp>(activePowerUps.Keys);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activePowerUps.Count > 0)
        {
            UpdateCurrentPowerUps();
        }
    }
}
