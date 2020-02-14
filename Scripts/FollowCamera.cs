using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject playerShip;
    Renderer rend;
    float xMinClamp;
    [SerializeField] float yMinClamp;

    [SerializeField] float xMaxClamp;
    [SerializeField] float yMaxClamp; //Might not need the Y to adjust, but you never know :)
    public float div;

    private void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        rend = playerShip.GetComponentInChildren<Renderer>();

        //set x min and x max to the size of the level
        xMinClamp = 0.0f;
        GameObject transXMax = GameObject.FindGameObjectWithTag("LevelEnd");
        xMaxClamp = transXMax.transform.position.x - Camera.main.orthographicSize * 2 + rend.bounds.size.x; 
        
        //- Screen.width / div;
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(playerShip.transform.position.x, xMinClamp, xMaxClamp);
        float y = Mathf.Clamp(playerShip.transform.position.y, yMinClamp, yMaxClamp);

        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
