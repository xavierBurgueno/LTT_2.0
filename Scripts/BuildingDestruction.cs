using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildingDestruction : MonoBehaviour
{

    public static Action<int> OnDamageReceived;
    public static Action OnBuildingDestroyed;

    [SerializeField] ProjectilesData playerProjectile;

    private GameObject[] buildingSections;
    [Space]

    [Header("BuildingAttributes")]
    [SerializeField] [Range(1,100)] int healthPerSection = 30;
    private int sectionHealth = 0;
    private int currentSection = 0;
    
    [Tooltip("Score must be intervals of 10")]
    [SerializeField] int scoreRewardAmount;
    private const int SCORE_DEFAULT = 10;

    [Header("Particles")]
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject hitParticle;


    private bool isbuildingSectionCollapsing = false;
    private bool hasCollapsed = false;
    private bool inAnimationState = false;
    private BoxCollider boxCol;



    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();

        sectionHealth = healthPerSection;

        buildingSections = new GameObject[this.transform.childCount];
        for(int i = 0; i < this.transform.childCount; i++)
        {
            buildingSections[i] = transform.GetChild(i).gameObject;
        }

        //Check if score is divisible by 10
        if (scoreRewardAmount <= 0)
            scoreRewardAmount = SCORE_DEFAULT;
        else if (scoreRewardAmount % 10 != 0)
            scoreRewardAmount = SCORE_DEFAULT;


        //Center box collider to first mesh
        ResizeBoxCollider();
    }

    private void GotHit(int damage)
    {    
        if(sectionHealth > 0 && !hasCollapsed && !inAnimationState)
        {
            sectionHealth -= damage;

            //Update the score
            if (OnDamageReceived != null)
            {
                OnDamageReceived(scoreRewardAmount);
            }
        }
        
        if(sectionHealth <= 0 && !hasCollapsed && !inAnimationState)
        {
            StartCoroutine(CollapseSection());
        }
    }

    IEnumerator CollapseSection()
    {
        isbuildingSectionCollapsing = true;
        int particleCount = 30;
        
        yield return null;

        while(particleCount > 0 && !hasCollapsed)
        {
            inAnimationState = true;
            SoundManager.instance.Play("BuildingCollapse");
            UseParticle();
            LowerBuilding();
            yield return new WaitForSeconds(0.1f);
            particleCount--;

        }

        inAnimationState = false;

        buildingSections[currentSection].SetActive(false);
        if (currentSection + 1 < transform.childCount)
        {
            sectionHealth = healthPerSection;
            currentSection++;
            buildingSections[currentSection].SetActive(true);
            ResizeBoxCollider();
            particleCount = 30;

        }
        if(currentSection + 1 >= transform.childCount)
        {
            sectionHealth = -1;
            buildingSections[transform.childCount - 1].SetActive(true);
            ResizeBoxCollider();

            if (OnBuildingDestroyed != null)
                OnBuildingDestroyed();

            hasCollapsed = true;
        }
          
        isbuildingSectionCollapsing = false;
    }
   
   


    private void UseParticle()
    {
        var center = boxCol.bounds.center;
        var min = boxCol.bounds.min;
        var max = boxCol.bounds.max;

        Vector3 spawnArea =
            new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

        var exp = Instantiate(explosion, spawnArea, Quaternion.identity);
       
        //clean up the particle
        Destroy(exp, 0.5f);

        //Maybe object pooling for this one
    }


    private void ResizeBoxCollider()
    {
        boxCol.center = transform.GetChild(currentSection).GetComponent<MeshFilter>().mesh.bounds.center;
        boxCol.size = transform.GetChild(currentSection).GetComponent<MeshFilter>().mesh.bounds.size;
    }

    private void LowerBuilding()
    {
        var pos = transform.GetChild(currentSection).position;
        transform.GetChild(currentSection).position = new Vector3(pos.x, pos.y - 0.75f, pos.z);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerProjectile" && !hasCollapsed && !inAnimationState)
        {
            var positionOfProjectile = other.GetComponent<Transform>().position;
            var explo = Instantiate(hitParticle, positionOfProjectile, Quaternion.identity);
            Destroy(explo, 0.5f);
            SoundManager.instance.PlayRaw("CollisionBasicShot");

            GotHit((int)playerProjectile.GetWeaponDamage());
        }
    }
}

//TODO: Place the sound, include the building dropping down