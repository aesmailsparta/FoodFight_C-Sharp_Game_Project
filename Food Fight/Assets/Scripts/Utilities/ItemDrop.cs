using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject Key;
    
    [Range(0, 1)]
    public float DropChance;

    public void DropItem(Transform DropLocation)
    {
        if (items.Count > 0 && Random.value < DropChance)
        {
            Instantiate(items[Random.Range(0, items.Count)], DropLocation.position, Quaternion.identity);

            Debug.Log(DropLocation.position);
        }
    }


    public void DropKey(Transform DropLocation)
    {
        if (Key)
        {
            Debug.Log("We Have A Key");
            Instantiate(Key, DropLocation.position, Quaternion.Euler(0, 0, -50));
        }
    }
}
