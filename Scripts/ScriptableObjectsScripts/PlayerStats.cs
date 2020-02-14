using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{

    public int totalCoins;
    public int totalScore;

     public int levelCoinsCollected;
     public int levelScoreCollected;

    public void ResetLevelCoins()
    {
        levelCoinsCollected = 0;
    }

    public void ResetLevelScore()
    {
        levelScoreCollected = 0;
    }

    public void DoubleCoinsCollected()
    {
        levelCoinsCollected *= 2;
    }

    public void UpdateTotalCoins()
    {
        totalCoins += levelCoinsCollected;
    }

    public void UpdateTotalScore()
    {
        totalScore += levelScoreCollected;
    }

    public bool PurchaseTrasaction(int price)
    {
        if (price > totalCoins)
        {
            return false;
        }
        else if (price <= totalCoins)
        {
            totalCoins -= price;
            return true;
        }
        else
            return false;
    }



}
