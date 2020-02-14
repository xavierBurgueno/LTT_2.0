using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardSpawner : MonoBehaviour
{
    [Header("Rewards Available")]
    [SerializeField] GameObject coin;
    [SerializeField] [Range(0, 100)] float coinDropProbability;
    [Space]

    [SerializeField] GameObject health;
    [SerializeField] [Range(0, 100)] float healthDropProbability;
    [Space]

    [SerializeField] GameObject rocket;
    [SerializeField] [Range(0, 100)] float rocketDropProbability;
    [Space]

    [SerializeField] GameObject laser;
    [SerializeField] [Range(0, 100)] float laserDropProbability;
    [Space]

    [SerializeField] GameObject ultimate;
    [SerializeField] [Range(0, 100)] float ultimateDropProbability;

    [Header("Drop Probability")]
    [SerializeField] [Range(0, 100)] float overallProbability;

    private void OnEnable()
    {
        BuildingDestruction.OnBuildingDestroyed += PoopReward;
    }

    public void PoopReward()
    {
        var randNum = GenerateRandomNumber();

        if (randNum <= overallProbability)
        {
            if (randNum <= 50 && randNum != 0)
                Debug.Log("You Got them Weapons Dawg");//WeaponDrop();
            else if( randNum > 50)
                Consumables();
        }
    }

    private void OnDisable()
    {
        BuildingDestruction.OnBuildingDestroyed -= PoopReward;
    }

    private void Consumables()
    {
        var randNum = GenerateRandomNumber();

        if (randNum <= 50 && randNum != 0)
            CoinDrop();
        else if(randNum > 50)
            HealthDrop();
    }

    public void WeaponDrop()
    {
        var randNum = GenerateRandomNumber();

        if (randNum <= 33)
            LaserDrop();
        else if (randNum > 33 && randNum <= 66)
            RocketDrop();
        else if (randNum > 66)
            UltimateDrop();
        else
            return;

    }

    private void LaserDrop()
    {
        if (GenerateRandomNumber() <= laserDropProbability)
            Instantiate(laser, this.transform.position, Quaternion.identity);
    }

    private void RocketDrop()
    {
        if (GenerateRandomNumber() <= rocketDropProbability)
            Instantiate(rocket, this.transform.position, Quaternion.identity);
    }

    private void UltimateDrop()
    {
        if (GenerateRandomNumber() <= ultimateDropProbability)
            Instantiate(ultimate, this.transform.position, Quaternion.identity);
    }

    private void CoinDrop()
    {
        if (GenerateRandomNumber() <= coinDropProbability)
            Instantiate(coin, this.transform.position, Quaternion.identity);
    }

    private void HealthDrop()
    {
        if (GenerateRandomNumber() <= healthDropProbability)
            Instantiate(health, this.transform.position, Quaternion.identity);
    }

    private float GenerateRandomNumber()
    {
        return Random.Range(1, 100);
    }
}
