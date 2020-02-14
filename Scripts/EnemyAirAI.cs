using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirAI : MonoBehaviour
{
    [Header("Enemy AI")]
    [Tooltip("Movement speed")]
    [SerializeField] float speed;

    [Tooltip("Projectile being used")]
    [SerializeField] ProjectilesData projectileData;

    [Tooltip("Position projectile is spawning from")]
    [SerializeField] List<GameObject> shootPos;

    private float timer;
    [Tooltip("Firing intervals")]
    [SerializeField] float interval;

    void Awake()
    {
        speed = 50.0f;
        timer = interval;
    }

    void FixedUpdate()
    {
        ShipMovement();
        ShipShoot();
    }

    void ShipMovement()
    {
        transform.position += Vector3.down * -speed * Time.deltaTime;
    }

    void ShipShoot()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            int i;

            for (i = 0; i < shootPos.Count; i++)
            {
                GameObject projectile = Instantiate(
                    projectileData.GetCurrentActiveWeapon(),
                    shootPos[i].GetComponent<Transform>().position,
                    shootPos[i].GetComponent<Transform>().rotation
                    );

                SoundManager.instance.PlayRaw("PlayerBasicWeapon");
                projectile.GetComponent<Rigidbody>().
                    AddForce(Vector3.up * projectileData.GetForce(), ForceMode.Impulse);

                Destroy(projectile, 2.0f);
            }

            timer = interval;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Death")
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
    }
}
