using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Beamable : MonoBehaviour
{
    public enum ItemType { None, Laser, Rocket, Ultimate, Health, Coin, Homie};

    public ItemType pickUpType;
    public float liftSpeed = 35;
    Rigidbody rb;
    Transform playerShip;
    bool useGravity;
    [SerializeField] GameObject beam;


    public static Action onBeamableSpawned; //Might be better to add the Broadcaster to the Beam 

    void Start()
    {

        if (onBeamableSpawned != null)
            onBeamableSpawned();

        beam = GameObject.FindGameObjectWithTag("Beam");
        StartCoroutine(Deactivate());
        

        useGravity = true;
        rb = GetComponent<Rigidbody>();
        playerShip = GameObject.FindWithTag("PlayerShip").transform;
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.1f);
        beam.SetActive(false);
    }

    private void Update()
    {
        if (useGravity)
            rb.useGravity = true;
        else if (!useGravity)
            rb.useGravity = false;

        if (!beam.activeSelf)
            useGravity = true;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            useGravity = false;
            transform.position += transform.up * Time.deltaTime * liftSpeed;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        useGravity = true;
    }

}
