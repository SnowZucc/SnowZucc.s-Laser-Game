using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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