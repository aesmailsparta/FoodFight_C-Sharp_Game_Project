using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    protected GameObject player;

    protected bool IsActive = false;
    public float ModifyTimer = 5f;

    public GameObject ModifyFX;

    private GameObject ModifyFXInstance;

    public virtual void ActivatePowerUp()
    {
        if (this.ModifyFX && player) { ModifyFXInstance = (GameObject)Instantiate(ModifyFX, player.transform); }
        Debug.Log("PowerUpActivated");
    }

    public virtual void DisablePowerUp()
    {
        if (ModifyFXInstance) { Destroy(ModifyFXInstance.gameObject); }
        Debug.Log("PowerUpDeactivated");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.gameObject;

            PowerUpController pc = collider.GetComponent<PowerUpController>();

            pc.ActivatePowerUp(this);

            Destroy(gameObject);
        }
    }
}
