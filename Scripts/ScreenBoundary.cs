using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundary : MonoBehaviour
{
    Camera mainCamera;
    Vector3 screenLimit;
    Renderer shipRender;
     Transform levelEdge;
    float objWidth;
    float objHeight;

    // Start is called before the first frame update
    void Start()
    {
        //Cache References for Components
        mainCamera = Camera.main; //Main Camera in game
        shipRender = GetComponentInChildren<Renderer>(); //Get the first render of the ship 
        levelEdge = GameObject.FindGameObjectWithTag("LevelEnd").transform;

        //Grab and convert the Screen points to Vector3 to use
        screenLimit = mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //Get the size of the current model 
        objWidth = shipRender.bounds.size.x / 2;
        objHeight = shipRender.bounds.size.y / 2; //Might need this later, I dont know tho


    }

    // Update is called once per frame
    void LateUpdate()
    {// + obj
        Vector3 viewPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        viewPos.x = Mathf.Clamp(viewPos.x, screenLimit.x * -1 + objWidth  , levelEdge.position.x + (objWidth/2));
        transform.position = viewPos;

    }
}

//TODO: 