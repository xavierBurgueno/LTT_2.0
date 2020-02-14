using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEnemies : MonoBehaviour
{
    public enum VehicleAIState
    { 
        idle,
        attack, 
        seek,
        none
    };

    private VehicleAIState state = VehicleAIState.idle;

    [Header("Enemy Properties")]
    [SerializeField] float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
