using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpShoot : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    Vector3 shooter;

    [SerializeField] ProjectilesData projectileData;


    // Start is called before the first frame update
    void Start()
    {
        shooter = new Vector3(transform.position.x, transform.position.y - 25, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            GameObject projectile = Instantiate(
                projectileData.GetCurrentActiveWeapon(),
                 shooter,
                Quaternion.identity
                );
 
             projectile.GetComponent<Rigidbody>().
                AddForce(Vector3.down * projectileData.GetForce(), ForceMode.Impulse);
         }
    }
}
