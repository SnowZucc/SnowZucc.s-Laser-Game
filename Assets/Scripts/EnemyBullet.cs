using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage; 
    public AudioClip killSound;
    private AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var health = collision.gameObject.GetComponentInParent<Health>();
            if (health != null)
            {
                //health.TakeDamage(damage); // Disable damage for now
                if (health.currentHealth <= 0)
                {
                    // Apply force to the Rigidbody that was directly hit
                    //audioSource.PlayOneShot(killSound);
                    var hitRigidbody = collision.rigidbody;
                    if (hitRigidbody != null)
                    {
                        // The force is in the direction of the bullet and proportional to its speed
                        hitRigidbody.AddForce(transform.forward * GetComponent<Rigidbody>().velocity.magnitude * 2, ForceMode.Impulse);
                    }
                }
            }

            // Start a coroutine to destroy the bullet after a delay
            StartCoroutine(DestroyBulletAfterDelay());
        }
        else if (collision.gameObject.tag == "Map")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(0.1f);

        // Destroy the bullet
        Destroy(gameObject);
    }
}