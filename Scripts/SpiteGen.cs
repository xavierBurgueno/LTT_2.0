using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiteGen : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 60000; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(50f, 50f, 50f);
            sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
            sphere.transform.position = Random.insideUnitSphere * 2000;
        }
    }
}
