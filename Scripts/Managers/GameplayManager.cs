using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    bool hasHomie;
    
    void Start()
    {
        hasHomie = false;
        //start music to test
        //SoundManager.instance.Play("LevelMusic1");
        SceneTracker.instance.RecordCurrentScene();
    }

    private void OnEnable()
    {
        ShipMovement.OnDeath += PlayerDiedLoss;
        ShipMovement.OnHomiePickUp += HomieUpdate;
        ShipMovement.OnLevelEdgeReached += LevelWon;
    }

    public void OnDisable()
    {
        ShipMovement.OnDeath -= PlayerDiedLoss;
        ShipMovement.OnHomiePickUp -= HomieUpdate;
        ShipMovement.OnLevelEdgeReached -= LevelWon;

    }

    //Need to know when the homie gets picked up
    public void HomieUpdate()
    {
        hasHomie = true;

    }

    public void PlayerDiedLoss()
    {
        //call method to end game
        

    }

    public void LevelWon()
    {
      
       if(hasHomie)
        {
            SceneTracker.instance.UpdateIndex();

            //play win sequence animation or whatever

            //Call WinTransition Scene
            SceneTracker.instance.LoadWinScreen();

        }
    }


}
