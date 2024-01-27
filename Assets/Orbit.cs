using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakout : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Orbit around the main sphere
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
        // Change color of the sphere every frame
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1);
    }
}
