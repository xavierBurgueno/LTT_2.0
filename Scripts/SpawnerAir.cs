using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAir: MonoBehaviour
{
    [Header("Enemies")]
    [Tooltip("Enemies that will spawn")]
    [SerializeField] List<GameObject> enemy;
    [Tooltip("boss that will spawn")]
    [SerializeField] GameObject boss;

    public int count;
    [Tooltip("Max amount of enemies that can spawn")]
    [SerializeField] int maxCount;

    private GameObject spawnObj;
    [Header("Spawners")]
    [Tooltip("Spawns available")]
    [SerializeField] List<GameObject> spawner;

    [Tooltip("Boss Spawn position")]
    [SerializeField] GameObject Bossspawn;

    [Header("Spawn Active")]
    [Tooltip("Default disabled")]
    [SerializeField] bool spawnActive;
    [SerializeField] bool bossActive;

    private float timer;
    [Header("Timer")]
    [Tooltip("Time between spawns")]
    [SerializeField] float maxTimer;

    private float ranValue;
    private int ranEnemy;
    
    private int ranSpawn;

    void Awake()
    {
        count = 0;

        spawnActive = false;
        bossActive = false;

        timer = maxTimer;

        ranValue = 0;
        ranEnemy = 0;

        ranSpawn = 0;

        spawnObj = spawner[0];
    }

    void FixedUpdate()
    {
        Spawn();
    }

    void Spawn()
    {
        if (spawnActive == true && count < maxCount)
        {
            timer -= Time.deltaTime;

            //Creates precentage
            ranValue = (1.0f / enemy.Count);

            //Creates random number from enemy list
            ranEnemy = UnityEngine.Random.Range(0, enemy.Count);

            //Creates random number from spawn list
            ranSpawn = UnityEngine.Random.Range(0, spawner.Count);

            spawnObj = spawner[ranSpawn];

            if (timer <= 0)
            {
                if (UnityEngine.Random.value <= ranValue)
                {
                    Instantiate(enemy[ranEnemy], spawnObj.GetComponent<Transform>().position, transform.rotation);

                    count++;
                    
                    timer = maxTimer;
                }
            }
        }

        if (bossActive == true)
        {
            Instantiate(boss, Bossspawn.GetComponent<Transform>().position, transform.rotation);
            bossActive = false;
        }
    }
}
