using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [Space]
    [SerializeField] GameObject[] lives;
    [Space]
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] GameObject arrow;
    int scoreCount = 0;
    int coinsCount = 0;
    int health;

    private void Start()
    {
        //Find what the health is and set the appropitate amount of lives on;
        SetupHealth();

        arrow.SetActive(false);
    }

    public void SetupHealth()
    {
        var healthCache = FindObjectOfType<ShipMovement>();
        if (healthCache != null)
            health = healthCache.GetHealth();
        else
            Debug.LogError("Missing Reference to ShipMovement");

        //go through the whole array and make sure it is all disable
        for (int i = 0; i < lives.Length; i++)
            lives[i].SetActive(false);

        //Now set active the correct amount of lives using updated health
        for (int i = 0; i < health; i++)
            lives[i].SetActive(true);
    }

    private void OnEnable()
    {
        BuildingDestruction.OnDamageReceived += UpdateScore;
        TankBasic.OnDamageReceived += UpdateScore;
        Beam.OnBeamConsumed += UpdateCoins;
        ShipMovement.OnHeal += AddLife;
        ShipMovement.OnHit += RemoveLife;
        ShipMovement.OnHomiePickUp += EnableArrow;
        ShipMovement.OnLevelEdgeReached += DisableArrow;

    }

    public void DisableArrow()
    {
        arrow.SetActive(false);
    }
    private void EnableArrow()
    {
        arrow.SetActive(true);
    }

    private void OnDisable()
    {
        BuildingDestruction.OnDamageReceived -= UpdateScore;
        TankBasic.OnDamageReceived -= UpdateScore;
        Beam.OnBeamConsumed -= UpdateCoins;
        ShipMovement.OnHeal -= AddLife;
        ShipMovement.OnHit -= RemoveLife;
        ShipMovement.OnHomiePickUp -= EnableArrow;
        ShipMovement.OnLevelEdgeReached -= DisableArrow;
    }

    public void AddLife()
    {
        for(int i=0; i < lives.Length; i++)
        {
            if(lives[i].gameObject.activeSelf == false)
            {
                lives[i].SetActive(true);
                return;
            }
        }
    }

    public void RemoveLife() 
    {
        for (int i = lives.Length - 1;  i >= 0; i--)
        {
            if (lives[i].gameObject.activeSelf == true)
            {
                lives[i].SetActive(false);
                return;
            }
        }
    }

    public void UpdateScore(int scoreAmount)
    {
        scoreCount += scoreAmount;
        string strScoreAmount = scoreCount.ToString("D9");
        score.text = strScoreAmount;
        playerStats.levelScoreCollected += scoreAmount;
    }

    public void UpdateCoins()
    {
        var item = FindObjectOfType<Beam>();

        if(item.GetItemGrabbed() == Beamable.ItemType.Coin)
        {
            coinsCount += 5;
            coins.text = coinsCount.ToString("D5");
            playerStats.levelCoinsCollected = coinsCount;
        }
    }
}
