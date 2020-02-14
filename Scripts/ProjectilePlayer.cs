using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    private float speed = 100f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * -speed * Time.deltaTime;


        if (transform.position.y <= startPos.y-200)
            Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider other)
    {
        /*if(other.tag == "BreakableBuilding")
        {
            other.GetComponent<BuildingMain>().GotHit();
            Destroy(gameObject);
        }

        else if(other.tag == "BreakableBuildingChild")
        {
            other.GetComponent<BuildingChild>().GotHit();
            Destroy(gameObject);
        }

        else if(other.tag == "Enemy")
        {
            other.GetComponent<TankBasic>().GotHit();
            Destroy(gameObject);
        }*/
    }
}
