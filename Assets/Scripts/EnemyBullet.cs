using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage; 

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision player");
            var health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage); 
            }

            // Start a coroutine to destroy the bullet after a delay
            StartCoroutine(DestroyBulletAfterDelay());
            Debug.Log("Destroy delay");
        }
        else if (collision.gameObject.tag == "Map")
        {
            Destroy(gameObject);
            Debug.Log("Destroy map");
        }
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        Debug.Log("Destroy delay");
        // Wait for the specified delay
        yield return new WaitForSeconds(0.1f);

        // Destroy the bullet
        Destroy(gameObject);
    }
}