using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBetweenSpawn;
    private float timeUntilNextSpawn;

    public GameObject EnemyPrefab;
    public GameObject Player;

    private int MaxSpawns;

    [SerializeField]
    private float Range;

    void Start()
    {
        timeBetweenSpawn = 2.0f;
        timeUntilNextSpawn = 0;

        MaxSpawns = 50;
        Range = 35f;
    }

    void RandomiseSpawn()
    {
        timeBetweenSpawn = Random.Range(2.0f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Player && (Player.transform.position - this.transform.position).magnitude < Range) {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < MaxSpawns)
            {
                if (timeUntilNextSpawn > 0)
                {
                    timeUntilNextSpawn -= Time.deltaTime;
                }
                else
                {
                    spawnEnemy();
                    RandomiseSpawn();
                    timeUntilNextSpawn = timeBetweenSpawn;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if ((Player.transform.position - this.transform.position).magnitude < Range)
        {
            Vector3 toLook = Player.transform.position;
            toLook.y = this.transform.position.y;
            this.transform.LookAt(toLook);
        }
    }
    public void spawnEnemy()
    {
        Vector3 SpawnOffset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
        Instantiate(EnemyPrefab, this.transform.position, Quaternion.identity);
    }
}
