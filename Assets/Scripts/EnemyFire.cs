using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyFire : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 50;
    public AudioClip[] fireSounds; // The sound effects
    private AudioSource audioSource; // The audio source
public float damage;

// Start is called before the first frame update
void Start()
{
    audioSource = GetComponent<AudioSource>(); // Get the audio source
    audioSource.spatialBlend = 1f;
    StartCoroutine(AutoFire());
}

public void FireBullet()
{
    GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
    Destroy(spawnedBullet, 1f);

    // Select a random sound effect
    AudioClip fireSound = fireSounds[Random.Range(0, fireSounds.Length)];
    audioSource.PlayOneShot(fireSound); // Play the sound effect
}

private IEnumerator AutoFire()
{
    while (true)
    {
        FireBullet();

        float fireCooldown = Random.Range(1f, 5f); // The cooldown time in seconds, randomly between 3 and 10
        yield return new WaitForSeconds(fireCooldown); // Wait for the cooldown time
    }
}
}
