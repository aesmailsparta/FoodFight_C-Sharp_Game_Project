using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed;
    public Rigidbody enemyRB;
    public GameObject player;
    public float MinFollowDistance;
    protected float CloseRange;

    protected float EnemyAttackPower;

    void Start()
    {
        setUpEnemyMovement();
    }

    public void setUpEnemyMovement()
    {
        enemyRB = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        movementSpeed = Random.Range(8f,11f);
        MinFollowDistance = 30f;
        EnemyAttackPower = 10f;
        CloseRange = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayer();
    }

    public void checkPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public float GetAttackPower()
    {
        return EnemyAttackPower;
    }

    private void FixedUpdate()
    {
        StartEnemyAI();
    }

    public virtual void StartEnemyAI()
    {
        if (enemyRB != null && player != null)
        {
            Vector3 toLook = player.transform.position;
            toLook.y = enemyRB.transform.position.y;
            enemyRB.transform.LookAt(toLook);
            Vector3 movement = Vector3.Normalize(player.transform.position - enemyRB.transform.position);
            movement.y = 0;
            enemyRB.transform.localPosition += movement * movementSpeed * Time.fixedDeltaTime;
        }
    }


}
