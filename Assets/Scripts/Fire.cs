using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fire : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 50;
    public AudioClip[] fireSounds; // The sound effects
    private AudioSource audioSource; // The audio source
    private bool canFire = true; // Whether the gun can be fired
    private float fireCooldown = 0.5f; // The cooldown time in seconds
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        audioSource = GetComponent<AudioSource>(); // Get the audio source
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (canFire)
        {
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 0.4f);

            // Select a random sound effect
            AudioClip fireSound = fireSounds[Random.Range(0, fireSounds.Length)];
            audioSource.PlayOneShot(fireSound); // Play the sound effect

            // Start the cooldown
            StartCoroutine(FireCooldown());
        }
    }

    private IEnumerator FireCooldown()
    {
        canFire = false; // Prevent firing
        yield return new WaitForSeconds(fireCooldown); // Wait for the cooldown time
        canFire = true; // Allow firing again
    }
}