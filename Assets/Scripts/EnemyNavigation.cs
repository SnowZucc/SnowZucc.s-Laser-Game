using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator; // Animator disabled

    public AudioClip spawnSound; // The sound effects
    private AudioSource audioSource; // The audio source

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the audio source
        audioSource.spatialBlend = 1f;
        audioSource.PlayOneShot(spawnSound);
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Assign the Main Camera's transform to the player variable
        player = Camera.main.transform;
    }

    // Update is called once per frame
void Update()
{
    // Set the player's position as the destination
    agent.destination = player.position;

    // If the enemy is not moving
    if (agent.velocity.magnitude < 0.1f)
    {
        // Calculate the direction vector from the enemy to the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // This line ensures that the enemy only rotates around the y-axis

        // Create a rotation based on this direction vector
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation *= Quaternion.Euler(0, -4, 0);

        // Apply this rotation to the enemy's transform
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

     animator.SetFloat("speed", agent.velocity.magnitude);
}
}