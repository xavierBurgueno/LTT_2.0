using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{

    [SerializeField]
    GameObject player;
    Vector3 playerPos;

    [SerializeField]
    float speed;

    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);

        
        transform.LookAt(player.transform.position);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (transform.position.y > player.transform.position.y +100)
            Destroy(gameObject);
    }
}

