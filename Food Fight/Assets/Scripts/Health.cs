using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField]
    protected float MaxHealth;
    [SerializeField]
    protected float currentHealth;
    [SerializeField]
    private Slider HealthBar;

    //Object Information
    public string Name = "Unknown";
    public Sprite Icon;
    public bool HoldingKey = false;


    Color startColor;
    private MeshRenderer meshRenderer;
    protected float damageEffectTime;
    protected bool isDead = false;

    public GameObject damageTextPrefab;
    public GameObject HealFX;
    public GameObject DieFX;

    // Start is called before the first frame update
    void Awake()
    {
        SetUp();
        try { 
            HealthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Slider>();
        }
        catch (Exception e)
        {

        }
        finally
        {
            UpdateHealthUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DeadStatus()
    {
        return isDead;
    }

    public void UpdateHealthUI()
    {
        if (HealthBar != null)
        {
            float normalizedHealth = currentHealth / MaxHealth;
            HealthBar.value = normalizedHealth;
        }
    }

    public virtual void SetUp()
    {
        if (MaxHealth == 0) { MaxHealth = 100f; }
        currentHealth = MaxHealth;
        damageEffectTime = 0.2f;
        meshRenderer = this.GetComponent<MeshRenderer>();
        startColor = meshRenderer.material.color;
        //Debug.Log("setupcomplete");
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public void setNewHealth(int maxHealth)
    {
        //Debug.Log("NewHealthSet");
        MaxHealth = maxHealth;
        currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float damage, bool isCriticalHit)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            FlashRed();
           
            GameObject dt = (GameObject)Instantiate(damageTextPrefab, this.gameObject.transform.position + Vector3.up * this.gameObject.transform.localScale.y/2, Camera.main.transform.rotation);
            DamageText script = dt.gameObject.GetComponent<DamageText>();
            if (isCriticalHit) { script.SetColor(Color.blue); }
            script.SetText(Mathf.Round(damage).ToString());
        }
        else
        {
            Die();
        }

        UpdateHealthUI();
    }

    public void Heal(float health)
    {
        currentHealth = Mathf.Min(currentHealth + health, MaxHealth);
        FlashGreen();

        if (HealFX) { Instantiate(HealFX, this.transform); }
           
        GameObject ht = (GameObject)Instantiate(damageTextPrefab, this.gameObject.transform.position, Camera.main.transform.rotation);
        DamageText script = ht.gameObject.GetComponent<DamageText>();

        script.SetColor(Color.green);
        script.SetText(Mathf.Round(health).ToString());

        UpdateHealthUI();
    }

    void FlashRed()
    {
        meshRenderer.material.color = Color.red;
        Invoke("ResetColor", damageEffectTime);
    }

    void FlashGreen()
    {
        meshRenderer.material.color = Color.green;
        Invoke("ResetColor", damageEffectTime);
    }

    void ResetColor()
    {
        meshRenderer.material.color = startColor;
    }

    public void SpawnDieFX()
    {
        if (DieFX) { Instantiate(DieFX, this.transform.position, Quaternion.identity); }
    }

    public virtual void Die()
    {
        this.isDead = true;
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in renderers)
            {
                mr.enabled = false;
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(LoadGameOver());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Drop Item Or Key
        ItemDrop dropper = gameObject.GetComponent<ItemDrop>();
        if (dropper)
        {
            if (HoldingKey)
            {
                //Debug.Log("I Should Drop A Key");
                //Drop Key
                dropper.DropKey(this.gameObject.transform);
            }
            else
            {

                //Drop Item
                dropper.DropItem(this.gameObject.transform);
            }
        }
    }

    IEnumerator LoadGameOver()
    {
        //Animate Die

        //Load GameOver
        Debug.Log("Hello");
        GameManager.TotalTime += GameManager.instance.GetTime();
        int LivesLost = PlayerPrefs.GetInt("LivesLost", 0);
        LivesLost++;

        PlayerPrefs.SetInt("BulletsFired", GameManager.BulletsFired);
        PlayerPrefs.SetInt("EnemiesKilled", GameManager.EnemiesKilled);
        PlayerPrefs.SetFloat("TotalTime", GameManager.TotalTime);
        PlayerPrefs.SetInt("LivesLost", LivesLost);


        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
}
