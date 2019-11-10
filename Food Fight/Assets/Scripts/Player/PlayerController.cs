using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public Health health;
    public float movementSpeed;
    public float pushBackForce;
    public float rotationSmoothing;

    public FixedJoystick joystick;
    public FixedJoystick aimjoystick;

    public float fireRate;
    private float fireRateDefault;

    private float BulletDamageBonus;
    private float timeBetweenShots;

    private bool IsJumping = false;

    private Vector3 input;
    private Ray MousePosition;
    private Vector3 newMousePos;


    // Start is called before the first frame update
    void Start()
    {
        //Set RB
        rb = this.GetComponent<Rigidbody>();
        movementSpeed = 13.0f;
        input = new Vector3();
        rotationSmoothing = 5f;
        pushBackForce = 3f;

        health = this.gameObject.GetComponent<Health>();

        BulletDamageBonus = 1f;
        fireRateDefault = 0.3f;
        fireRate = fireRateDefault;
        timeBetweenShots = fireRate;
    }

    public void resetFireRate()
    {
        fireRate = fireRateDefault;
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.DeadStatus())
        {
            //Get Movement
           // float x = Input.GetAxisRaw("Horizontal");
           // float z = Input.GetAxisRaw("Vertical");

            //Joystick
            float x = joystick.Horizontal;
            float z = joystick.Vertical;

            input = new Vector3(x, 0, z);

            if ((Input.GetKey(KeyCode.L) || Input.GetMouseButton(0)) && timeBetweenShots <= 0)
            {
                GameObject bgo = (GameObject)Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                if (BulletDamageBonus != 1) { bgo.GetComponent<Bullet>().SetDamageBonus(BulletDamageBonus); }
                //Camera.main.GetComponent<CameraFollow>().CameraShake();
                timeBetweenShots = fireRate;
                GameManager.BulletsFired++;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }

            if ((Input.GetKey(KeyCode.Space) && !IsJumping))
            {
                Jump();
                IsJumping = true;
            }

            if (Input.GetMouseButtonDown(1))
            {

                Collider[] EnemiesWithinRange = Physics.OverlapSphere(this.transform.position, 4f);

                foreach (Collider collider in EnemiesWithinRange)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        Debug.Log("exploding");
                        //collider.GetComponent<Rigidbody>().AddExplosionForce(500f, this.transform.position, 3f);
                        collider.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.Normalize(collider.transform.position - this.transform.position) * 15f, this.transform.position, ForceMode.Impulse);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameManager.instance.Teleport(this.gameObject, GameManager.instance.Shooter);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GameManager.instance.Teleport(this.gameObject, GameManager.instance.Boss);
            }

            //DoLookPlayer();

            // Debug.Log(x + " " + z);
        }
    }

    public void Jump()
    {
        //rb.AddForce(Vector3.up * 10f, ForceMode.VelocityChange);
        rb.velocity = Vector3.up * 6f;
        //this.transform.position += Vector3.up * 1.5f * Time.fixedDeltaTime;
    }

    public void SetBulletDamageBonus(float Bonus)
    {
        BulletDamageBonus = Bonus <= 1 ? 1 : Bonus;
    }

    private void FixedUpdate()
    {
        //Move Player
        if (rb != null && !health.DeadStatus())
        {
            float xForce = (movementSpeed - rb.velocity.x) * input.x;
            float zForce = (movementSpeed - rb.velocity.z) * input.z;

            //transform.position += new Vector3(xForce, 0 , zForce) * Time.fixedDeltaTime;
            rb.velocity = (new Vector3(input.x, 0, input.z) * movementSpeed + Vector3.up * rb.velocity.y) ;
            DoLookPlayer();
        }

        //DoLookPlayer();

    }

    void DoLookPlayer()
    {
        if (aimjoystick != null)
        {
            Vector3 aimView = new Vector3(aimjoystick.Direction.x, 0 , aimjoystick.Direction.y) + transform.position;

            Quaternion rotation = Quaternion.LookRotation(aimView - this.transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSmoothing * Time.fixedDeltaTime);

            bulletSpawn.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSmoothing * Time.fixedDeltaTime);

        }
        else {
            Plane p = new Plane(Vector3.up, this.gameObject.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayLength;
            if (p.Raycast(ray, out rayLength))
            {
                Vector3 mouseWorldPoint = ray.GetPoint(rayLength);
                // TODO make the cake look at the mouseWorldPoint
                //rb.transform.LookAt(mouseWorldPoint);
                //newMousePos = mouseWorldPoint;
                //Vector3 CorrectionVector = new Vector3(1f, 0, 1f);
                //mouseWorldPoint.y = this.transform.position.y;
                Quaternion rotation = Quaternion.LookRotation(mouseWorldPoint - this.transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSmoothing * Time.fixedDeltaTime);

                bulletSpawn.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSmoothing * Time.fixedDeltaTime);

            }

            //LookTowardsMousePosition(newMousePos);
        }
    }

    public void LookTowardsMousePosition(Vector3 lookAt)
    {
        var angle = Mathf.Atan2(newMousePos.x, newMousePos.y) * Mathf.Rad2Deg;
        var toward = Quaternion.AngleAxis(angle, Vector3.up);
        Quaternion q = Quaternion.Euler(new Vector3(0,(Mathf.Acos(Vector3.Dot(rb.transform.position, lookAt)) * Time.fixedDeltaTime),0));
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, toward, 3f * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("EnemyAttacked");
            EnemyMovement em = collision.gameObject.GetComponent<EnemyMovement>();
            if (health) { health.TakeDamage(em.GetAttackPower(), false); }


            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(collision.gameObject.transform.localPosition - this.gameObject.transform.position) * pushBackForce, ForceMode.Impulse);
        }

        if(collision.gameObject.tag == "Ground")
        {
            IsJumping = false;
        }
    }
}
