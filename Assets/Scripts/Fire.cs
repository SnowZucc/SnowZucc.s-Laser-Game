using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fire : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 50;
    public AudioClip[] fireSounds; // The sound effects
    public AudioClip reloadSound; // The reload sound effect
    private AudioSource audioSource; // The audio source
    private bool canFire = true; // Whether the gun can be fired
    private float fireCooldown = 0.3f; // The cooldown time in seconds
    public float damage;
    private int shotsFired = 0; // The number of shots fired
    private bool isReloading = false; // Whether the gun is reloading

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        audioSource = GetComponent<AudioSource>(); // Get the audio source
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        if (canFire && !isReloading)
        {
            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 0.4f);

            // Select a random sound effect
            AudioClip fireSound = fireSounds[Random.Range(0, fireSounds.Length)];
            audioSource.PlayOneShot(fireSound); // Play the sound effect

            shotsFired++;
            if (shotsFired >= 10)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound); // Play the reload sound effect
        yield return new WaitForSeconds(3); // Wait for 3 seconds
        shotsFired = 0; // Reset the number of shots fired
        isReloading = false; // The gun is no longer reloading
    }
}