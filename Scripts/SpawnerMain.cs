using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMain : MonoBehaviour
{

    GameObject[] spawnPoints;

    private GameObject[] buildings;

    [SerializeField]
    GameObject alien;

    private GameObject[] powerups;

    private GameObject player;

    private int buildingCount;


    private int randRotate;
    private int alienSpawn;
    [SerializeField]
    private int totalPowerups;


    //make alien object
    //make upgrade objects
    //make health object

    /*
     * TODO
     * Go through each spawn not holding alien and randomly choose whether to place health, upgrade, or nothing
     * 
     * 
     * MAYBE TODO
     * randomly choose how many spawners spawn buildings
     * dont even use spawn points, automatically choose where buildings should spawn (dont even know how i would do this but would make level design so fucking simple)
     * 
     * */

    void Start()
    {
        player = GameObject.Find("Player");
        buildings = Resources.LoadAll<GameObject>("BreakableBuildings");
        buildingCount = buildings.Length;
        powerups = Resources.LoadAll<GameObject>("PowerUps");
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

        for(int i=0; i< spawnPoints.Length;i++)
        {
            randRotate = Random.Range(0, 4);
            int thisBuilding = Random.Range(0, buildingCount - 1);
            Vector3 thisVec = new Vector3(spawnPoints[i].transform.position.x, spawnPoints[i].transform.position.y, player.transform.position.z+63);
            Instantiate(buildings[thisBuilding], thisVec, Quaternion.Euler(0, 90*randRotate, 0));
        }

        alienSpawn = Random.Range(0, spawnPoints.Length-1);
        Instantiate(alien, new Vector3(spawnPoints[alienSpawn].transform.position.x, spawnPoints[alienSpawn].transform.position.y + 50, spawnPoints[alienSpawn].transform.position.z + 63), new Quaternion(0, 0, 0, 0));


        totalPowerups = Random.Range(0, 3);
        if (totalPowerups > 0)
            PowerUps(totalPowerups);

    }


    private void PowerUps(int powerupCount)
    {
        int[] powerupSpawn = new int[powerupCount];

        int count = 0;

        while(count <powerupCount)
        {
            int thisSpawn = Random.Range(0, spawnPoints.Length-1);

            if(thisSpawn !=alienSpawn )
            {
                if(count ==0)
                {
                    powerupSpawn[count] = thisSpawn;
                    count++;
                }

                else
                {
                    if (IsInArray(thisSpawn, powerupSpawn));
                    {
                        powerupSpawn[count] = thisSpawn;
                        count++;
                    }

                }
            }
        }

        for(int i=0; i<powerupCount;i++)
        {
            Vector3 _vecSpawn = new Vector3(spawnPoints[powerupSpawn[i]].transform.position.x, spawnPoints[powerupSpawn[i]].transform.position.y + 50, spawnPoints[powerupSpawn[i]].transform.position.z + 63);
            Instantiate(powerups[Random.Range(0, powerups.Length - 1)], _vecSpawn , new Quaternion(0, 0, 0, 0));
        }
    }


    private bool IsInArray(int x, int[]y)
    {
        for(int i=0; i<y.Length;i++)
        {
            if (x == y[i])
                return false;
        }

        return true;
    }
}
