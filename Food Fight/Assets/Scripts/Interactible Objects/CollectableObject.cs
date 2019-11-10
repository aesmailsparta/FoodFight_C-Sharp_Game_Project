using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{

    public GameObject FX;

    [SerializeField]
    private bool isKey;

    //what to do when object is collected
    public virtual void DoOnCollect() {
        if (isKey) {
            GameManager.instance.KeyPickedUp();
            Debug.Log(GameManager.KeysObtained);
        }
        React();
    }

    //how the collectable should react on screen
    public virtual void React()
    {
        if (FX) { Instantiate(FX, this.gameObject.transform.position, Quaternion.identity); }
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoOnCollect();
        }
    }
}
