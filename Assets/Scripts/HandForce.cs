using UnityEngine;

public class HandForce : MonoBehaviour
{
    public float damage;
    public AudioClip killSound;
    public float minForceForDamage;
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private Vector3 currentVelocity;
    private bool hasHit = false; // Add this line

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !hasHit) // Check the hasHit variable here
        {
            if (currentVelocity.magnitude >= minForceForDamage)
            {
                var health = other.gameObject.GetComponentInParent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    if (health.currentHealth <= 0)
                    {
                        audioSource = GetComponent<AudioSource>();
                        audioSource.PlayOneShot(killSound);
                        var hitRigidbody = other.attachedRigidbody;
                        if (hitRigidbody != null)
                        {
                            Vector3 direction = other.transform.position - transform.position;
                            hitRigidbody.AddForce(direction.normalized * currentVelocity.magnitude * 15, ForceMode.Impulse);
                        }
                    }
                }
            }
            hasHit = true; // Set the hasHit variable to true after the enemy has been hit
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hasHit = false; // Reset the hasHit variable when the hand leaves the enemy's collider
        }
    }
}