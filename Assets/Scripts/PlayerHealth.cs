using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    audioSource.PlayOneShot(bulletImpactSound);
    currentHealth -= amount;
    UpdateHealthText();

    if (currentHealth <= 0.0f)
    {
        
    }
} 


    private void UpdateHealthText()
    {
        HealthText.text = currentHealth +  "HP";
    }
}