using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject cubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

public void TakeDamage(float amount)
{
    currentHealth -= amount;
    if (currentHealth <= 0.0f)
    {

    }
    Vector3 cubePosition = transform.position + Vector3.down * transform.localScale.y / 2;
        Instantiate(cubePrefab, cubePosition, Quaternion.identity);
} 
}