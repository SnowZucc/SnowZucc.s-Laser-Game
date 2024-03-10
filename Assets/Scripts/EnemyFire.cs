using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyFire : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 50;
    public AudioClip[] fireSounds; 
    private AudioSource audioSource; 
public float damage;

// Start is called before the first frame update
void Start()
{
    audioSource = GetComponent<AudioSource>(); 
    audioSource.spatialBlend = 1f;
    StartCoroutine(AutoFire());
}

public void FireBullet()
{
    GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
    Destroy(spawnedBullet, 1f);

    AudioClip fireSound = fireSounds[Random.Range(0, fireSounds.Length)];
    audioSource.PlayOneShot(fireSound); 
}

private IEnumerator AutoFire()
{
    while (true)
    {
        FireBullet();

        float fireCooldown = Random.Range(2f, 3f); 
        yield return new WaitForSeconds(fireCooldown); 
    }
}
}
