using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // Set this to the amount of damage you want the bullet to do

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Enemy")
    {
        var health = collision.gameObject.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Destroy the bullet on impact with enemy
        Destroy(gameObject);
    }
}
}