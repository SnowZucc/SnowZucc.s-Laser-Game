using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

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
        agent.destination = player.position;
        animator.SetFloat("speed", agent.velocity.magnitude);
    }
}