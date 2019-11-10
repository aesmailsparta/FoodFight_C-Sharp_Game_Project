using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyMovement
{
    // Start is called before the first frame update
    private float FireRate = 5f;

    public float maxDefaultFireRate = 1.5f;
    public float minDefaultFireRate = 0.75f;

    public GameObject EnemyBullet;
    public GameObject EnemyBulletSpawn;

    public override void StartEnemyAI()
    {
        float PlayerDistance = (player.transform.position - this.transform.position).magnitude;

        if (player) {

            if (PlayerDistance > CloseRange && PlayerDistance < MinFollowDistance)
            {
                base.StartEnemyAI();
            }
            else if(PlayerDistance < CloseRange - 1f)
            {
                movementSpeed = -movementSpeed;
                base.StartEnemyAI();
                movementSpeed = -movementSpeed;
            }
        }

        if(FireRate < 0)
        {
            if (PlayerDistance < MinFollowDistance) {
                Shoot();
                FireRate = Random.Range(minDefaultFireRate, maxDefaultFireRate);
            }
        }
        else
        {
            FireRate -= Time.fixedDeltaTime;
        }
    }

    public void Shoot()
    {
        Instantiate(EnemyBullet,EnemyBulletSpawn.transform.position, EnemyBulletSpawn.transform.rotation);
    }
}
