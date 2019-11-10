using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : EnemyMovement
{
    [SerializeField]
    private bool isAttacking = false;

    private bool isDead = false;

    private float DefaultSpeed;
    private float DecisionTimer;
    private float TimeBetweenDecisions = 7f;

    public GameObject[] enemies;
    public GameObject BulletPos;
    public GameObject MinionSpawn;
    public GameObject Bullet;

    public Transform CenterPosition;

    public EnemyHealth health;

    void Start()
    {
        setUpEnemyMovement();

        this.health = this.gameObject.GetComponent<EnemyHealth>();
        this.health.setNewHealth(1000);

        this.movementSpeed = 4f;
        this.MinFollowDistance = 40f;
        this.EnemyAttackPower = 25f;

        this.DecisionTimer = TimeBetweenDecisions;
    }

    private void FixedUpdate()
    {
        if ((this.player.transform.position - this.transform.position).magnitude < MinFollowDistance && !isDead)
        {
            if (!isAttacking) { 
                Debug.Log("Default Enemy AI");

                StartEnemyAI();
            }  

            if(DecisionTimer < 0 && !isAttacking)
            {
                Debug.Log("EnemyDecision");

                MakeDecision();
                DecisionTimer = TimeBetweenDecisions;
            }
            else if(!isAttacking)
            {
                DecisionTimer -= Time.fixedDeltaTime;
            }
        }
    }

    public void MakeDecision()
    {

        isAttacking = true;

        //Get a Random number which will determine the Boss Attack
        int AttackNumber = Random.Range(1, 5);

        switch(AttackNumber)
        {

            case 1:
                Shoot();
                break;
            case 2:
                SpawnMinions(5, 12);
                break;
            case 3:
                Charge();
                break;
            case 4:
                Shoot();
                break;
            default:
                break;

        }

    }

    public void SpawnMinions(int SpawnAmount, float spreadDistance)
    {
        //Take number of Minions to spawn and distance over which to distribute them

        //Calculate distance between each Minion and the first spawn location
        float perEnemySpawnDistance = spreadDistance / (SpawnAmount - 1f);
        float startDistance = spreadDistance * -0.5f;

        if (health.GetHealth() > health.GetMaxHealth() / 2)
        {
            //if Boss health greater than 50%
            for(int i = 0; i < SpawnAmount; i++)
            {
                GameObject newMinion = Instantiate(enemies[0], MinionSpawn.transform.position + Vector3.forward, this.transform.rotation);
                newMinion.transform.position += Vector3.left * (startDistance + i * perEnemySpawnDistance);
            }
        }
        else if(health.GetHealth() > health.GetMaxHealth() / 4)
        {
            //if Boss health greater than 25%
            for (int i = 0; i < SpawnAmount; i++)
            {
                GameObject newMinion = Instantiate(enemies[Random.Range(0, enemies.Length)], MinionSpawn.transform.position, this.transform.rotation);
                newMinion.transform.position += Vector3.left * (startDistance + i * perEnemySpawnDistance);
            }
        }
        else
        {
            //if Boss health is less than 25%
            for (int i = 0; i < SpawnAmount; i++)
            {
                GameObject newMinion = Instantiate(enemies[1], MinionSpawn.transform.position, this.transform.rotation);
                newMinion.transform.position += Vector3.left * (startDistance + i * perEnemySpawnDistance);
            }
        }

        isAttacking = false;

    }

    public void Shoot()
    {
        StartCoroutine(FireBullets());
    }

    public void Charge()
    {
        StartCoroutine(ChargeAtPlayer());
    }

    public void Die()
    {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        this.isDead = true;

        GameManager.instance.LevelComplete();

    }

    IEnumerator FireBullets()
    {
        //Move to a defined shooting location
        while((this.transform.position - CenterPosition.position).magnitude > 0.1f)
        {
            Vector3 LookAtVector = CenterPosition.position;
            LookAtVector.y = this.transform.position.y;

            this.transform.LookAt(LookAtVector);
            this.transform.position += Vector3.Normalize(CenterPosition.position - this.transform.position) * 12f * Time.deltaTime;

            yield return null;
        }

        int counter = 0;
        int bullets = 15; // Number of bullets to spawn
        float spreadAngle = 336f; // Angle to spread bullets over
        float perBulletAngle = spreadAngle / (bullets - 1f);
        float startAngle = spreadAngle * -0.5f;

        //Fire a round of bullets 3 Times
        while (counter < 3)
        {

            float LookSmoothing = 4f;
            float t = 0f;

            while (t <= 0.5f)
            {
                Vector3 LookAtVector = player.transform.position;
                LookAtVector.y = this.transform.position.y;
                Quaternion rotation = Quaternion.LookRotation(LookAtVector - this.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, LookSmoothing * Time.fixedDeltaTime);
                t += Time.fixedDeltaTime;
                yield return null;
            }

            for (int i = 0; i < bullets; i++)
            {

                GameObject NewBullet = (GameObject)Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation);
                //Debug.Log(this.transform.position);
                NewBullet.transform.Rotate(Vector3.up, startAngle + i * perBulletAngle);

            }

            counter++;
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
        yield return null;
    }
    

    IEnumerator ChargeAtPlayer()
    {
        //TODO IMPLEMENT : Charing Bull
        isAttacking = false;
        yield return null;
    }

    
}
