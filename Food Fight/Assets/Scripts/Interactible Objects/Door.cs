using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    public int KeysRequired = 2;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void ShowMessage(string message)
    {
        Debug.Log("Message");
        GameManager.UIMessageBox.SetActive(true);
        Text text = GameManager.UIMessageBox.GetComponentInChildren<Text>();
        if (text) { text.text = message; }
    }

    public virtual void CloseMessage()
    {
        Debug.Log("MessageClose");
        GameManager.UIMessageBox.SetActive(false);
        Text text = GameManager.UIMessageBox.GetComponentInChildren<Text>();
        if (text) { text.text = ""; }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Collided");
            if (!isOpen && GameManager.KeysObtained >= KeysRequired)
            {
                StartCoroutine(OpenDoor());
                isOpen = true;
                GameManager.instance.KeysUsed(KeysRequired);
                this.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                //OutputMessage
                ShowMessage($"You require {KeysRequired - GameManager.KeysObtained} more key(s)");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            CloseMessage();
        }
    }

    public IEnumerator OpenDoor()
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 targetScale = new Vector3(this.transform.localScale.x, 0.01f ,this.transform.localScale.z);
        
        float currentTime = 0f;
        float ScaleTime = 1.2f;

        while(currentTime < ScaleTime)
        {
            currentTime += Time.deltaTime;

            this.transform.localScale = Vector3.Lerp(startScale, targetScale, currentTime / ScaleTime);
            Debug.Log(currentTime);
            yield return null;
        }

        var doorColliders = this.gameObject.GetComponentsInChildren<BoxCollider>();

        foreach(BoxCollider collider in doorColliders)
        {
            collider.enabled = false;
        }

    }

}
