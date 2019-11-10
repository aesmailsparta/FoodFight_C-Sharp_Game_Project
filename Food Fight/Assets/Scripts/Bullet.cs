using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    protected float BulletSpeed;
    protected float BulletDamageMax;
    protected float BulletDamageMin;
    private Vector3 DefaultLocalScale;
    private float BulletDamageBonus = 1f;
    private float criticalHitMultiplier;
    public Material StrongBulletMatierial;
    public Gradient StrongBulletTrailGradient;

    protected Rigidbody bulletRb;

    public Vector3 shootDirection;

    void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        Destroy(this.gameObject, 2.0f);
        BulletSpeed = 25f;
        BulletDamageMax = 20f;
        BulletDamageMin = 15f;
        criticalHitMultiplier = 1.3f;
        DefaultLocalScale = transform.localScale;
        shootDirection = transform.forward;
        bulletRb = this.GetComponent<Rigidbody>();
    }

    public void setMoveDirection(Vector3 moveDir)
    {
        shootDirection = moveDir;
    }

    public void SetDamageBonus( float bonus )
    {
        BulletDamageBonus = bonus;
        transform.localScale *= 1.5f;
        gameObject.GetComponent<MeshRenderer>().material = StrongBulletMatierial;

        Light l = gameObject.GetComponentInChildren<Light>();
        l.color = StrongBulletMatierial.color;

        TrailRenderer tr = gameObject.GetComponentInChildren<TrailRenderer>();

        tr.colorGradient = StrongBulletTrailGradient;
        tr.transform.localScale += Vector3.one * 0.7f;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        UpdateBulletPosition();
    }

    public void UpdateBulletPosition()
    {
        if (bulletRb != null)
        {
            bulletRb.transform.position += shootDirection * BulletSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(BulletDamageMin * BulletDamageBonus);
        if (other.gameObject.tag == "Enemy")
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            bool CriticalHit = false;
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (Random.value <= 0.2f) {
                CriticalHit = true;
                health.TakeDamage(Random.Range(BulletDamageMax * BulletDamageBonus, BulletDamageMax * criticalHitMultiplier * BulletDamageBonus), CriticalHit);
            }
            else
            {
                health.TakeDamage(Random.Range(BulletDamageMin * BulletDamageBonus, BulletDamageMax * BulletDamageBonus), CriticalHit);
            }

            if (enemyRB)
            {
                enemyRB.AddForce(-bulletRb.transform.forward);
            }

            if (other) { 
                GameManager.EnemyHit = other.gameObject;
            }

            Debug.Log("EnemyHit");
        }

        else if (other.gameObject.tag == "DestructibleObject")
        {
            Rigidbody objectRB = other.gameObject.GetComponent<Rigidbody>();
            bool CriticalHit = false;
            DestructibleObjectHealth health = other.gameObject.GetComponent<DestructibleObjectHealth>();
            if (Random.value <= 0.2f)
            {
                CriticalHit = true;
                health.TakeDamage(Random.Range(BulletDamageMax * BulletDamageBonus, BulletDamageMax * criticalHitMultiplier * BulletDamageBonus), CriticalHit);
            }
            else
            {
                health.TakeDamage(Random.Range(BulletDamageMin * BulletDamageBonus, BulletDamageMax * BulletDamageBonus), CriticalHit);
            }

            if (objectRB)
            {
                objectRB.AddForce(-bulletRb.transform.forward);
            }

            if (other)
            {
                GameManager.EnemyHit = other.gameObject;
            }
        }

        if (!other.CompareTag("EventTrigger"))
        {
            Destroy(this.gameObject);
        }
    }
}
