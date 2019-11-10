using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDoor : Door
{
    public List<EnemyHealth> EnemiesToDefeat = new List<EnemyHealth>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (EnemyHealth eh in EnemiesToDefeat)
        {
            //Debug.Log(eh.GetHealth());
            if(eh.GetHealth() <= 0 || eh == null)
            {
                EnemiesToDefeat.Remove(eh);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            if (EnemiesToDefeat.Count <= 0)
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                ShowMessage($"You must defeat all enemies\n to open this door,\n {EnemiesToDefeat.Count} Left!");
            }
        }
    }

}
