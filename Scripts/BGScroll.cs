using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{

    public float Speed = -0.25f;

    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * Speed);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
