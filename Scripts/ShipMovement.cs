using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Organize and label the class
public class ShipMovement : MonoBehaviour
{
    public static Action OnHit;
    public static Action OnHeal;
    public static Action OnDeath;
    public static Action OnHomiePickUp;
    public static Action OnLevelEdgeReached;
    

    [SerializeField] float movementSpeed;
    [SerializeField] GameObject tractorBeam;
    [SerializeField] [Range(0,9)] int health; 

    float beamOffset = 11;
    bool beamButton1;
    bool beamButton2;
    [SerializeField] bool useKeys;

    [SerializeField] GameObject shootPos;

    [SerializeField] ProjectilesData projectileData;
    bool rightShoot, leftShoot;
    bool isInitRunning = false;
    private const int MAX_HEALTH = 9;

    // Start is called before the first frame update
    void Start()
    {

        //Init
        projectileData.InitateData();

        //set both bool flags to false
        beamButton1 = false;
        beamButton2 = false;

        //bool flags for shooting
        rightShoot = false;
        leftShoot = false;

        //Offset the beam at start to the current ship model
        Vector3 tempPos = this.transform.localPosition;

        tempPos.y -= this.transform.localPosition.y - GetComponentInChildren<Renderer>().bounds.size.y + beamOffset;

        tractorBeam.transform.position = tempPos;
        shootPos.transform.position = tempPos;
    }

   
    void Update()
    {
         //Movement input handled by button events, update only called when button is held down       
    }


    public void UsePickUp()
    {
        var pickUpType = GetComponentInChildren<Beam>().GetItemGrabbed();
       

        if (pickUpType == Beamable.ItemType.Health)
            Heal();
        
        if(pickUpType == Beamable.ItemType.Homie)
        {
            HomiePickedUp();
        }

    }

    public void HomiePickedUp()
    {
        if(OnHomiePickUp != null)
        {
            OnHomiePickUp();
        }
    }

    private void Heal()
    {
       
        if (health < MAX_HEALTH)
        {

            health++;

            if (OnHeal != null)
                OnHeal();
        }
         
        
    }

    private void Hit()
    {
        if (health > 0)
        {
            health--;

            if (OnHit != null)
                OnHit();
        }
        else
        {
            //Player must have died
        }
            
    }

    private void PlayerDeath()
    {
        if (OnDeath != null)
            OnDeath();
    }

    //Enable and disable -----------------------------------------
    private void OnEnable()
    {
        //Subscriptions
        Beam.OnBeamConsumed += UsePickUp;
        Beamable.onBeamableSpawned += EnableBeam;

    }

    private void OnDisable()
    {
        Beam.OnBeamConsumed -= UsePickUp;
        Beamable.onBeamableSpawned -= EnableBeam;
    }

    private void EnableBeam()
    {
        if (!isInitRunning)
            StartCoroutine(EnableBeamForInit());
    }

    IEnumerator EnableBeamForInit()
    {
        isInitRunning = true;
        tractorBeam.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        tractorBeam.SetActive(false);
        isInitRunning = false;

    }

    //Button Click Events--------------------------------------------


    public void RightShoot()
    {
        if (!leftShoot) // if the left button isn't firing
        {
            rightShoot = true;
            GameObject projectile = Instantiate(
                projectileData.GetCurrentActiveWeapon(),
                shootPos.transform.position,
                Quaternion.identity
                );

            SoundManager.instance.PlayRaw("PlayerBasicWeapon");
            projectile.GetComponent<Rigidbody>().
                AddForce(Vector3.down * projectileData.GetForce(), ForceMode.Impulse);

            Destroy(projectile, 2.0f);
            rightShoot = false;
        }
    }

    public void LeftShoot()
    {
        if (!rightShoot) // if the right button isn't firing
        {
            leftShoot = true;
            GameObject projectile = Instantiate(
                projectileData.GetCurrentActiveWeapon(),
                shootPos.transform.position,
                Quaternion.identity
                );

            SoundManager.instance.PlayRaw("PlayerBasicWeapon");
            projectile.GetComponent<Rigidbody>().
                AddForce(Vector3.down * projectileData.GetForce(), ForceMode.Impulse);

            Destroy(projectile, 2.0f);
            leftShoot = false;
        }
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
    }

    public void ActivateBeamButton1()
    {
        beamButton1 = true;
    }


    public void ActivateBeamButton2()
    {
        beamButton2 = true;
    }

    public void DeactivateBeamButton1()
    {
        beamButton1 = false;
    }

    public void DeactivateBeamButton2()
    {
        beamButton2 = false;
    }

    //Accessors and Mutators----------------------------------------------------
    public int GetHealth()
    {
        return health;
    }

    //Collision Detection--------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyProjectile")
        {
            Hit();
        }

        if(other.gameObject.tag == "LevelEnd")
        {
            if (OnLevelEdgeReached != null)
                OnLevelEdgeReached();

           
        }
    }
}
