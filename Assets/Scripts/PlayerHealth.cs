using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public TextMesh HealthText;

    public AudioClip bulletImpactSound; // The sound effects
    private AudioSource audioSource; // The audio source
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>(); // Get the audio source
        audioSource.spatialBlend = 1f;

        UpdateHealthText();
    }

public void TakeDamage(float amount)
{
    Debug.Log("Took damage");
    audioSource.PlayOneShot(bulletImpactSound);
    currentHealth -= amount;
    Debug.Log("Current Health: " + currentHealth);
    UpdateHealthText();

    if (currentHealth <= 0.0f)
    {
        Debug.Log("Ded");
    }
} 


    private void UpdateHealthText()
    {
        Debug.Log("Updated text");
        HealthText.text = currentHealth +  "HP";
    }
}