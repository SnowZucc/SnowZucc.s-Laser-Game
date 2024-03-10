using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public TextMesh HealthText;

    public AudioClip bulletImpactSound; // The sound effects
    private AudioSource audioSource; // The audio source
    public AudioClip Death;

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
        StartCoroutine(DeathAndReset());
    }
}

    private void UpdateHealthText()
    {
        Debug.Log("Updated text");
        HealthText.text = currentHealth +  "HP";
    }

    private IEnumerator DeathAndReset()
    {
        audioSource.PlayOneShot(Death);
        Time.timeScale = 0f; // Freeze time
        yield return new WaitForSecondsRealtime(5); // Wait for 5 seconds
        Time.timeScale = 1f; // Unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reset the game
    }
}