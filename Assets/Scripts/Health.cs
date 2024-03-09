using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        disableRagdoll();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
        //animator.enabled = false;
        navMeshAgent.enabled = false;
        foreach (var item in GetComponentsInChildren<Rigidbody>())
            {
                item.isKinematic = false;
            }

            StartCoroutine(DestroyAfterDelay(5.0f));
        }
    } 

    public void disableRagdoll()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = true;
        }
        //animator.enabled = true;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}