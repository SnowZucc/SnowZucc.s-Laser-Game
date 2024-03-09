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
        navMeshAgent.enabled = false;
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var item in rigidbodies)
        {
            item.isKinematic = false;
            item.drag = 10f; // Increase drag to slow down
            item.angularDrag = 10f; // Increase angularDrag to slow down rotation
        }

        StartCoroutine(ResetDragAfterDelay(rigidbodies, 0.2f));

        StartCoroutine(DestroyAfterDelay(5.0f));
    }
} 

private IEnumerator ResetDragAfterDelay(Rigidbody[] rigidbodies, float delay)
{
    yield return new WaitForSeconds(delay);
    foreach (var item in rigidbodies)
    {
        item.drag = 0f; // Reset drag
        item.angularDrag = 0.05f; // Reset angularDrag
    }
}

    private IEnumerator ResetTimeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
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