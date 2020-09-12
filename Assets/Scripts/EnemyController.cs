using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{

    public GameObject enemyObject;
    public float secondsBetweenSpawn;
    public float elapsedTime = 0.0f;
    private Rigidbody2D rb;
    public GameObject laserPrefab;
    private GameObject[] enemylist;
    
    // Update is called once per frame
    void Update()
    {
        // spawn enemy
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0;

            // random spawn point just over camera
            float x = Random.Range(2.2f, -2.2f);
            float y = Random.Range(9.5f, 11f); 

            Vector3 spawnPosition = new Vector3 (x, y, 0f);
            GameObject newEnemy = (GameObject) Instantiate(enemyObject, spawnPosition, Quaternion.Euler (0, 0, 0));
            Destroy(newEnemy, 10); // destroy after 10 seconds, just enough to go out of screen
            
            enemylist = GameObject.FindGameObjectsWithTag("Enemy"); // put a list of all enemies into this array
            rb = enemylist[Random.Range(0, enemylist.Length)].GetComponent<Rigidbody2D>(); // pick random enemy from array
            Shoot();
            
            if(secondsBetweenSpawn > 0) secondsBetweenSpawn = secondsBetweenSpawn - 0.02f;
        }
    }

    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, rb.position + Vector2.down * 0.2f, Quaternion.identity);
        BulletController projectile = laser.GetComponent<BulletController>();
        projectile.Shoot(new Vector2(0,-1), 150);
        projectile.Break(2);
    }
}
