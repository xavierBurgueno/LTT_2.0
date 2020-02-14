using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMain : MonoBehaviour
{

    public static float currScore = 0;
    GameObject scoreObject;
    [SerializeField]
    Text scoreDisplay;



    // Start is called before the first frame update
    void Start()
    {
        scoreObject = GameObject.Find("CurrentScore");
        scoreDisplay = scoreObject.GetComponent<Text>();
        scoreDisplay.text = currScore.ToString();
    }

    public void ResetScore()
    {
        currScore = 0;
    }

    public void AddToScore(float points)
    {
        currScore += points;
        scoreDisplay.text = currScore.ToString();
    }
}
