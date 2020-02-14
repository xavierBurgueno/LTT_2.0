using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerAI : MonoBehaviour
{
    [Header("Stalker AI")]
    [Tooltip("Movement speed")]
    [SerializeField] float speed = 25.0f;

    private bool movSide;

    [Tooltip("Projectile being used")]
    [SerializeField] ProjectilesData projectileData;

    [Tooltip("Position of turret projectile is spawning from")]
    [SerializeField] GameObject turretShootPos;
    [Tooltip("turret base object")]
    [SerializeField] GameObject turretBase;
    [Tooltip("Turret gun object")]
    [SerializeField] GameObject turretGun;
    [SerializeField] bool turretShoot;

    private float timer;
    [Tooltip("Firing interval for turret")]
    [SerializeField] float interval = 1.0f;

    [Tooltip("Position of missile projectile is spawning from")]
    [SerializeField] List<GameObject> missileShootPos;
    [SerializeField] bool missileShoot;

    [Tooltip("Position of laser projectile is spawning from")]
    [SerializeField] List<GameObject> laserShootPos;
    [SerializeField] bool laserShoot;

    [Tooltip("Position of thrusters")]
    [SerializeField] GameObject leftThrust;
    [SerializeField] GameObject rightThrust;

    private float smooth;

    private Vector3 bossSpawn;
    private Quaternion turretBaseRest;
    private Quaternion turretGunRest;

    private GameObject player;

    private bool bossRest;

    void Awake()
    {
        player = GameObject.Find("PlayerShip");

        timer = interval;

        turretShoot = false;
        missileShoot = false;
        laserShoot = false;

        movSide = false;

        smooth = 2.0f;

        bossSpawn = GetComponent<Transform>().position;
        turretBaseRest = GetComponent<Transform>().localRotation;
        turretGunRest = GetComponent<Transform>().localRotation;
    }

    void FixedUpdate()
    {
        ShipMovement();
    }

    void ShipMovement()
    {
        if (transform.position.y <= bossSpawn.y + 100)
        {
            transform.position += Vector3.down * -speed * Time.deltaTime;
            bossRest = true;
        }
        else
        {
            if (turretShoot == true)
            {
                GetComponent<TurretControl>().enabled = true;

                ShipTurretShoot();
            }
            else
            {
                GetComponent<TurretControl>().enabled = false;

                turretBase.transform.localRotation = Quaternion.Lerp(turretBase.transform.localRotation, Quaternion.identity, smooth * Time.deltaTime);
                turretGun.transform.localRotation = Quaternion.Lerp(turretGun.transform.localRotation, Quaternion.identity, smooth * Time.deltaTime);
            }

            if (missileShoot == true)
            {
                ShipMissileShoot();
            }

            if (laserShoot == true)
            {
                bossRest = false;

                ShiplaserShoot();

                if (movSide == true)
                {
                    if (transform.position.x == bossSpawn.x + 120)
                    {
                        movSide = false;
                    }
                    else
                    {
                        transform.position += Vector3.left * -speed * Time.deltaTime;
                    }
                }
                else
                {
                    if (transform.position.x == bossSpawn.x - 120)
                    {
                        movSide = true;
                    }
                    else
                    {
                        transform.position += Vector3.right * -speed * Time.deltaTime;
                    }
                }
            }
            else
            {
                if (bossRest == false)
                {
                    Vector3 newPos = new Vector3(bossSpawn.x, bossSpawn.y + 100, bossSpawn.z);
                    transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
                }
            }
        }
    }

    void ShipTurretShoot()
    {
        //This code can be expanded on to include a game object list TurretShootPos and can be nested in a for loop. I tested it but removed it cause it wasn't needed.

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject projectile = Instantiate(
                projectileData.GetCurrentActiveWeapon(),
                turretShootPos.GetComponent<Transform>().position,
                turretShootPos.GetComponent<Transform>().rotation
                );

            Debug.Log("The Force is : " + projectileData.GetForce());

            var dir = player.transform.position - transform.position;

            SoundManager.instance.PlayRaw("PlayerBasicWeapon");
            projectile.GetComponent<Rigidbody>().AddRelativeForce(projectile.transform.forward * projectileData.GetForce() * 20, ForceMode.Impulse);

//             projectile.GetComponent<Rigidbody>().
//                 AddForceAtPosition(dir.normalized, transform.forward * projectileData.GetForce() * 100, ForceMode.Impulse);

            Destroy(projectile, 4.0f);
            
            timer = interval;
        }
    }

    void ShiplaserShoot()
    {
        timer -= Time.deltaTime;

        if (timer / 4 <= 0)
        {
            int i;

            for (i = 0; i < laserShootPos.Count; i++)
            {
                GameObject projectile = Instantiate(
                    projectileData.GetCurrentActiveWeapon(),
                    laserShootPos[i].GetComponent<Transform>().position,
                    laserShootPos[i].GetComponent<Transform>().rotation
                    );

                SoundManager.instance.PlayRaw("PlayerBasicWeapon");
                projectile.GetComponent<Rigidbody>().
                    AddForce(Vector3.up * projectileData.GetForce(), ForceMode.Impulse);

                Destroy(projectile, 4.0f);
            }

            timer = interval;
        }
    }

    void ShipMissileShoot()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            int i;

            for (i = 0; i < missileShootPos.Count; i++)
            {
                GameObject projectile = Instantiate(
                    projectileData.GetCurrentActiveWeapon(),
                    missileShootPos[i].GetComponent<Transform>().position,
                    missileShootPos[i].GetComponent<Transform>().rotation
                    );

                SoundManager.instance.PlayRaw("PlayerBasicWeapon");
                projectile.GetComponent<Rigidbody>().
                    AddForce(Vector3.up * projectileData.GetForce(), ForceMode.Impulse);

                Destroy(projectile, 4.0f);
            }

            timer = interval;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Death")
        {
            var spawnerAir = FindObjectOfType<SpawnerAir>();
            spawnerAir.count--;
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "PlayerProjectile")
        {
            var spawnerAir = FindObjectOfType<SpawnerAir>();
            spawnerAir.count--;
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "LevelEnd")
        {
            if (movSide == true)
            {
                movSide = false;
            }
            else
            {
                movSide = true;
            }
        }
    }
}
