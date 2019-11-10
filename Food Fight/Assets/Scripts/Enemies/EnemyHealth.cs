using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    private int EnemyPointValue;
    private int BossPointValue;


    // Start is called before the first frame update
    void Awake()
    {
        this.SetUp();
        this.setNewHealth(50);
        EnemyPointValue = Random.Range(125, 200);
        BossPointValue = Random.Range(2000, 2500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        //Debug.Log(ScoreManager.score);

        base.Die();
        BossBehaviour bb = gameObject.GetComponent<BossBehaviour>();
        if (bb)
        {
            GameManager.instance.UpdateScore(BossPointValue);
            bb.Die();
        }
        else
        {
            GameManager.instance.UpdateScore(EnemyPointValue);
            SpawnDieFX();
           // if (DieFX) { Instantiate(DieFX, this.transform.position, Quaternion.identity); }
            Destroy(this.gameObject);
        }

        GameManager.EnemiesKilled++;

    }
}
