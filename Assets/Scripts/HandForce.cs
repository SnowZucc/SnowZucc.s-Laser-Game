using UnityEngine;

public class HandForce : MonoBehaviour
{
    public float damage;
    private Vector3 lastPosition; // The position of the hand in the previous frame
    private Vector3 currentVelocity; // The velocity of the hand

    private void Update()
    {
        // Calculate the current velocity (to apply the force to the enemy because it won't work automaticaly with onCollision)
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;

        // Store the current position for the next frame
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var health = other.gameObject.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                if (health.currentHealth <= 0)
                {
                    // Apply force to the Rigidbody that was directly hit
                    var hitRigidbody = other.attachedRigidbody;
                    if (hitRigidbody != null)
                    {
                        // Calculate the direction from the hand to the enemy
                        Vector3 direction = other.transform.position - transform.position;

                        // Apply a force in the direction of the impact, proportional to the hand's velocity
                        hitRigidbody.AddForce(direction.normalized * currentVelocity.magnitude * 15, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}