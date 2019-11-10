using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enters");
        if(other.gameObject.tag == "Player")
        {
           Health health = other.gameObject.GetComponent<Health>();
            if (health) { health.TakeDamage(health.GetHealth(), false); }
        }
    }
}
