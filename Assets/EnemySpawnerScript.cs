using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    //public GameObject healthPack;
    public float upperX = 1.0f;
    public float upperZ = 1.0f;
    public float minX = -1.0f;
    public float minZ = -1.0f;
    public float spawnRate = 2.0f;
    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Spawn();
            spawnTimer = 0;
        }
    }
    void Spawn()
    {
        int healthPackChance = Random.Range(0, 4);

        float x = Random.Range(transform.position.x + minX, transform.position.x + upperX);
        float z = Random.Range(transform.position.z + minZ, transform.position.z + upperZ);
        Vector3 enemyPosition = new Vector3(x, transform.position.y, z);
        Instantiate(enemy, enemyPosition, transform.rotation);
        if (healthPackChance == 2)
        {
            //Instantiate(healthPack, enemyPosition, transform.rotation);
        }
    }
}
